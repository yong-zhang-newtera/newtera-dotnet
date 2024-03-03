/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Xml.Serialization;

namespace Newtera.Exceptions;

[Serializable]
[XmlRoot(ElementName = "Error", Namespace = "")]
public class ErrorResponse
{
    public string Code { get; set; }
    public string Message { get; set; }
    public string RequestId { get; set; }
    public string HostId { get; set; }
    public string Resource { get; set; }
    public string BucketName { get; set; }
    public string Key { get; set; }
    public string VersionId { get; set; }
    public bool DeleteMarker { get; set; }
    public string BucketRegion { get; set; }
}
