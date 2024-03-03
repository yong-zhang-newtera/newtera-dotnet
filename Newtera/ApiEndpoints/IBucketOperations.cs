/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2017-2021 Newtera, Inc.
 *
 */

using Newtera.DataModel;
using Newtera.DataModel.Args;
using Newtera.DataModel.Result;
using Newtera.Exceptions;

namespace Newtera.ApiEndpoints;

public interface IBucketOperations
{
    /// <summary>
    ///     Check if a private bucket with the given name exists.
    /// </summary>
    /// <param name="args">BucketExistsArgs Arguments Object which has bucket identifier information - bucket name, region</param>
    /// <param name="cancellationToken">Optional cancellation token to cancel the operation</param>
    /// <returns> Task </returns>
    /// <exception cref="AuthorizationException">When access or secret key is invalid</exception>
    /// <exception cref="InvalidBucketNameException">When bucket name is invalid</exception>
    /// <exception cref="BucketNotFoundException">When bucket is not found</exception>
    Task<bool> BucketExistsAsync(BucketExistsArgs args, CancellationToken cancellationToken = default);

    /// <summary>
    ///     List all objects non-recursively in a bucket with a given prefix, optionally emulating a directory
    /// </summary>
    /// <param name="args">
    ///     ListObjectsArgs Arguments Object with information like Bucket name, prefix, recursive listing,
    ///     versioning
    /// </param>
    /// <param name="cancellationToken">Optional cancellation token to cancel the operation</param>
    /// <returns>An observable of items that client can subscribe to</returns>
    /// <exception cref="AuthorizationException">When access or secret key is invalid</exception>
    /// <exception cref="InvalidBucketNameException">When bucket name is invalid</exception>
    /// <exception cref="BucketNotFoundException">When bucket is not found</exception>
    /// <exception cref="InvalidOperationException">
    ///     For example, if you call ListObjectsAsync on a bucket with versioning
    ///     enabled or object lock enabled
    /// </exception>
    IObservable<Item> ListObjectsAsync(ListObjectsArgs args, CancellationToken cancellationToken = default);
}
