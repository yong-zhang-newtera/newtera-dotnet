/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

using System.Globalization;
using System.Net;
using System.Text;
using System.Xml.Linq;
using CommunityToolkit.HighPerformance;
using Newtera.DataModel.Result;
using Newtera.Helper;

namespace Newtera.DataModel.Response;

internal class GetObjectsListResponse : GenericResponse
{
    internal ListBucketResult BucketResult;
    internal Tuple<ListBucketResult, List<Item>> ObjectsTuple;

    internal GetObjectsListResponse(HttpStatusCode statusCode, string responseContent)
        : base(statusCode, responseContent)
    {
        if (string.IsNullOrEmpty(responseContent) ||
            !HttpStatusCode.OK.Equals(statusCode))
            return;

        using var stream = Encoding.UTF8.GetBytes(responseContent).AsMemory().AsStream();
        BucketResult = Utils.DeserializeXml<ListBucketResult>(stream);

        var root = XDocument.Parse(responseContent);
        XNamespace ns = Utils.DetermineNamespace(root);

        var items = from c in root.Root.Descendants(ns + "Contents")
            select new Item
            {
                Key = c.Element(ns + "Key").Value,
                LastModified = c.Element(ns + "LastModified").Value,
                ETag = c.Element(ns + "ETag").Value,
                Size = ulong.Parse(c.Element(ns + "Size").Value,
                    CultureInfo.CurrentCulture),
                IsDir = false
            };
        var prefixes = from c in root.Root.Descendants(ns + "CommonPrefixes")
            select new Item { Key = c.Element(ns + "Prefix").Value, IsDir = true };
        items = items.Concat(prefixes);
        ObjectsTuple = Tuple.Create(BucketResult, items.ToList());
    }
}
