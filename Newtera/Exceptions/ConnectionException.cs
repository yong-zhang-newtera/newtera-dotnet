/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2017, 2018, 2019, 2020 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class ConnectionException : NewteraException
{
    public ConnectionException(string message, ResponseResult response) : base(message, response)
    {
    }

    public ConnectionException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public ConnectionException(string message) : base(message)
    {
    }

    public ConnectionException()
    {
    }

    public ConnectionException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ConnectionException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}
