/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

using Newtera.Helper;

namespace Newtera.DataModel.Args;

public abstract class BucketArgs<T> : RequestArgs
    where T : BucketArgs<T>
{
    protected const string BucketForceDeleteKey = "X-Newtera-Force-Delete";

    public bool IsBucketCreationRequest { get; set; }

    internal string BucketName { get; set; }

    internal IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>(StringComparer.Ordinal);

    public T WithBucket(string bucket)
    {
        BucketName = bucket;
        return (T)this;
    }

    public virtual T WithHeaders(IDictionary<string, string> headers)
    {
        if (headers is null || headers.Count <= 0) return (T)this;
        Headers ??= new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (var key in headers.Keys)
        {
            _ = Headers.Remove(key);
            Headers[key] = headers[key];
        }

        return (T)this;
    }

    internal virtual void Validate()
    {
        Utils.ValidateBucketName(BucketName);
    }
}
