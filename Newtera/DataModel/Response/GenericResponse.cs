/*
 * Newtera .NET Library for Newtera TDM, (C) 2020 Newtera, Inc.
 *
 */

using System.Net;

namespace Newtera.DataModel.Response;

public class GenericResponse
{
    internal GenericResponse(HttpStatusCode statusCode, string responseContent)
    {
        ResponseContent = responseContent;
        ResponseStatusCode = statusCode;
    }

    internal string ResponseContent { get; }
    internal HttpStatusCode ResponseStatusCode { get; }
}
