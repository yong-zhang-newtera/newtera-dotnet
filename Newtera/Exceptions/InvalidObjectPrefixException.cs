/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class InvalidObjectPrefixException : NewteraException
{
    private readonly string objectPrefix;

    public InvalidObjectPrefixException(string objectPrefix, string message) : base(message)
    {
        this.objectPrefix = objectPrefix;
    }

    public InvalidObjectPrefixException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public InvalidObjectPrefixException(string message) : base(message)
    {
    }

    public InvalidObjectPrefixException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public InvalidObjectPrefixException()
    {
    }

    public InvalidObjectPrefixException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidObjectPrefixException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
        base(serializationInfo, streamingContext)
    {
    }

    public override string ToString()
    {
        return $"{objectPrefix}: {base.ToString()}";
    }
}
