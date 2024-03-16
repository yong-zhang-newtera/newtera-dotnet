/*
 * Newtera .NET Library for Newtera TDM, (C) 2017-2021 Newtera, Inc.
 *
 */

using Newtera.DataModel.Args;

namespace Newtera.Examples.Cases;

internal static class ListObjects
{
    // List objects matching optional prefix in a specified bucket.
    public static async Task Run(INewteraClient newtera,
        string bucketName = "my-bucket-name",
        string prefix = "",
        bool recursive = false)
    {
        try
        {
            Console.WriteLine("Running example for API: ListObjectsAsync");
            var listArgs = new ListObjectsArgs()
                .WithBucket(bucketName)
                .WithPrefix(prefix)
                .WithRecursive(recursive);
            var objectList = await newtera.ListObjectsAsync(listArgs).ConfigureAwait(false);
            foreach (var obj in objectList)
            {
                Console.WriteLine(obj.Name);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"[Bucket]  Exception: {e}");
        }
    }
}
