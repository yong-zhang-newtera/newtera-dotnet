/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Globalization;

namespace Newtera.DataModel;

[Serializable]
public class ObjectModel
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public string Size { get; set; }
    public string Type { get; set; }
    public string Suffix { get; set; }
    public string InstanceId { get; set; }
    public string ClassName { get; set; }
    public string Creator { get; set; }
}
