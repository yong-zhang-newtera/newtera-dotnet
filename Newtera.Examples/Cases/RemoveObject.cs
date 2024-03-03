/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using Newtera.DataModel.Args;

namespace Newtera.Examples.Cases;

internal static class RemoveObject
{
    // Remove an object from a bucket
    public static async Task Run(INewteraClient newtera,
        string bucketName = "my-bucket-name",
        string prefix = "my-prefix",
        string objectName = "my-object-name")
    {
        if (newtera is null) throw new ArgumentNullException(nameof(newtera));

        try
        {
            var args = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithPrefix(prefix)
                .WithObject(objectName);

            Console.WriteLine("Running example for API: RemoveObjectAsync");
            await newtera.RemoveObjectAsync(args).ConfigureAwait(false);
            Console.WriteLine($"Removed object {objectName} from bucket {bucketName} successfully");
            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine($"[Bucket-Object]  Exception: {e}");
        }
    }
}
