/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2017, 2018, 2019, 2020 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class ErrorResponseException : NewteraException
{
    public ErrorResponseException(ErrorResponse errorResponse, ResponseResult serverResponse) :
        base(serverResponse)
    {
        Response = errorResponse;
    }

    public ErrorResponseException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public ErrorResponseException(string message) : base(message)
    {
    }

    public ErrorResponseException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public ErrorResponseException()
    {
    }

    public ErrorResponseException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ErrorResponseException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }
}
