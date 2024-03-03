/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

using System.Net;
using Newtera.DataModel.Args;

namespace Newtera.DataModel.Response;

internal class StatObjectResponse : GenericResponse
{
    internal StatObjectResponse(HttpStatusCode statusCode, string responseContent,
        IDictionary<string, string> responseHeaders, StatObjectArgs args)
        : base(statusCode, responseContent)
    {
        // StatObjectResponse object is populated with available stats from the response.
        ObjectInfo = ObjectStat.FromResponseHeaders(args.ObjectName, responseHeaders);
    }

    internal ObjectStat ObjectInfo { get; set; }
}
