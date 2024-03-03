/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Net;
using Newtera;
using Newtera.DataModel.Args;

namespace SimpleTest;

public static class Program
{
    private static async Task Main()
    {
        // Note: s3 AccessKey and SecretKey needs to be added in App.config file
        // See instructions in README.md on running examples for more information.
        using var newtera = new NewteraClient()
            .WithEndpoint("localhost:8080")
            .WithCredentials("demo1",
                "888")
            .Build();

        //Supply a new bucket name
        var bucketName = "tdm";

        var found = await IsBucketExists(newtera, bucketName).ConfigureAwait(false);
        Console.WriteLine("Bucket exists? = " + found);
        _ = Console.ReadLine();
    }

    private static Task<bool> IsBucketExists(INewteraClient newtera, string bucketName)
    {
        var bktExistsArgs = new BucketExistsArgs().WithBucket(bucketName);
        return newtera.BucketExistsAsync(bktExistsArgs);
    }
}
