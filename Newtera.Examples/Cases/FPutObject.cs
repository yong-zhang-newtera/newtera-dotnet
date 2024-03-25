/*
 * Newtera .NET Library for Newtera TDM, (C) 2017-2021 Newtera, Inc.
 *
 */

using Newtera.DataModel.Args;

namespace Newtera.Examples.Cases;

internal static class FPutObject
{
    // Upload object to bucket from file
    public static async Task Run(INewteraClient newtera,
        string bucketName = "my-bucket-name",
        string prefix = null,
        string objectName = "my-object-name",
        string fileName = "from where")
    {
        try
        {
            Console.WriteLine("Running example for API: PutObjectAsync with FileName");
            var metaData = new Dictionary<string, string>
                (StringComparer.Ordinal) { { "user", newtera.Config.AccessKey } };
            var args = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithPrefix(prefix)
                .WithObject(objectName)
                .WithContentType("application/octet-stream")
                .WithFileName(fileName)
                .WithHeaders(metaData);
            _ = await newtera.PutObjectAsync(args).ConfigureAwait(false);

            File.Delete(fileName);

            Console.WriteLine($"Uploaded object {objectName} to bucket {bucketName}");
            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine($"[Bucket]  Exception: {e}");
        }
    }
}
