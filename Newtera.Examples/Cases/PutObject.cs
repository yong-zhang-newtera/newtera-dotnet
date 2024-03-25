/*
 * Newtera .NET Library for Newtera TDM, (C) 2017-2021 Newtera, Inc.
 *
 */

using CommunityToolkit.HighPerformance;
using Newtera.DataModel;
using Newtera.DataModel.Args;

namespace Newtera.Examples.Cases;

internal static class PutObject
{
    private const int MB = 1024 * 1024;

    // Put an object from a local stream into bucket
    public static async Task Run(INewteraClient newtera,
        string bucketName = "my-bucket-name",
        string prefix = null,
        string objectName = "my-object-name",
        string fileName = "location-of-file",
        IProgress<ProgressReport> progress = null)
    {
        try
        {
            ReadOnlyMemory<byte> bs = await File.ReadAllBytesAsync(fileName).ConfigureAwait(false);
            Console.WriteLine("Running example for API: PutObjectAsync");
            using var filestream = bs.AsStream();

            var fileInfo = new FileInfo(fileName);
            var metaData = new Dictionary<string, string>
                (StringComparer.Ordinal) { { "user", newtera.Config.AccessKey } };
            var args = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithPrefix(prefix)
                .WithStreamData(filestream)
                .WithObjectSize(filestream.Length)
                .WithContentType("application/octet-stream")
                .WithHeaders(metaData)
                .WithProgress(progress);
            _ = await newtera.PutObjectAsync(args).ConfigureAwait(false);

            Console.WriteLine($"Uploaded object {objectName} to bucket {bucketName}");
            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine($"[Bucket]  Exception: {e}");
        }
    }
}
