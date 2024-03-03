/*
 * Newtera .NET Library for Newtera TDM, (C) 2020 Newtera, Inc.
 *
 */

using Newtera.Helper;

namespace Newtera.DataModel.Args;

public abstract class ObjectArgs<T> : BucketArgs<T>
    where T : ObjectArgs<T>
{
    protected const string S3ZipExtractKey = "X-Newtera-Extract";

    internal string ObjectName { get; set; }
    internal ReadOnlyMemory<byte> RequestBody { get; set; }

    internal string Prefix { get; private set; }

    public T WithObject(string obj)
    {
        ObjectName = obj;
        return (T)this;
    }

    public T WithPrefix(string prefix)
    {
        Prefix = prefix ?? string.Empty;
        return (T) this;
    }

    public T WithRequestBody(ReadOnlyMemory<byte> data)
    {
        RequestBody = data;
        return (T)this;
    }

    internal override void Validate()
    {
        base.Validate();
        Utils.ValidateObjectName(ObjectName);
    }
}
