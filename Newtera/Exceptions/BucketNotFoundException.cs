/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2017, 2018, 2019, 2020 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class BucketNotFoundException : NewteraException
{
    private readonly string bucketName;

    public BucketNotFoundException(string bucketName, string message) : base(message)
    {
        this.bucketName = bucketName;
    }

    public BucketNotFoundException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public BucketNotFoundException(string message) : base(message)
    {
    }

    public BucketNotFoundException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public BucketNotFoundException()
    {
    }

    public BucketNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected BucketNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }

    public override string ToString()
    {
        return $"{bucketName}: {base.ToString()}";
    }
}
