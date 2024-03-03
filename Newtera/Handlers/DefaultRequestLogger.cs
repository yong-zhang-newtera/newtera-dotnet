/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Text;
using Microsoft.Extensions.Logging;
using Newtera.DataModel.Tracing;

namespace Newtera.Handlers;

public sealed class DefaultRequestLogger : IRequestLogger
{
    private readonly ILogger logger;

    public DefaultRequestLogger()
    {
    }

    public DefaultRequestLogger(ILogger logger)
    {
        this.logger = logger;
    }

    public void LogRequest(RequestToLog requestToLog, ResponseToLog responseToLog, double durationMs)
    {
        if (requestToLog is null) throw new ArgumentNullException(nameof(requestToLog));

        if (responseToLog is null) throw new ArgumentNullException(nameof(responseToLog));

        var sb = new StringBuilder("Request completed in ");

        _ = sb.Append(durationMs);
        _ = sb.AppendLine(" ms");

        _ = sb.AppendLine();
        _ = sb.AppendLine("- - - - - - - - - - BEGIN REQUEST - - - - - - - - - -");
        _ = sb.AppendLine();
        _ = sb.Append(requestToLog.Method);
        _ = sb.Append(' ');
        _ = sb.Append(requestToLog.Uri);
        _ = sb.AppendLine(" HTTP/1.1");

        var requestHeaders = requestToLog.Parameters;
        requestHeaders =
            requestHeaders.OrderByDescending(p => string.Equals(p.Name, "Host", StringComparison.OrdinalIgnoreCase));

        foreach (var item in requestHeaders)
        {
            _ = sb.Append(item.Name);
            _ = sb.Append(": ");
            _ = sb.Append(item.Value).AppendLine();
        }

        _ = sb.AppendLine();
        _ = sb.AppendLine();

        _ = sb.AppendLine("- - - - - - - - - - END REQUEST - - - - - - - - - -");
        _ = sb.AppendLine();

        _ = sb.AppendLine("- - - - - - - - - - BEGIN RESPONSE - - - - - - - - - -");
        _ = sb.AppendLine();

        _ = sb.Append("HTTP/1.1 ");
        _ = sb.Append((int)responseToLog.StatusCode);
        _ = sb.Append(' ');
        _ = sb.AppendLine(responseToLog.StatusCode.ToString());

        var responseHeaders = responseToLog.Headers;

        foreach (var item in responseHeaders)
        {
            _ = sb.Append(item.Key);
            _ = sb.Append(": ");
            _ = sb.AppendLine(item.Value);
        }

        _ = sb.AppendLine();
        _ = sb.AppendLine();

        _ = sb.AppendLine(responseToLog.Content);
        _ = sb.AppendLine(responseToLog.ErrorMessage);

        _ = sb.AppendLine("- - - - - - - - - - END RESPONSE - - - - - - - - - -");


        if (logger is not null)
            logger.LogInformation(sb.ToString());
        else
            Console.WriteLine(sb.ToString());
    }
}
