/*
 * Newtera .NET Library for Newtera TDM, (C) 2017-2021 Newtera, Inc.
 *
 */

using Newtera.DataModel.Args;

namespace Newtera.Examples.Cases;

internal static class FGetObject
{
    // Download object from bucket into local file
    public static async Task Run(INewteraClient newtera,
        string bucketName = "my-bucket-name",
        string prefix = "my-prefix",
        string objectName = "my-object-name",
        string fileName = "local-filename")
    {
        try
        {
            Console.WriteLine("Running example for API: GetObjectAsync");
            File.Delete(fileName);
            var args = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithPrefix(prefix)
                .WithObject(objectName)
                .WithFile(fileName);
            _ = await newtera.GetObjectAsync(args).ConfigureAwait(false);
            Console.WriteLine($"Downloaded the file {fileName} from bucket {bucketName}");
            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine($"[Bucket]  Exception: {e}");
        }
    }
}
