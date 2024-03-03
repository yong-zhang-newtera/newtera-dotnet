/*
 * Newtera .NET Library for Newtera TDM, (C) 2017-2021 Newtera, Inc.
 *
 */

using Newtera.DataModel.Args;

namespace Newtera.Examples.Cases;

internal static class BucketExists
{
    // Check if a bucket exists
    public static async Task Run(INewteraClient newtera,
        string bucketName = "my-bucket-name")
    {
        try
        {
            Console.WriteLine("Running example for API: BucketExistsAsync");
            var args = new BucketExistsArgs()
                .WithBucket(bucketName);
            var found = await newtera.BucketExistsAsync(args).ConfigureAwait(false);
            Console.WriteLine((found ? "Found" : "Couldn't find ") + "bucket " + bucketName);
            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine($"[Bucket]  Exception: {e}");
        }
    }
}
