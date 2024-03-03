/*
 * Newtera .NET Library for Newtera TDM, (C) 2020 Newtera, Inc.
 *
 */

namespace Newtera.DataModel.Args;

public abstract class RequestArgs

{
    // RequestMethod will be the HTTP Method for request variable,
    // which is of type HttpRequestMessage.
    // Will be one of the types: - HEAD, GET, PUT, DELETE. etc.
    internal HttpMethod RequestMethod { get; set; }

    internal string RequestPath { get; set; }

    internal virtual HttpRequestMessageBuilder BuildRequest(HttpRequestMessageBuilder requestMessageBuilder)
    {
        return requestMessageBuilder;
    }
}
