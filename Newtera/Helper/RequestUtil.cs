﻿/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using Newtera.Exceptions;

namespace Newtera.Helper;

internal static class RequestUtil
{
    internal static Uri GetEndpointURL(string endPoint, bool secure)
    {
        var uri = TryCreateUri(endPoint, secure);
        ValidateEndpoint(uri, endPoint);
        return uri;
    }

    internal static Uri MakeTargetURL(string endPoint, bool secure, string requestPath = null, string bucketName = null)
    {
        var host = endPoint;

        if (!string.IsNullOrEmpty(requestPath))
        {
            host = Combine(host, requestPath);
        }

        if (!string.IsNullOrEmpty(bucketName))
        {
            host = Combine(host, bucketName);
        }

        var scheme = secure ? "https" : "http";
        var endpointURL = string.Format(CultureInfo.InvariantCulture, "{0}://{1}", scheme, host);
        return new Uri(endpointURL, UriKind.Absolute);
    }

    internal static Uri TryCreateUri(string endpoint, bool secure)
    {
        var scheme = secure ? HttpUtility.UrlEncode("https") : HttpUtility.UrlEncode("http");

        // This is the actual url pointed to for all HTTP requests
        var endpointURL = string.Format(CultureInfo.InvariantCulture, "{0}://{1}", scheme, endpoint);
        Uri uri;
        try
        {
            uri = new Uri(endpointURL);
        }
        catch (UriFormatException e)
        {
            throw new InvalidEndpointException(e.Message);
        }

        return uri;
    }

    /// <summary>
    ///     Validates URI to check if it is well formed. Otherwise cry foul.
    /// </summary>
    internal static void ValidateEndpoint(Uri uri, string endpoint)
    {
        if (string.IsNullOrEmpty(uri.OriginalString)) throw new InvalidEndpointException("Endpoint cannot be empty.");

        if (!IsValidEndpoint(uri.Host)) throw new InvalidEndpointException(endpoint, "Invalid endpoint.");
        if (!uri.AbsolutePath.Equals("/", StringComparison.OrdinalIgnoreCase))
            throw new InvalidEndpointException(endpoint, "No path allowed in endpoint.");

        if (!string.IsNullOrEmpty(uri.Query))
            throw new InvalidEndpointException(endpoint, "No query parameter allowed in endpoint.");
        if (!uri.Scheme.ToUpperInvariant().Equals("https", StringComparison.OrdinalIgnoreCase) &&
            !uri.Scheme.ToUpperInvariant().Equals("http", StringComparison.OrdinalIgnoreCase))
            throw new InvalidEndpointException(endpoint, "Invalid scheme detected in endpoint.");
    }

    /// <summary>
    ///     Validate Url endpoint
    /// </summary>
    /// <param name="endpoint"></param>
    /// <returns>true/false</returns>
    internal static bool IsValidEndpoint(string endpoint)
    {
        // endpoint may be a hostname
        // refer https://en.wikipedia.org/wiki/Hostname#Restrictions_on_valid_host_names
        // why checks are as shown below.
        if (endpoint.Length is < 1 or > 253) return false;

        foreach (var label in endpoint.Split('.'))
        {
            if (label.Length is < 1 or > 63) return false;

            var validLabel = new Regex("^[a-zA-Z0-9]([A-Za-z0-9-_]*[a-zA-Z0-9])?$", RegexOptions.ExplicitCapture,
                TimeSpan.FromHours(1));

            if (!validLabel.IsMatch(label)) return false;
        }

        return true;
    }

    internal static string Combine(string uri1, string uri2)
    {
        uri1 = uri1.TrimEnd('/');
        uri2 = uri2.TrimStart('/');
        return string.Format("{0}/{1}", uri1, uri2);
    }
}
