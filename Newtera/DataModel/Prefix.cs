/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Xml.Serialization;

namespace Newtera.DataModel;

[Serializable]
public class Prefix
{
    [XmlAttribute("Prefix")] public string Name { get; set; }
}
