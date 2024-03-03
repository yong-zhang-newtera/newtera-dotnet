/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Newtera.DataModel.Tracing;

public sealed class ResponseToLog
{
    public string Content { get; internal set; }

    [SuppressMessage("Design", "MA0016:Prefer returning collection abstraction instead of implementation",
        Justification = "Needs to be concrete type for XML deserialization")]
    public Dictionary<string, string> Headers { get; internal set; }

    public HttpStatusCode StatusCode { get; internal set; }
    public Uri ResponseUri { get; internal set; }
    public double DurationMs { get; internal set; }
    public string ErrorMessage { get; set; }
}
