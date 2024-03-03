/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class DeleteObjectException : NewteraException
{
    public DeleteObjectException(string message) : base(message)
    {
    }

    public DeleteObjectException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public DeleteObjectException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public DeleteObjectException()
    {
    }

    public DeleteObjectException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected DeleteObjectException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}
