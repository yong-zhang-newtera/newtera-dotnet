/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 */

using Newtera.Helper;

namespace Newtera.DataModel.Args;

internal class NewMultipartUploadArgs<T> : ObjectWriteArgs<T>
    where T : NewMultipartUploadArgs<T>
{
    internal NewMultipartUploadArgs()
    {
        RequestMethod = HttpMethod.Post;
        RequestPath = "/api/blob/objects/";
    }

    internal DateTime RetentionUntilDate { get; set; }
    internal bool ObjectLockSet { get; set; }

    internal override HttpRequestMessageBuilder BuildRequest(HttpRequestMessageBuilder requestMessageBuilder)
    {
        requestMessageBuilder.AddQueryParameter("uploads", "");
        if (ObjectLockSet)
        {
            if (!RetentionUntilDate.Equals(default))
                requestMessageBuilder.AddOrUpdateHeaderParameter("x-amz-object-lock-retain-until-date",
                    Utils.To8601String(RetentionUntilDate));
        }

        requestMessageBuilder.AddOrUpdateHeaderParameter("content-type", ContentType);

        return requestMessageBuilder;
    }
}
