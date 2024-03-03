/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class ForbiddenException : NewteraException
{
    public ForbiddenException(string message) : base(message)
    {
    }

    public ForbiddenException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public ForbiddenException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public ForbiddenException()
    {
    }

    public ForbiddenException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ForbiddenException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}
