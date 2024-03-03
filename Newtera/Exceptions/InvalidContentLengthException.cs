/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class InvalidContentLengthException : NewteraException
{
    private readonly string bucketName;
    private readonly string objectName;

    public InvalidContentLengthException(string bucketName, string objectName, string message) : base(message)
    {
        this.bucketName = bucketName;
        this.objectName = objectName;
    }

    public InvalidContentLengthException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public InvalidContentLengthException(string message) : base(message)
    {
    }

    public InvalidContentLengthException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public InvalidContentLengthException()
    {
    }

    public InvalidContentLengthException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidContentLengthException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
        base(serializationInfo, streamingContext)
    {
    }

    public override string ToString()
    {
        return $"{bucketName} :{objectName}: {base.ToString()}";
    }
}
