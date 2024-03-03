/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class EntityTooLargeException : NewteraException
{
    public EntityTooLargeException(string message) : base(message)
    {
    }

    public EntityTooLargeException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public EntityTooLargeException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public EntityTooLargeException()
    {
    }

    public EntityTooLargeException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected EntityTooLargeException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}
