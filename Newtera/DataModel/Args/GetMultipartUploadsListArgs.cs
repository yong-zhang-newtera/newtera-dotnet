/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

using System.Globalization;

namespace Newtera.DataModel.Args;

public class GetMultipartUploadsListArgs : BucketArgs<GetMultipartUploadsListArgs>
{
    public GetMultipartUploadsListArgs()
    {
        RequestMethod = HttpMethod.Get;
        MAX_UPLOAD_COUNT = 1000;
    }

    internal string Prefix { get; private set; }
    internal string Delimiter { get; private set; }
    internal string KeyMarker { get; private set; }
    internal string UploadIdMarker { get; private set; }
    internal uint MAX_UPLOAD_COUNT { get; }

    public GetMultipartUploadsListArgs WithPrefix(string prefix)
    {
        Prefix = prefix ?? string.Empty;
        return this;
    }

    public GetMultipartUploadsListArgs WithDelimiter(string delim)
    {
        Delimiter = delim ?? string.Empty;
        return this;
    }

    public GetMultipartUploadsListArgs WithKeyMarker(string nextKeyMarker)
    {
        KeyMarker = nextKeyMarker ?? string.Empty;
        return this;
    }

    public GetMultipartUploadsListArgs WithUploadIdMarker(string nextUploadIdMarker)
    {
        UploadIdMarker = nextUploadIdMarker ?? string.Empty;
        return this;
    }

    internal override HttpRequestMessageBuilder BuildRequest(HttpRequestMessageBuilder requestMessageBuilder)
    {
        requestMessageBuilder.AddQueryParameter("uploads", "");
        requestMessageBuilder.AddQueryParameter("prefix", Prefix);
        requestMessageBuilder.AddQueryParameter("delimiter", Delimiter);
        requestMessageBuilder.AddQueryParameter("key-marker", KeyMarker);
        requestMessageBuilder.AddQueryParameter("upload-id-marker", UploadIdMarker);
        requestMessageBuilder.AddQueryParameter("max-uploads", MAX_UPLOAD_COUNT.ToString(CultureInfo.InvariantCulture));
        return requestMessageBuilder;
    }
}
