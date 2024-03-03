/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class InternalServerException : NewteraException
{
    public InternalServerException(string message) : base(message)
    {
    }

    public InternalServerException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public InternalServerException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public InternalServerException()
    {
    }

    public InternalServerException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InternalServerException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}
