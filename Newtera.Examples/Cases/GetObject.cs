/*
 * Newtera .NET Library for Newtera TDM, (C) 2017-2020 Newtera, Inc.
 *
 */

using Newtera.DataModel.Args;

namespace Newtera.Examples.Cases;

internal static class GetObject
{
    // Get object in a bucket
    public static async Task Run(INewteraClient newtera,
        string bucketName = "my-bucket-name",
        string prefix = null,
        string objectName = "my-object-name",
        string fileName = "my-file-name")
    {
        try
        {
            Console.WriteLine("Running example for API: GetObjectAsync");
            var args = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithPrefix(prefix)
                .WithObject(objectName)
                .WithFile(fileName);
            var stat = await newtera.GetObjectAsync(args).ConfigureAwait(false);
            Console.WriteLine($"Downloaded the file {fileName} in bucket {bucketName}");
            Console.WriteLine($"Stat details of object {objectName} in bucket {bucketName}\n" + stat);
            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine($"[Bucket]  Exception: {e}");
        }
    }
}
