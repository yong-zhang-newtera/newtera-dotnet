/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2020 Newtera, Inc.
 *
 */

using Newtera.ApiEndpoints;
using Newtera.Handlers;

namespace Newtera;

public interface INewteraClient : IBucketOperations, IObjectOperations, IDisposable
{
    NewteraConfig Config { get; }
    IEnumerable<IApiResponseErrorHandler> ResponseErrorHandlers { get; }
    IApiResponseErrorHandler DefaultErrorHandler { get; }
    IRequestLogger RequestLogger { get; }

    void SetTraceOff();
    void SetTraceOn(IRequestLogger logger = null);
    Task<HttpResponseMessage> WrapperGetAsync(Uri uri);
    Task WrapperPutAsync(Uri uri, StreamContent strm);
}
