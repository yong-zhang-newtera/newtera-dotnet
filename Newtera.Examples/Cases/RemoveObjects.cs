/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using Newtera.DataModel.Args;

namespace Newtera.Examples.Cases;

internal static class RemoveObjects
{
    // Remove a list of objects from a bucket
    public static async Task Run(INewteraClient newtera,
        string bucketName = "my-bucket-name",
        List<string> objectsList = null,
        List<Tuple<string, string>> objectsVersionsList = null)
    {
        try
        {
            Console.WriteLine("Running example for API: RemoveObjectsAsync");
            if (objectsList is not null)
            {
                var objArgs = new RemoveObjectsArgs()
                    .WithBucket(bucketName)
                    .WithObjects(objectsList);
                var objectsOservable = await newtera.RemoveObjectsAsync(objArgs).ConfigureAwait(false);
                var objectsSubscription = objectsOservable.Subscribe(
                    objDeleteError => Console.WriteLine($"Object: {objDeleteError.Key}"),
                    ex => Console.WriteLine($"OnError: {ex}"),
                    () => Console.WriteLine($"Removed objects in list from {bucketName}\n"));
                return;
            }

            var objVersionsArgs = new RemoveObjectsArgs()
                .WithBucket(bucketName)
                .WithObjectsVersions(objectsVersionsList);
            var observable = await newtera.RemoveObjectsAsync(objVersionsArgs).ConfigureAwait(false);
            var subscription = observable.Subscribe(
                objVerDeleteError => Console.WriteLine($"Object: {objVerDeleteError.Key} " +
                                                       $"Object Version: {objVerDeleteError.VersionId}"),
                ex => Console.WriteLine($"OnError: {ex}"),
                () => Console.WriteLine($"Removed objects versions from {bucketName}\n"));
        }
        catch (Exception e)
        {
            Console.WriteLine($"[Bucket-Object]  Exception: {e}");
        }
    }
}
