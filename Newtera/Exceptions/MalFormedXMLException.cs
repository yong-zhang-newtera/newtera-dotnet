/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2017, 2018, 2019, 2020 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;

namespace Newtera.Exceptions;

[Serializable]
public class MalFormedXMLException : Exception
{
    internal string bucketName;
    internal string key;
    internal string resource;

    public MalFormedXMLException()
    {
    }

    public MalFormedXMLException(string message) : base(message)
    {
    }

    public MalFormedXMLException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public MalFormedXMLException(string resource, string bucketName, string message, string keyName = null) :
        base(message)
    {
        this.resource = resource;
        this.bucketName = bucketName;
        key = keyName;
    }

    protected MalFormedXMLException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}
