/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Xml.Serialization;

namespace Newtera.DataModel.Result;

[Serializable]
[XmlRoot(ElementName = "ListBucketResult", Namespace = "http://s3.amazonaws.com/doc/2006-03-01/")]
[XmlInclude(typeof(Item))]
[XmlInclude(typeof(Prefix))]
public class ListBucketResult
{
    public string Name { get; set; }
    public string Prefix { get; set; }
    public string NextMarker { get; set; }
    public string MaxKeys { get; set; }
    public string Delimiter { get; set; }
    public string KeyCount { get; set; }
    public bool IsTruncated { get; set; }
    public string NextContinuationToken { get; set; }
    public string EncodingType { get; set; }
}
