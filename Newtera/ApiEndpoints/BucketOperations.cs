/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2017-2021 Newtera, Inc.
 */

using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reactive.Linq;
using CommunityToolkit.HighPerformance;
using Newtera.ApiEndpoints;
using Newtera.DataModel;
using Newtera.DataModel.Args;
using Newtera.DataModel.Response;
using Newtera.Exceptions;
using Newtonsoft.Json;

namespace Newtera;

[SuppressMessage("Design", "MA0048:File name must match type name", Justification = "Split up in partial classes")]
public partial class NewteraClient : IBucketOperations
{
    /// <summary>
    ///     Check if a private bucket with the given name exists.
    /// </summary>
    /// <param name="args">BucketExistsArgs Arguments Object which has bucket identifier information - bucket name, region</param>
    /// <param name="cancellationToken">Optional cancellation token to cancel the operation</param>
    /// <returns> Task </returns>
    public async Task<bool> BucketExistsAsync(BucketExistsArgs args, CancellationToken cancellationToken = default)
    {
        args?.Validate();
        try
        {
            var requestMessageBuilder = await this.CreateRequest(args).ConfigureAwait(false);
            using var response =
                await this.ExecuteTaskAsync(requestMessageBuilder,
                    cancellationToken: cancellationToken).ConfigureAwait(false);
            return response is not null && response.StatusCode == HttpStatusCode.OK;
        }
        catch (InternalClientException ice)
        {
            return (ice.ServerResponse is null ||
                    !HttpStatusCode.NotFound.Equals(ice.ServerResponse.StatusCode)) &&
                   ice.ServerResponse is not null;
        }
        catch (Exception ex)
        {
            if (ex.GetType() == typeof(BucketNotFoundException)) return false;
            throw;
        }
    }

    /// <summary>
    ///     List all objects along with versions non-recursively in a bucket with a given prefix, optionally emulating a
    ///     directory
    /// </summary>
    /// <param name="args">
    ///     ListObjectsArgs Arguments Object with information like Bucket name, prefix, recursive listing,
    ///     versioning
    /// </param>
    /// <param name="cancellationToken">Optional cancellation token to cancel the operation</param>
    /// <returns>A list of objects</returns>
    /// <exception cref="AuthorizationException">When access or secret key is invalid</exception>
    /// <exception cref="InvalidBucketNameException">When bucket name is invalid</exception>
    /// <exception cref="BucketNotFoundException">When bucket is not found</exception>
    /// <exception cref="NotImplementedException">If a functionality or extension (like versioning) is not implemented</exception>
    /// <exception cref="InvalidOperationException">
    ///     For example, if you call ListObjectsAsync on a bucket with versioning
    ///     enabled or object lock enabled
    /// </exception>
    public async Task<IList<ObjectModel>> ListObjectsAsync(ListObjectsArgs args, CancellationToken cancellationToken = default)
    { 
        args?.Validate();

        var getObjListArgs = new GetObjectListArgs()
            .WithBucket(args.BucketName)
            .WithPrefix(args.Prefix);
        var response = await GetObjectListAsync(getObjListArgs, cancellationToken).ConfigureAwait(false);

        return response;
    }

    /// <summary>
    ///     Gets the list of objects in the bucket filtered by prefix
    /// </summary>
    /// <param name="args">
    ///     GetObjectListArgs Arguments Object with information like Bucket name, prefix, delimiter, marker,
    ///     versions(get version IDs of the objects)
    /// </param>
    /// <returns>Task with a tuple populated with objects</returns>
    /// <param name="cancellationToken">Optional cancellation token to cancel the operation</param>
    private async Task<IList<ObjectModel>> GetObjectListAsync(GetObjectListArgs args,
        CancellationToken cancellationToken = default)
    {
        var requestMessageBuilder = await this.CreateRequest(args).ConfigureAwait(false);
        using var responseResult =
            await this.ExecuteTaskAsync(requestMessageBuilder,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

        var response = new GetObjectsListResponse(responseResult.StatusCode, responseResult.Content);

        return response.Objects;
    }
}
