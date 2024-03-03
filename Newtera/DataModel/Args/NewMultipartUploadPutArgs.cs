/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

namespace Newtera.DataModel.Args;

internal class NewMultipartUploadPutArgs : NewMultipartUploadArgs<NewMultipartUploadPutArgs>
{
    internal override HttpRequestMessageBuilder BuildRequest(HttpRequestMessageBuilder requestMessageBuilder)
    {
        requestMessageBuilder.AddQueryParameter("uploads", "");
        requestMessageBuilder.AddQueryParameter("prefix", Prefix);

        requestMessageBuilder.AddOrUpdateHeaderParameter("content-type", ContentType);

        return requestMessageBuilder;
    }
}
