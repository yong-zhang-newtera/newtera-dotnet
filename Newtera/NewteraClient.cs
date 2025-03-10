﻿/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2017-2021 Newtera, Inc.
 */

using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtera.DataModel.Result;
using Newtera.Exceptions;
using Newtera.Handlers;
using Newtera.Helper;

namespace Newtera;

public partial class NewteraClient : INewteraClient
{
    private static readonly char[] separator = { '/' };

    private bool disposedValue;

    /// <summary>
    ///     Creates and returns an Newtera Client
    /// </summary>
    /// <returns>Client with no arguments to be used with other builder methods</returns>
    public NewteraClient()
    {
    }

    public NewteraConfig Config { get; } = new();

    public IEnumerable<IApiResponseErrorHandler> ResponseErrorHandlers { get; internal set; } =
        Enumerable.Empty<IApiResponseErrorHandler>();

    /// <summary>
    ///     Default error handling delegate
    /// </summary>
    public IApiResponseErrorHandler DefaultErrorHandler { get; internal set; } = new DefaultErrorHandler();

    public IRequestLogger RequestLogger { get; internal set; }

    /// <summary>
    ///     Runs httpClient's GetAsync method
    /// </summary>
    public Task<HttpResponseMessage> WrapperGetAsync(Uri uri)
    {
        return Config.HttpClient.GetAsync(uri);
    }

    /// <summary>
    ///     Runs httpClient's PutObjectAsync method
    /// </summary>
    public Task WrapperPutAsync(Uri uri, StreamContent strm)
    {
        return Task.Run(async () => await Config.HttpClient.PutAsync(uri, strm).ConfigureAwait(false));
    }

    /// <summary>
    ///     Sets HTTP tracing On.Writes output to Console
    /// </summary>
    public void SetTraceOn(IRequestLogger requestLogger = null)
    {
        var logger = Config?.ServiceProvider?.GetRequiredService<ILogger<DefaultRequestLogger>>();
        RequestLogger = requestLogger ?? new DefaultRequestLogger(logger);
        Config.TraceHttp = true;
    }

    /// <summary>
    ///     Sets HTTP tracing Off.
    /// </summary>
    public void SetTraceOff()
    {
        Config.TraceHttp = false;
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Parse response errors if any and return relevant error messages
    /// </summary>
    /// <param name="response"></param>
    internal static void ParseError(ResponseResult response)
    {
        if (response is null)
            throw new ConnectionException(
                "Response is nil. Please report this issue https://github.com/newtera/newtera-dotnet/issues", response);

        if (HttpStatusCode.Redirect.Equals(response.StatusCode) ||
            HttpStatusCode.TemporaryRedirect.Equals(response.StatusCode) ||
            HttpStatusCode.MovedPermanently.Equals(response.StatusCode))
            throw new RedirectionException(
                "Redirection detected. Please report this issue https://github.com/newtera/newtera-dotnet/issues");

        if (string.IsNullOrWhiteSpace(response.Content))
        {
            ParseErrorNoContent(response);
            return;
        }

        ParseErrorFromContent(response);
    }

    private static void ParseErrorNoContent(ResponseResult response)
    {
        if (HttpStatusCode.Forbidden.Equals(response.StatusCode)
            || HttpStatusCode.BadRequest.Equals(response.StatusCode)
            || HttpStatusCode.NotFound.Equals(response.StatusCode)
            || HttpStatusCode.MethodNotAllowed.Equals(response.StatusCode)
            || HttpStatusCode.NotImplemented.Equals(response.StatusCode))
            ParseWellKnownErrorNoContent(response);

#pragma warning disable MA0099 // Use Explicit enum value instead of 0
        if (response.StatusCode == 0)
            throw new ConnectionException("Connection error:" + response.ErrorMessage, response);
#pragma warning restore MA0099 // Use Explicit enum value instead of 0
        throw new InternalClientException(
            "Unsuccessful response from server without XML:" + response.ErrorMessage, response);
    }

    private static void ParseWellKnownErrorNoContent(ResponseResult response)
    {
        NewteraException error = null;
        var errorResponse = new ErrorResponse();

        foreach (var parameter in response.Headers)
        {
            if (parameter.Key.Equals("x-amz-id-2", StringComparison.OrdinalIgnoreCase))
                errorResponse.HostId = parameter.Value;

            if (parameter.Key.Equals("x-amz-request-id", StringComparison.OrdinalIgnoreCase))
                errorResponse.RequestId = parameter.Value;

            if (parameter.Key.Equals("x-amz-bucket-region", StringComparison.OrdinalIgnoreCase))
                errorResponse.BucketRegion = parameter.Value;
        }

        var pathAndQuery = response.Request.RequestUri.PathAndQuery;
        var host = response.Request.RequestUri.Host;
        errorResponse.Resource = pathAndQuery;

        // zero, one or two segments
        var resourceSplits = pathAndQuery.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);

        if (HttpStatusCode.NotFound.Equals(response.StatusCode))
        {
            var pathLength = resourceSplits.Length;
            var isAWS = host.EndsWith("s3.amazonaws.com", StringComparison.OrdinalIgnoreCase);
            var isVirtual = isAWS && !host.StartsWith("s3.amazonaws.com", StringComparison.OrdinalIgnoreCase);

            if (pathLength > 1)
            {
                var objectName = resourceSplits[1];
                errorResponse.Code = "NoSuchKey";
                error = new ObjectNotFoundException(objectName, "Not found.");
            }
            else if (pathLength == 1)
            {
                var resource = resourceSplits[0];

                if (isAWS && isVirtual && !string.IsNullOrEmpty(pathAndQuery))
                {
                    errorResponse.Code = "NoSuchKey";
                    error = new ObjectNotFoundException(resource, "Not found.");
                }
                else
                {
                    errorResponse.Code = "NoSuchBucket";
                    error = new BucketNotFoundException(resource, "Not found.");
                }
            }
            else
            {
                error = new InternalClientException("404 without body resulted in path with less than two components",
                    response);
            }
        }
        else if (HttpStatusCode.BadRequest.Equals(response.StatusCode))
        {
            var pathLength = resourceSplits.Length;

            if (pathLength > 1)
            {
                var objectName = resourceSplits[1];
                errorResponse.Code = "InvalidObjectName";
                error = new InvalidObjectNameException(objectName, "Invalid object name.");
            }
            else
            {
                error = new InternalClientException("400 without body resulted in path with less than two components",
                    response);
            }
        }
        else if (HttpStatusCode.Forbidden.Equals(response.StatusCode))
        {
            errorResponse.Code = "Forbidden";
            error = new AccessDeniedException("Access denied on the resource: " + pathAndQuery);
        }

        error.Response = errorResponse;
        throw error;
    }

