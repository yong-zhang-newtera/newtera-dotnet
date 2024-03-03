/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

using System.Net;

namespace Newtera.DataModel.Response;

public class PutObjectResponse : GenericResponse
{
    public PutObjectResponse(HttpStatusCode statusCode, string responseContent,
        IDictionary<string, string> responseHeaders, long size, string name)
        : base(statusCode, responseContent)
    {
        if (responseHeaders is null) throw new ArgumentNullException(nameof(responseHeaders));

        foreach (var parameter in responseHeaders)
            if (parameter.Key.Equals("ETag", StringComparison.OrdinalIgnoreCase))
            {
                Etag = parameter.Value;
                break;
            }

        Size = size;
        ObjectName = name;
    }

    public string Etag { get; set; }
    public string ObjectName { get; set; }
    public long Size { get; set; }
}
