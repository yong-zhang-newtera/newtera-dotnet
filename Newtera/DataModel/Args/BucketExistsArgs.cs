/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

namespace Newtera.DataModel.Args;

public class BucketExistsArgs : BucketArgs<BucketExistsArgs>
{
    public BucketExistsArgs()
    {
        RequestMethod = HttpMethod.Head;
        RequestPath = "/api/blob/buckets/";
    }
}
