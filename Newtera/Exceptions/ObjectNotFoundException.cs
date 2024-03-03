/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class ObjectNotFoundException : NewteraException
{
    private readonly string objectName;

    public ObjectNotFoundException(string objectName, string message) : base(message)
    {
        this.objectName = objectName;
    }

    public ObjectNotFoundException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public ObjectNotFoundException(string message) : base(message)
    {
    }

    public ObjectNotFoundException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public ObjectNotFoundException()
    {
    }

    public ObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ObjectNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }

    public override string ToString()
    {
        return $"{objectName}: {base.ToString()}";
    }
}
