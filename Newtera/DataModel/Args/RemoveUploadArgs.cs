/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

namespace Newtera.DataModel.Args;

public class RemoveUploadArgs : ObjectArgs<RemoveUploadArgs>
{
    public RemoveUploadArgs()
    {
        RequestMethod = HttpMethod.Delete;
    }

    internal string UploadId { get; private set; }

    public RemoveUploadArgs WithUploadId(string id)
    {
        UploadId = id;
        return this;
    }

    internal override void Validate()
    {
        base.Validate();
        if (string.IsNullOrEmpty(UploadId))
            throw new InvalidOperationException(nameof(UploadId) +
                                                " cannot be empty. Please assign a valid upload ID to remove.");
    }

    internal override HttpRequestMessageBuilder BuildRequest(HttpRequestMessageBuilder requestMessageBuilder)
    {
        requestMessageBuilder.AddQueryParameter("uploadId", $"{UploadId}");
        return requestMessageBuilder;
    }
}
