/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class InvalidEndpointException : NewteraException
{
    private readonly string endpoint;

    public InvalidEndpointException(string endpoint, string message) : base(message)
    {
        this.endpoint = endpoint;
    }

    public InvalidEndpointException(string message) : base(message)
    {
    }

    public InvalidEndpointException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public InvalidEndpointException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public InvalidEndpointException()
    {
    }

    public InvalidEndpointException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidEndpointException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }

    public override string ToString()
    {
        if (string.IsNullOrEmpty(endpoint))
            return base.ToString();
        return $"{endpoint}: {base.ToString()}";
    }
}
