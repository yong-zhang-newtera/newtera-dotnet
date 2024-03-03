/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 */

using System.Web;
using Newtera.DataModel.Args;
using Newtera.DataModel.Result;

namespace Newtera.DataModel.Response;

internal class ListObjectsItemResponse
{
    internal Item BucketObjectsLastItem;
    internal IObserver<Item> ItemObservable;

    internal ListObjectsItemResponse(ListObjectsArgs args, Tuple<ListBucketResult, List<Item>> objectList,
        IObserver<Item> obs)
    {
        ItemObservable = obs;
        NextMarker = string.Empty;
        foreach (var item in objectList.Item2)
        {
            BucketObjectsLastItem = item;
            if (string.Equals(objectList.Item1.EncodingType, "url", StringComparison.OrdinalIgnoreCase))
                item.Key = HttpUtility.UrlDecode(item.Key);
            ItemObservable.OnNext(item);
        }

        if (objectList.Item1.NextMarker is not null)
        {
            if (string.Equals(objectList.Item1.EncodingType, "url", StringComparison.OrdinalIgnoreCase))
                NextMarker = HttpUtility.UrlDecode(objectList.Item1.NextMarker);
            else
                NextMarker = objectList.Item1.NextMarker;
        }
        else if (BucketObjectsLastItem is not null)
        {
            if (string.Equals(objectList.Item1.EncodingType, "url", StringComparison.OrdinalIgnoreCase))
                NextMarker = HttpUtility.UrlDecode(BucketObjectsLastItem.Key);
            else
                NextMarker = BucketObjectsLastItem.Key;
        }
    }

    internal string NextMarker { get; }
}
