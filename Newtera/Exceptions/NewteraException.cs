/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2017, 2018, 2019, 2020 Newtera, Inc.
 *
 */

using System.Runtime.Serialization;
using Newtera.DataModel.Result;

namespace Newtera.Exceptions;

[Serializable]
public class NewteraException : Exception
{
    public NewteraException()
    {
    }

    public NewteraException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public NewteraException(ResponseResult serverResponse) : this(null, serverResponse)
    {
    }

    public NewteraException(string message) : this(message, serverResponse: null)
    {
    }

    protected NewteraException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
        serializationInfo, streamingContext)
    {
    }

    public NewteraException(string message, ResponseResult serverResponse)
        : base(GetMessage(message, serverResponse))
    {
        ServerMessage = message;
        ServerResponse = serverResponse;
    }

    public string ServerMessage { get; }

    public ResponseResult ServerResponse { get; }

    public ErrorResponse Response { get; internal set; }

    public string XmlError { get; internal set; }

    private static string GetMessage(string message, ResponseResult serverResponse)
    {
        if (serverResponse is null && string.IsNullOrEmpty(message))
            throw new ArgumentNullException(nameof(message));

        if (serverResponse is null)
            return $"Newtera API responded with message={message}";

        var contentString = serverResponse.Content;

        return message is null
            ? $"Newtera API responded with status code={serverResponse.StatusCode}, response={serverResponse.ErrorMessage}, content={contentString}"
            : $"Newtera API responded with message={message}. Status code={serverResponse.StatusCode}, response={serverResponse.ErrorMessage}, content={contentString}";
    }
}
