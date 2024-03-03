/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class RedirectionException : NewteraException
{
    public RedirectionException(string message) : base(message)
    {
    }

    public RedirectionException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public RedirectionException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public RedirectionException()
    {
    }

    public RedirectionException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected RedirectionException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}
