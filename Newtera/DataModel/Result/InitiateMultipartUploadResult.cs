/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Xml.Serialization;

namespace Newtera.DataModel.Result;

[Serializable]
[XmlRoot(ElementName = "InitiateMultipartUploadResult", Namespace = "http://s3.amazonaws.com/doc/2006-03-01/")]
public class InitiateMultipartUploadResult
{
    public string UploadId { get; set; }
}
