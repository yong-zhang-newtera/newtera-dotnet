/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 */

using System.Xml.Serialization;

namespace Newtera.Exceptions;

[Serializable]
[XmlRoot(ElementName = "Error")]
public class DeleteError : ErrorResponse
{
}
