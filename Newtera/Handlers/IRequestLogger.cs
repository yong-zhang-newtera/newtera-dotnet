/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using Newtera.DataModel.Tracing;

namespace Newtera.Handlers;

public interface IRequestLogger
{
    void LogRequest(RequestToLog requestToLog, ResponseToLog responseToLog, double durationMs);
}
