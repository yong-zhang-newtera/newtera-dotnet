﻿/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2017-2021 Newtera, Inc.
 *
 */

using System.Net;
using System.Text;

namespace Newtera.DataModel.Result;

public sealed class ResponseResult : IDisposable
{
    private readonly Dictionary<string, string> headers = new(StringComparer.Ordinal);
    private string content;
    private ReadOnlyMemory<byte> contentBytes;
    private bool disposed;
    private Stream stream;

    public ResponseResult(HttpRequestMessage request, HttpResponseMessage response)
    {
        Request = request;
        Response = response;
    }

    public ResponseResult(HttpRequestMessage request, Exception exception)
        : this(request, response: null)
    {
        Exception = exception;
    }

    public Exception Exception { get; set; }
    public HttpRequestMessage Request { get; }
    public HttpResponseMessage Response { get; }

    public HttpStatusCode StatusCode
    {
        get
        {
#pragma warning disable MA0099 // Use Explicit enum value instead of 0
            if (Response is null) return 0;
#pragma warning restore MA0099 // Use Explicit enum value instead of 0

            return Response.StatusCode;
        }
    }

    public Stream ContentStream
    {
        get
        {
            if (Response is null) return null;
            return stream ??= Response.Content.ReadAsStream();
        }
    }

    public ReadOnlyMemory<byte> ContentBytes
    {
        get
        {
            if (ContentStream is null)
                return ReadOnlyMemory<byte>.Empty;

            if (contentBytes.IsEmpty)
            {
                using var memoryStream = new MemoryStream();
                ContentStream.CopyTo(memoryStream);
                contentBytes = new ReadOnlyMemory<byte>(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            }

            return contentBytes;
        }
    }

    public string Content
    {
        get
        {
            if (ContentBytes.Length == 0) return "";
            content ??= Encoding.UTF8.GetString(ContentBytes.Span);
            return content;
        }
    }

    public IDictionary<string, string> Headers
    {
        get
        {
            if (Response is null) return new Dictionary<string, string>(StringComparer.Ordinal);

            if (headers.Count == 0)
            {
                if (Response.Content is not null)
                    foreach (var item in Response.Content.Headers)
                        headers.Add(item.Key, item.Value.FirstOrDefault());

                foreach (var item in Response.Headers) headers.Add(item.Key, item.Value.FirstOrDefault());
            }

            return headers;
        }
    }

    public string ErrorMessage => Exception?.Message;

    public void Dispose()
    {
        if (disposed) return;

        stream?.Dispose();
        Request?.Dispose();
        Response?.Dispose();

        content = null;
        contentBytes = null;
        stream = null;

        disposed = true;
    }
}
