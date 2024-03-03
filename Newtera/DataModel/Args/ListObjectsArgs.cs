/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

namespace Newtera.DataModel.Args;

public class ListObjectsArgs : BucketArgs<ListObjectsArgs>
{
    public ListObjectsArgs()
    {
        RequestPath = "/api/blob/objects/";
    }

    internal string Prefix { get; private set; }
    internal bool Recursive { get; private set; }

    public ListObjectsArgs WithPrefix(string prefix)
    {
        Prefix = prefix;
        return this;
    }

    public ListObjectsArgs WithRecursive(bool rec)
    {
        Recursive = rec;
        return this;
    }
}
