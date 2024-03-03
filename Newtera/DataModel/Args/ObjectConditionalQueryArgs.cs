/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

using Newtera.Helper;

namespace Newtera.DataModel.Args;

public abstract class ObjectConditionalQueryArgs<T> : ObjectArgs<T>
    where T : ObjectConditionalQueryArgs<T>
{
    internal string MatchETag { get; set; }
    internal string NotMatchETag { get; set; }
    internal DateTime ModifiedSince { get; set; }
    internal DateTime UnModifiedSince { get; set; }

    internal override void Validate()
    {
        base.Validate();
        if (!string.IsNullOrEmpty(MatchETag) && !string.IsNullOrEmpty(NotMatchETag))
            throw new InvalidOperationException("Cannot set both " + nameof(MatchETag) + " and " +
                                                nameof(NotMatchETag) + " for query.");

        if (ModifiedSince != default &&
            UnModifiedSince != default)
            throw new InvalidOperationException("Cannot set both " + nameof(ModifiedSince) + " and " +
                                                nameof(UnModifiedSince) + " for query.");
    }

    internal override HttpRequestMessageBuilder BuildRequest(HttpRequestMessageBuilder requestMessageBuilder)
    {
        if (!string.IsNullOrEmpty(MatchETag)) requestMessageBuilder.AddOrUpdateHeaderParameter("If-Match", MatchETag);
        if (!string.IsNullOrEmpty(NotMatchETag))
            requestMessageBuilder.AddOrUpdateHeaderParameter("If-None-Match", NotMatchETag);
        if (ModifiedSince != default)
            requestMessageBuilder.AddOrUpdateHeaderParameter("If-Modified-Since", Utils.To8601String(ModifiedSince));
        if (UnModifiedSince != default)
            requestMessageBuilder.AddOrUpdateHeaderParameter("If-Unmodified-Since",
                Utils.To8601String(UnModifiedSince));

        return requestMessageBuilder;
    }
}
