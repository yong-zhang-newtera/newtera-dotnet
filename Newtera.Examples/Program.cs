/*
 * Newtera .NET Library for Newtera TDM, (C) 2017, 2020 Newtera, Inc.
 *
 */

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtera.DataModel;
using Newtera.Examples.Cases;

namespace Newtera.Examples;

public static class Program
{
    private const int UNIT_MB = 1024 * 1024;
    private static readonly Random rnd = new();

    // Create a file of given size from random byte array
    private static string CreateFile(int size)
    {
        var fileName = GetRandomName();
        var data = new byte[size];
        rnd.NextBytes(data);

        File.WriteAllBytes(fileName, data);

        return fileName;
    }

    // Generate a random string
    public static string GetRandomName()
    {
        var characters = "0123456789abcdefghijklmnopqrstuvwxyz";
        var result = new StringBuilder(5);
        for (var i = 0; i < 5; i++) _ = result.Append(characters[rnd.Next(characters.Length)]);
        return "newtera-tdm-" + result;
    }

    [SuppressMessage("Design", "MA0051:Method is too long", Justification = "Needs to run all tests")]
    public static async Task Main()
    {
        var endPoint = "localhost";
        var accessKey = "demo1"; // with Administrator as a role
        var secretKey = "888";
        var isSecure = false;
        var port = 8080;

        using var newteraClient = new NewteraClient()
            .WithEndpoint(endPoint, port)
            .WithCredentials(accessKey, secretKey)
            .WithSSL(isSecure)
            .Build();

        // Assign parameters before starting the test 
        var bucketName = "tdm";
        var smallFileName = CreateFile(1 * UNIT_MB);
        var bigFileName = CreateFile(6 * UNIT_MB);
        var objectName = GetRandomName();
        var prefix = @"Task-20230930-0023\慢充功能测试\电池循环充放电数据";
        var progress = new Progress<ProgressReport>(progressReport =>
        {
            Console.WriteLine(
                $"Percentage: {progressReport.Percentage}% TotalBytesTransferred: {progressReport.TotalBytesTransferred} bytes");
            if (progressReport.Percentage != 100)
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            else Console.WriteLine();
        });
        var objectsList = new List<string>();
        for (var i = 0; i < 10; i++) objectsList.Add(objectName + i);

        // Set HTTP Tracing On
        //newteraClient.SetTraceOn();

        // Set HTTP Tracing Off
        // newteraClient.SetTraceOff();
        // Check if bucket exists
        await BucketExists.Run(newteraClient, bucketName).ConfigureAwait(false);

        // List the objects in the new bucket
        ListObjects.Run(newteraClient, bucketName);

        // Put an object to the new bucket
        await PutObject.Run(newteraClient, bucketName, prefix, objectName, smallFileName, progress).ConfigureAwait(false);

        // Get the file and Download the object as file
        await GetObject.Run(newteraClient, bucketName, prefix, objectName, smallFileName).ConfigureAwait(false);

        await FPutObject.Run(newteraClient, bucketName, prefix, objectName, smallFileName).ConfigureAwait(false);

        await FGetObject.Run(newteraClient, bucketName, prefix, objectName, smallFileName).ConfigureAwait(false);

        // Delete the object
        await RemoveObject.Run(newteraClient, bucketName, prefix, objectName).ConfigureAwait(false);

        // Automatic Multipart Upload with object more than 5Mb
        await PutObject.Run(newteraClient, bucketName, prefix, objectName, bigFileName, progress).ConfigureAwait(false);

        // Delete the object
        await RemoveObject.Run(newteraClient, bucketName, prefix, objectName).ConfigureAwait(false);

        // Remove the binary files created for test
        File.Delete(smallFileName);
        File.Delete(bigFileName);

        if (OperatingSystem.IsWindows()) _ = Console.ReadLine();
    }
}
