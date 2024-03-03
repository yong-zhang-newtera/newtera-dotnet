/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Newtera.DataModel;

[Serializable]
[XmlType(TypeName = "Delete")]
public class DeleteObjectsRequest
{
    public DeleteObjectsRequest(ICollection<DeleteObject> objectsList, bool quiet = true)
    {
        Quiet = quiet;
        Objects = objectsList;
    }

    public DeleteObjectsRequest()
    {
        Quiet = true;
        Objects = new Collection<DeleteObject>();
    }

    [XmlElement("Quiet")] public bool Quiet { get; set; }

    [XmlElement("Object")] public ICollection<DeleteObject> Objects { get; set; }
}
