/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Xml.Serialization;

namespace Newtera.DataModel;

[Serializable]
[XmlType(TypeName = "Object")]
public class DeleteObject
{
    public DeleteObject()
    {
        Key = null;
        VersionId = null;
    }

    public DeleteObject(string key, string versionId = null)
    {
        Key = key;
        VersionId = versionId;
    }

    [XmlElement("Key")] public string Key { get; set; }

    [XmlElement("VersionId")] public string VersionId { get; set; }
}
