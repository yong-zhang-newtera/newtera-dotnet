﻿/*
 * Newtera .NET Library for Newtera TDM, (C) 2017-2021 Newtera, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
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
                (StringComparer.Ordinal) { { "user", "tdm" } };
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
