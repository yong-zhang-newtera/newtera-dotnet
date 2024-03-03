/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

using Newtera.Helper;

namespace Newtera.DataModel.Args;

internal class PutObjectPartArgs : PutObjectArgs
{
    public PutObjectPartArgs()
    {
        RequestMethod = HttpMethod.Put;
    }

    internal override void Validate()
    {
        base.Validate();
        if (string.IsNullOrWhiteSpace(UploadId))
            throw new InvalidOperationException(nameof(UploadId) + " not assigned for PutObjectPart operation.");
    }

    public new PutObjectPartArgs WithBucket(string bkt)
    {
        return (PutObjectPartArgs)base.WithBucket(bkt);
    }

    public new PutObjectPartArgs WithObject(string obj)
    {
        return (PutObjectPartArgs)base.WithObject(obj);
    }

    public new PutObjectPartArgs WithObjectSize(long size)
    {
        return (PutObjectPartArgs)base.WithObjectSize(size);
    }

    public new PutObjectPartArgs WithHeaders(IDictionary<string, string> hdr)
    {
        return (PutObjectPartArgs)base.WithHeaders(hdr);
    }

    public PutObjectPartArgs WithRequestBody(object data)
    {
        return (PutObjectPartArgs)base.WithRequestBody(Utils.ObjectToByteArray(data));
    }

    public new PutObjectPartArgs WithStreamData(Stream data)
    {
        return (PutObjectPartArgs)base.WithStreamData(data);
    }

    public new PutObjectPartArgs WithContentType(string type)
    {
        return (PutObjectPartArgs)base.WithContentType(type);
    }

    public new PutObjectPartArgs WithUploadId(string id)
    {
        return (PutObjectPartArgs)base.WithUploadId(id);
    }

    public new PutObjectPartArgs WithProgress(IProgress<ProgressReport> progress)
    {
        return (PutObjectPartArgs)base.WithProgress(progress);
    }

    internal override HttpRequestMessageBuilder BuildRequest(HttpRequestMessageBuilder requestMessageBuilder)
    {
        return requestMessageBuilder;
    }
}
