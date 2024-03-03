/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

namespace Newtera.DataModel.Tracing;

public sealed class RequestParameter
{
    public string Name { get; internal set; }
    public object Value { get; internal set; }
    public string Type { get; internal set; }
}
