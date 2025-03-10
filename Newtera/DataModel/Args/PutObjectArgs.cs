﻿/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

using System.Security.Cryptography;
using Newtera.Helper;

namespace Newtera.DataModel.Args;

public class PutObjectArgs : ObjectWriteArgs<PutObjectArgs>
{
    public PutObjectArgs()
    {
        RequestMethod = HttpMethod.Put;
        RequestPath = "/api/blob/objects/";
        RequestBody = null;
        ObjectStreamData = null;
        User = string.Empty;
        PartNumber = 0;
        ContentType = "application/octet-stream";
    }

    internal PutObjectArgs(PutObjectPartArgs args)
    {
        RequestMethod = HttpMethod.Put;
        RequestPath = "/api/blob/objects/";
        BucketName = args.BucketName;
        ContentType = args.ContentType ?? "application/octet-stream";
        User = string.Empty;
        FileName = args.FileName;
        Headers = args.Headers;
        ObjectName = args.ObjectName;
        ObjectSize = args.ObjectSize;
        PartNumber = args.PartNumber;
        UploadId = args.UploadId;
    }

    internal string User { get; private set; }
    internal string UploadId { get; private set; }
    internal int PartNumber { get; set; }
    internal string FileName { get; set; }
    internal long ObjectSize { get; set; }
    internal Stream ObjectStreamData { get; set; }
    internal IProgress<ProgressReport> Progress { get; set; }

    internal override void Validate()
    {
        base.Validate();
        // Check atleast one of filename or stream are initialized
        if (string.IsNullOrWhiteSpace(FileName) && ObjectStreamData is null)
            throw new InvalidOperationException("One of " + nameof(FileName) + " or " + nameof(ObjectStreamData) +
                                                " must be set.");

        if (PartNumber < 0)
            throw new InvalidDataException("Invalid Part number value. Cannot be less than 0");
        // Check if only one of filename or stream are initialized
        if (!string.IsNullOrWhiteSpace(FileName) && ObjectStreamData is not null)
            throw new InvalidOperationException("Only one of " + nameof(FileName) + " or " + nameof(ObjectStreamData) +
                                                " should be set.");

        if (!string.IsNullOrWhiteSpace(FileName)) Utils.ValidateFile(FileName);
        // Check object size when using stream data
        if (ObjectStreamData is not null && ObjectSize == 0)
            throw new InvalidOperationException($"{nameof(ObjectSize)} must be set");
        Populate();
    }

    private void Populate()
    {
        if (!string.IsNullOrWhiteSpace(FileName))
        {
            var fileInfo = new FileInfo(FileName);
            ObjectSize = fileInfo.Length;
            ObjectStreamData = new FileStream(FileName, FileMode.Open, FileAccess.Read);
        }
    }

    internal override HttpRequestMessageBuilder BuildRequest(HttpRequestMessageBuilder requestMessageBuilder)
    {
        requestMessageBuilder = base.BuildRequest(requestMessageBuilder);
        if (string.IsNullOrWhiteSpace(ContentType)) ContentType = "application/octet-stream";
        if (!Headers.ContainsKey("Content-Type")) Headers["Content-Type"] = ContentType;

        requestMessageBuilder.AddOrUpdateHeaderParameter("Content-Type", Headers["Content-Type"]);
        if (!string.IsNullOrWhiteSpace(UploadId) && PartNumber > 0)
        {
            requestMessageBuilder.AddQueryParameter("uploadId", $"{UploadId}");
            requestMessageBuilder.AddQueryParameter("partNumber", $"{PartNumber}");
        }

        if (!string.IsNullOrWhiteSpace(Prefix))
        {
            requestMessageBuilder.AddQueryParameter("prefix", Prefix);
        }

        if (!RequestBody.IsEmpty)
        {
            requestMessageBuilder.SetBody(RequestBody);
        }

        return requestMessageBuilder;
    }

    public override PutObjectArgs WithHeaders(IDictionary<string, string> headers)
    {
        Headers ??= new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (headers is not null)
            foreach (var p in headers)
            {
                var key = p.Key;
                if (!OperationsUtil.IsSupportedHeader(p.Key) &&
                    !p.Key.StartsWith("x-amz-meta-", StringComparison.OrdinalIgnoreCase))
                {
                    key = "x-amz-meta-" + key.ToLowerInvariant();
                    _ = Headers.Remove(p.Key);
                }

                Headers[key] = p.Value;
                if (string.Equals(key, "Content-Type", StringComparison.OrdinalIgnoreCase))
                    ContentType = p.Value;
            }

        if (string.IsNullOrWhiteSpace(ContentType)) ContentType = "application/octet-stream";
        Headers["Content-Type"] = ContentType;
        return this;
    }

    internal PutObjectArgs WithUploadId(string id = null)
    {
        UploadId = id;
        return this;
    }

    internal PutObjectArgs WithPartNumber(int num)
    {
        PartNumber = num;
        return this;
    }

    public PutObjectArgs WithFileName(string file)
    {
        FileName = file;
        return this;
    }

    public PutObjectArgs WithObjectSize(long size)
    {
        ObjectSize = size;
        return this;
    }

    public PutObjectArgs WithStreamData(Stream data)
    {
        ObjectStreamData = data;
        return this;
    }

    public PutObjectArgs WithProgress(IProgress<ProgressReport> progress)
    {
        Progress = progress;
        return this;
    }
}
