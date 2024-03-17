/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

using System.Net;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Newtera.DataModel.Response;

internal class GetObjectsListResponse : GenericResponse
{
    internal List<ObjectModel> Objects { get; }

    internal GetObjectsListResponse(HttpStatusCode statusCode, string responseContent)
        : base(statusCode, responseContent)
    {
        Objects = new List<ObjectModel>();

        if (string.IsNullOrEmpty(responseContent) ||
            !HttpStatusCode.OK.Equals(statusCode))
            return;

        var jObject = (JObject) JsonConvert.DeserializeObject(responseContent);
        var jArray = (JArray)jObject["files"];
        foreach (var item in jArray) {
            var objectModel = item.ToObject<ObjectModel>();
            Objects.Add(objectModel);
        }
    }
}
