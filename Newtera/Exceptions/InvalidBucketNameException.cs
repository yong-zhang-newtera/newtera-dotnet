/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class InvalidBucketNameException : NewteraException
{
    private readonly string bucketName;

    public InvalidBucketNameException(string bucketName, string message) : base(message)
    {
        this.bucketName = bucketName;
    }

    public InvalidBucketNameException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public InvalidBucketNameException(string message) : base(message)
    {
    }

    public InvalidBucketNameException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public InvalidBucketNameException()
    {
    }

    public InvalidBucketNameException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidBucketNameException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }

    public override string ToString()
    {
        return $"{bucketName}: {base.ToString()}";
    }
}