    private static void ParseErrorFromContent(ResponseResult response)
    {
        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.StatusCode.Equals(HttpStatusCode.NotFound)
            && response.Request.RequestUri.PathAndQuery.EndsWith("?location", StringComparison.OrdinalIgnoreCase)
            && response.Request.Method.Equals(HttpMethod.Get))
        {
            var bucketName = response.Request.RequestUri.PathAndQuery.Split('?')[0];
            throw new BucketNotFoundException(bucketName, "Not found.");
        }

        var errResponse = Utils.DeserializeXml<ErrorResponse>(response.Content);

        if (response.StatusCode.Equals(HttpStatusCode.Forbidden)
            && (errResponse.Code.Equals("SignatureDoesNotMatch", StringComparison.OrdinalIgnoreCase) ||
                errResponse.Code.Equals("InvalidAccessKeyId", StringComparison.OrdinalIgnoreCase)))
            throw new AuthorizationException(errResponse.Resource, errResponse.BucketName, errResponse.Message);

        // Handle XML response for Bucket Policy not found case
        if (response.StatusCode.Equals(HttpStatusCode.NotFound)
            && response.Request.RequestUri.PathAndQuery.EndsWith("?policy", StringComparison.OrdinalIgnoreCase)
            && response.Request.Method.Equals(HttpMethod.Get)
            && string.Equals(errResponse.Code, "NoSuchBucketPolicy", StringComparison.OrdinalIgnoreCase))
            throw new ErrorResponseException(errResponse, response) { XmlError = response.Content };

        if (response.StatusCode.Equals(HttpStatusCode.NotFound)
            && string.Equals(errResponse.Code, "NoSuchBucket", StringComparison.OrdinalIgnoreCase))
            throw new BucketNotFoundException(errResponse.BucketName, "Not found.");

        if (response.StatusCode.Equals(HttpStatusCode.BadRequest)
            && errResponse.Code.Equals("MalformedXML", StringComparison.OrdinalIgnoreCase))
            throw new MalFormedXMLException(errResponse.Resource, errResponse.BucketName, errResponse.Message,
                errResponse.Key);

        if (response.StatusCode.Equals(HttpStatusCode.NotImplemented)
            && errResponse.Code.Equals("NotImplemented", StringComparison.OrdinalIgnoreCase))
        {
#pragma warning disable MA0025 // Implement the functionality instead of throwing NotImplementedException
            throw new NotImplementedException(errResponse.Message);
        }
#pragma warning restore MA0025 // Implement the functionality instead of throwing NotImplementedException

        if (response.StatusCode.Equals(HttpStatusCode.Conflict)
            && errResponse.Code.Equals("BucketAlreadyOwnedByYou", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Bucket already owned by you: " + errResponse.BucketName,
                nameof(response));

        throw new UnexpectedNewteraException(errResponse.Message) { Response = errResponse, XmlError = response.Content };
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing && Config.DisposeHttpClient)
                Config.HttpClient?.Dispose();
            disposedValue = true;
        }
    }
}
