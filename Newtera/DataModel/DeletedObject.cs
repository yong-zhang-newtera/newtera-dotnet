/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Xml.Serialization;

namespace Newtera.DataModel;

[Serializable]
public class DeletedObject
{
    [XmlElement("Key")] public string Key { get; set; }

    [XmlElement("VersionId")] public string VersionId { get; set; }

    [XmlElement("DeleteMarker")] public string DeleteMarker { get; set; }

    [XmlElement("DeleteMarkerVersionId")] public string DeleteMarkerVersionId { get; set; }
}
