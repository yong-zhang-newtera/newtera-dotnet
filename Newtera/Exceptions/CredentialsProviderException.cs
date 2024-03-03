/*
 * Newtera .NET Library for Newtera TDM, (C) 2021 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class CredentialsProviderException : NewteraException
{
    private readonly string credentialProviderType;

    public CredentialsProviderException(string credentialProviderType, string message) : base(message)
    {
        this.credentialProviderType = credentialProviderType;
    }

    public CredentialsProviderException(ResponseResult serverResponse) : base(serverResponse)
    {
    }

    public CredentialsProviderException(string message) : base(message)
    {
    }

    public CredentialsProviderException(string message, ResponseResult serverResponse) : base(message, serverResponse)
    {
    }

    public CredentialsProviderException()
    {
    }

    public CredentialsProviderException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected CredentialsProviderException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
        base(serializationInfo, streamingContext)
    {
    }

    public override string ToString()
    {
        return $"{credentialProviderType}: {base.ToString()}";
    }
}
