/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

namespace Newtera.DataModel.Args;

public class RemoveObjectArgs : ObjectArgs<RemoveObjectArgs>
{
    public RemoveObjectArgs()
    {
        RequestMethod = HttpMethod.Delete;
        RequestPath = "/api/blob/objects/";
    }

    internal override HttpRequestMessageBuilder BuildRequest(HttpRequestMessageBuilder requestMessageBuilder)
    {
        requestMessageBuilder.AddQueryParameter("prefix", Prefix);

        return requestMessageBuilder;
    }
}
