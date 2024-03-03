/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Xml.Serialization;

namespace Newtera.DataModel.Result;

[Serializable]
[XmlRoot(ElementName = "ListPartsResult", Namespace = "http://s3.amazonaws.com/doc/2006-03-01/")]
public class ListPartsResult
{
    public int NextPartNumberMarker { get; set; }
    public bool IsTruncated { get; set; }
}
