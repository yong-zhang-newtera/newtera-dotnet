/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Newtera.Exceptions;

namespace Newtera.DataModel.Result;

[Serializable]
[XmlRoot(ElementName = "DeleteResult", Namespace = "http://s3.amazonaws.com/doc/2006-03-01/")]
public class DeleteObjectsResult
{
    [XmlElement("Deleted")] public Collection<DeletedObject> ObjectsList { get; set; }
    [XmlElement("Error")] public Collection<DeleteError> ErrorList { get; set; }
}
