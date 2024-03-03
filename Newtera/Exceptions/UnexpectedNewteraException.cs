/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2017, 2018, 2019, 2020 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class UnexpectedNewteraException : NewteraException
{
    public UnexpectedNewteraException(string message) : base(message)
    {
    }

    public UnexpectedNewteraException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public UnexpectedNewteraException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public UnexpectedNewteraException()
    {
    }

    public UnexpectedNewteraException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected UnexpectedNewteraException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}
