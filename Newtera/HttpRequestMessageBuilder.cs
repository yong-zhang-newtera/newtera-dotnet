/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2017, 2018, 2019, 2020 Newtera, Inc.
 *
 */

using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Newtera.Exceptions;

namespace Newtera;

internal class HttpRequestMessageBuilder
{
    internal HttpRequestMessageBuilder(Uri requestUri, HttpMethod method)
    {
        RequestUri = requestUri;
        Method = method;
    }

    public HttpRequestMessageBuilder(HttpMethod method, Uri host, string path)
        : this(method, new UriBuilder(host) { Path = Path.Combine(host.AbsolutePath, path) }.Uri)
    {
    }

    public HttpRequestMessageBuilder(HttpMethod method, string requestUrl)
        : this(method, new Uri(requestUrl))
    {
    }

    public HttpRequestMessageBuilder(HttpMethod method, Uri requestUri)
    {
        Method = method;
        RequestUri = requestUri;

        QueryParameters = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        HeaderParameters = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        BodyParameters = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
    }

    public Uri RequestUri { get; set; }
    public Func<Stream, CancellationToken, Task> ResponseWriter { get; set; }
    public HttpMethod Method { get; }

    public HttpRequestMessage Request
    {
        get
        {
            var requestUriBuilder = new UriBuilder(RequestUri);
            if (QueryParameters?.Count > 0)
            {
                var query = HttpUtility.ParseQueryString(requestUriBuilder.Query);
                foreach (var queryParameter in QueryParameters)
                {
                    query[queryParameter.Key] = queryParameter.Value;
                }
                requestUriBuilder.Query = query.ToString();
            }

            var requestUri = requestUriBuilder.Uri;
            var request = new HttpRequestMessage(Method, requestUri);

            if (!Content.IsEmpty) request.Content = new ReadOnlyMemoryContent(Content);

            foreach (var parameter in HeaderParameters)
            {
                var key = parameter.Key.ToLowerInvariant();
                var val = parameter.Value;

                var addSuccess = request.Headers.TryAddWithoutValidation(key, val);
                if (!addSuccess)
                {
                    request.Content ??= new StringContent("");
                    switch (key)
                    {
                        case "content-type":
                            try
                            {
                                request.Content.Headers.ContentType = new MediaTypeHeaderValue(val);
                            }
                            catch
                            {
                                var success = request.Content.Headers.TryAddWithoutValidation(ContentTypeKey, val);
                            }

                            break;

                        case "content-length":
                            request.Content.Headers.ContentLength = Convert.ToInt32(val, CultureInfo.InvariantCulture);
                            break;

                        default:
                            var errMessage = "Unsupported signed header: (" + key + ": " + val;
                            throw new UnexpectedNewteraException(errMessage);
                    }
                }
            }

            if (request.Content is not null)
            {
                var isMultiDeleteRequest = false;
                if (Method == HttpMethod.Post) isMultiDeleteRequest = QueryParameters.ContainsKey("delete");
                var isSecure = RequestUri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase);

                if (!isSecure && !isMultiDeleteRequest &&
                    BodyParameters.TryGetValue("Content-Md5", out var value) && value is not null)
                {
                    _ = BodyParameters.TryGetValue("Content-Md5", out var returnValue);
                    request.Content.Headers.ContentMD5 = Convert.FromBase64String(returnValue);
                }
            }

            return request;
        }
    }

    public Dictionary<string, string> QueryParameters { get; }

    public Dictionary<string, string> HeaderParameters { get; }

    public Dictionary<string, string> BodyParameters { get; }

    public ReadOnlyMemory<byte> Content { get; private set; }

    public string ContentTypeKey => "Content-Type";

    public void AddHeaderParameter(string key, string value)
    {
        if (key.StartsWith("content-", StringComparison.InvariantCultureIgnoreCase) &&
            !string.IsNullOrEmpty(value) &&
            !BodyParameters.ContainsKey(key))
            BodyParameters.Add(key, value);

        HeaderParameters[key] = value;
    }

    public void AddOrUpdateHeaderParameter(string key, string value)
    {
        if (HeaderParameters.GetType().GetProperty(key) is not null)
            _ = HeaderParameters.Remove(key);
        HeaderParameters[key] = value;
    }

    public void AddBodyParameter(string key, string value)
    {
        BodyParameters.Add(key, value);
    }

    public void AddQueryParameter(string key, string value)
    {
        QueryParameters[key] = value;
    }

    public void SetBody(ReadOnlyMemory<byte> body)
    {
        Content = body;
    }

    public void AddXmlBody(string body)
    {
        SetBody(Encoding.UTF8.GetBytes(body));
        BodyParameters.Add(ContentTypeKey, "application/xml");
    }

    public void AddJsonBody(string body)
    {
        SetBody(Encoding.UTF8.GetBytes(body));
        BodyParameters.Add(ContentTypeKey, "application/json");
    }
}
