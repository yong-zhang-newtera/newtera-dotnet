﻿/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

using System.Text;
using System.Xml.Linq;

namespace Newtera.DataModel.Args;

internal class CompleteMultipartUploadArgs : ObjectWriteArgs<CompleteMultipartUploadArgs>
{
    internal CompleteMultipartUploadArgs()
    {
        RequestMethod = HttpMethod.Post;
    }

    internal CompleteMultipartUploadArgs(PutObjectPartArgs args)
    {
        // destBucketName, destObjectName, metadata, sseHeaders
        RequestMethod = HttpMethod.Post;
        BucketName = args.BucketName;
        ObjectName = args.ObjectName;
        Headers = new Dictionary<string, string>(StringComparer.Ordinal);
        if (args.Headers?.Count > 0)
            Headers = Headers.Concat(args.Headers).GroupBy(item => item.Key, StringComparer.Ordinal)
                .ToDictionary(item => item.Key, item => item.First().Value, StringComparer.Ordinal);
    }

    internal string UploadId { get; set; }
    internal Dictionary<int, string> ETags { get; set; }

    internal override void Validate()
    {
        base.Validate();
        if (string.IsNullOrWhiteSpace(UploadId))
            throw new InvalidOperationException(nameof(UploadId) + " cannot be empty.");
        if (ETags is null || ETags.Count <= 0)
            throw new InvalidOperationException(nameof(ETags) + " dictionary cannot be empty.");
    }

    internal CompleteMultipartUploadArgs WithUploadId(string uploadId)
    {
        UploadId = uploadId;
        return this;
    }

    internal CompleteMultipartUploadArgs WithETags(IDictionary<int, string> etags)
    {
        if (etags?.Count > 0) ETags = new Dictionary<int, string>(etags);
        return this;
    }

    internal override HttpRequestMessageBuilder BuildRequest(HttpRequestMessageBuilder requestMessageBuilder)
    {
        requestMessageBuilder.AddQueryParameter("uploadId", $"{UploadId}");
        var parts = new List<XElement>();

        for (var i = 1; i <= ETags.Count; i++)
            parts.Add(new XElement("Part",
                new XElement("PartNumber", i),
                new XElement("ETag", ETags[i])));

        var completeMultipartUploadXml = new XElement("CompleteMultipartUpload", parts);
        var bodyString = completeMultipartUploadXml.ToString();
        ReadOnlyMemory<byte> bodyInBytes = Encoding.UTF8.GetBytes(bodyString);
        requestMessageBuilder.BodyParameters.Add("content-type", "application/xml");
        requestMessageBuilder.SetBody(bodyInBytes);
        // var bodyInCharArr = Encoding.UTF8.GetString(requestMessageBuilder.Content).ToCharArray();

        return requestMessageBuilder;
    }
}
