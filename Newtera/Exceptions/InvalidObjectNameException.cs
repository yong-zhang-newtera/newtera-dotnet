/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class InvalidObjectNameException : NewteraException
{
    private readonly string objectName;

    public InvalidObjectNameException(string objectName, string message) : base(message)
    {
        this.objectName = objectName;
    }

    public InvalidObjectNameException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public InvalidObjectNameException(string message) : base(message)
    {
    }

    public InvalidObjectNameException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public InvalidObjectNameException()
    {
    }

    public InvalidObjectNameException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidObjectNameException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }

    public override string ToString()
    {
        return $"{objectName}: {base.ToString()}";
    }
}
