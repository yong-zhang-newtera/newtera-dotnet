/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2017, 2018, 2019, 2020 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;

namespace Newtera.Exceptions;

[Serializable]
public class AuthorizationException : Exception
{
    internal readonly string accessKey;
    internal readonly string bucketName;
    internal readonly string resource;

    public AuthorizationException()
    {
    }

    public AuthorizationException(string message) : base(message)
    {
    }

    public AuthorizationException(string resource, string bucketName, string message, string accesskey = null) :
        base(message)
    {
        this.resource = resource;
        this.bucketName = bucketName;
        accessKey = accesskey;
    }

    public AuthorizationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected AuthorizationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}
