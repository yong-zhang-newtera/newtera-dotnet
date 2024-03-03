/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2017, 2018, 2019, 2020 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class AccessDeniedException : NewteraException
{
    public AccessDeniedException(string message) : base(message)
    {
    }

    public AccessDeniedException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public AccessDeniedException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public AccessDeniedException()
    {
    }

    public AccessDeniedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected AccessDeniedException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}
