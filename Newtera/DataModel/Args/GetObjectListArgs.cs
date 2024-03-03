/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

namespace Newtera.DataModel.Args;

internal class GetObjectListArgs : BucketArgs<GetObjectListArgs>
{
    public GetObjectListArgs()
    {
        RequestMethod = HttpMethod.Get;
        RequestPath = "/api/blob/objects/";
        Prefix = string.Empty;
    }

    internal string Prefix { get; private set; }
    internal string ContinuationToken { get; set; }

    public GetObjectListArgs WithPrefix(string prefix)
    {
        Prefix = prefix ?? string.Empty;
        return this;
    }

    public GetObjectListArgs WithContinuationToken(string token)
    {
        ContinuationToken = string.IsNullOrWhiteSpace(token) ? string.Empty : token;
        return this;
    }

    internal override HttpRequestMessageBuilder BuildRequest(HttpRequestMessageBuilder requestMessageBuilder)
    {
        foreach (var h in Headers)
            requestMessageBuilder.AddOrUpdateHeaderParameter(h.Key, h.Value);

        requestMessageBuilder.AddQueryParameter("max-keys", "1000");
        requestMessageBuilder.AddQueryParameter("prefix", Prefix);

        return requestMessageBuilder;
    }
}
