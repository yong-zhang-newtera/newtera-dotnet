/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

namespace Newtera.DataModel.Tracing;

public sealed class RequestToLog
{
    public string Resource { get; internal set; }
    public IEnumerable<RequestParameter> Parameters { get; internal set; }
    public string Method { get; internal set; }
    public Uri Uri { get; internal set; }
}
