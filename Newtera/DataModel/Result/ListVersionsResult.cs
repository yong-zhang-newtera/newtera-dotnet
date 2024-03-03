/*
 * Newtera .NET Library for Newtera TDM, (C) 2020 Newtera, Inc.
 *
 */

using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Newtera.DataModel.Result;

[Serializable]
[XmlRoot(ElementName = "ListVersionsResult", Namespace = "http://s3.amazonaws.com/doc/2006-03-01/")]
[XmlInclude(typeof(Prefix))]
public class ListVersionsResult
{
    public string Name { get; set; }
    public string Prefix { get; set; }
    public string NextMarker { get; set; }
    public string MaxKeys { get; set; }
    public string Delimiter { get; set; }
    public bool IsTruncated { get; set; }
    public string EncodingType { get; set; }

    [XmlElement("Version")] public Collection<Item> Versions { get; set; }

    public string NextKeyMarker { get; set; }
    public string NextVersionIdMarker { get; set; }
}
