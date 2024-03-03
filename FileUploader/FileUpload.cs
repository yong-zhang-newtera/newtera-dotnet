/*
 * Newtera .NET Library for Newtera TDM, (C) 2017-2021 Newtera, Inc.
 *
 */

using System.Net;
using Newtera;
using Newtera.DataModel.Args;

namespace FileUploader;

/// <summary>
///     This example creates a new bucket if it does not already exist, and
///     uploads a file to the bucket. The file name is chosen to be
///     "C:\\Users\\vagrant\\Downloads\\golden_oldies.mp3"
///     Either create a file with this name or change it with your own file,
///     where it is defined down below.
/// </summary>
public static class FileUpload
{
    private static bool IsWindows()
    {
        return OperatingSystem.IsWindows();
    }

    private static async Task Main()
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                                               | SecurityProtocolType.Tls11
                                               | SecurityProtocolType.Tls12;
        var endpoint = "play.min.io";
        var accessKey = "Q3AM3UQ867SPQQA43P2F";
        var secretKey = "zuf+tfteSlswRu7BJ86wekitnifILbZam1KYY3TG";

        try
        {
            using var newtera = new NewteraClient()
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey)
                .WithSSL()
                .Build();
            await Run(newtera).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        if (IsWindows()) _ = Console.ReadLine();
    }

    /// <summary>
    ///     Task that uploads a file to a bucket
    /// </summary>
    /// <param name="newtera"></param>
    /// <returns></returns>
    private static async Task Run(INewteraClient newtera)
    {
        // Make a new bucket called mymusic.
        var bucketName = "tdm"; //<==== change this
        // Upload the zip file
        var objectName = "my-golden-oldies.mp3";
        // The following is a source file that needs to be created in
        // your local filesystem.
        var filePath = "C:\\Users\\vagrant\\Downloads\\golden_oldies.mp3";
        var contentType = "application/zip";

        try
        {
            var bktExistArgs = new BucketExistsArgs()
                .WithBucket(bucketName);
            var found = await newtera.BucketExistsAsync(bktExistArgs).ConfigureAwait(false);
            if (found)
            {
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithFileName(filePath)
                    .WithContentType(contentType);
                _ = await newtera.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
                Console.WriteLine($"\nSuccessfully uploaded {objectName}\n");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        // Added for Windows folks. Without it, the window, tests
        // run in, dissappears as soon as the test code completes.
        if (IsWindows()) _ = Console.ReadLine();
    }
}
