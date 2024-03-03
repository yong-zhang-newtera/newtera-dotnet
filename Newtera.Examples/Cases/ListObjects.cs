/*
 * Newtera .NET Library for Newtera TDM, (C) 2017-2021 Newtera, Inc.
 *
 */

using Newtera.DataModel.Args;

namespace Newtera.Examples.Cases;

internal static class ListObjects
{
    // List objects matching optional prefix in a specified bucket.
    public static void Run(INewteraClient newtera,
        string bucketName = "my-bucket-name",
        string prefix = @"Task-20230930-0023\慢充功能测试\电池循环充放电数据",
        bool recursive = false)
    {
        try
        {
            Console.WriteLine("Running example for API: ListObjectsAsync");
            var listArgs = new ListObjectsArgs()
                .WithBucket(bucketName)
                .WithPrefix(prefix)
                .WithRecursive(recursive);
            var observable = newtera.ListObjectsAsync(listArgs);
            var subscription = observable.Subscribe(
                item => Console.WriteLine($"Object: {item.Key}"),
                ex => Console.WriteLine($"OnError: {ex}"),
                () => Console.WriteLine($"Listed all objects in bucket {bucketName}\n"));
        }
        catch (Exception e)
        {
            Console.WriteLine($"[Bucket]  Exception: {e}");
        }
    }
}
