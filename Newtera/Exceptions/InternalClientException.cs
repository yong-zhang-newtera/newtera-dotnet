/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2017, 2018, 2019, 2020 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class InternalClientException : NewteraException
{
    public InternalClientException(string message, ResponseResult response) : base(message, response)
    {
    }

    public InternalClientException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public InternalClientException(string message) : base(message)
    {
    }

    public InternalClientException()
    {
    }

    public InternalClientException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InternalClientException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}
