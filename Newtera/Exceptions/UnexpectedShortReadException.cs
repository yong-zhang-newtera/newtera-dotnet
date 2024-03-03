/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class UnexpectedShortReadException : NewteraException
{
    public UnexpectedShortReadException(string message) : base(message)
    {
    }

    public UnexpectedShortReadException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public UnexpectedShortReadException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public UnexpectedShortReadException()
    {
    }

    public UnexpectedShortReadException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected UnexpectedShortReadException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
        base(serializationInfo, streamingContext)
    {
    }
}
