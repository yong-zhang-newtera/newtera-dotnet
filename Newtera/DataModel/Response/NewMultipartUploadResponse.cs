/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

using System.Net;
using System.Text;
using CommunityToolkit.HighPerformance;
using Newtera.DataModel.Result;
using Newtera.Helper;

namespace Newtera.DataModel.Response;

internal class NewMultipartUploadResponse : GenericResponse
{
    internal NewMultipartUploadResponse(HttpStatusCode statusCode, string responseContent)
        : base(statusCode, responseContent)
    {
        using var stream = Encoding.UTF8.GetBytes(responseContent).AsMemory().AsStream();
        var newUpload = Utils.DeserializeXml<InitiateMultipartUploadResult>(stream);

        UploadId = newUpload.UploadId;
    }

    internal string UploadId { get; }
}
