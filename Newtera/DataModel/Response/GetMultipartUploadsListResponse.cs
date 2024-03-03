/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

using System.Net;
using System.Text;
using System.Xml.Linq;
using CommunityToolkit.HighPerformance;
using Newtera.DataModel.Result;
using Newtera.Helper;

namespace Newtera.DataModel.Response;

internal class GetMultipartUploadsListResponse : GenericResponse
{
    internal GetMultipartUploadsListResponse(HttpStatusCode statusCode, string responseContent)
        : base(statusCode, responseContent)
    {
        using var stream = Encoding.UTF8.GetBytes(responseContent).AsMemory().AsStream();
        var uploadsResult = Utils.DeserializeXml<ListMultipartUploadsResult>(stream);
        var root = XDocument.Parse(responseContent);
        XNamespace ns = Utils.DetermineNamespace(root);

        var itemCheck = root.Root.Descendants(ns + "Upload").FirstOrDefault();
        if (uploadsResult is null || itemCheck?.HasElements != true) return;
        var uploads = from c in root.Root.Descendants(ns + "Upload")
            select new Upload
            {
                Key = c.Element(ns + "Key").Value,
                UploadId = c.Element(ns + "UploadId").Value,
                Initiated = c.Element(ns + "Initiated").Value
            };
        UploadResult = new Tuple<ListMultipartUploadsResult, List<Upload>>(uploadsResult, uploads.ToList());
    }

    internal Tuple<ListMultipartUploadsResult, List<Upload>> UploadResult { get; }
}
