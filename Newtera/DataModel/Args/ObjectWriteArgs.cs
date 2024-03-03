/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */


namespace Newtera.DataModel.Args;

public abstract class ObjectWriteArgs<T> : ObjectConditionalQueryArgs<T>
    where T : ObjectWriteArgs<T>
{
    internal string ContentType { get; set; }

    public T WithContentType(string type)
    {
        ContentType = string.IsNullOrWhiteSpace(type) ? "application/octet-stream" : type;
        if (!Headers.ContainsKey("Content-Type")) Headers["Content-Type"] = type;
        return (T)this;
    }
}
