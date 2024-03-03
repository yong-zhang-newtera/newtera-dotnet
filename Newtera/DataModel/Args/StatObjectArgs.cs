/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

namespace Newtera.DataModel.Args;

public class StatObjectArgs : ObjectConditionalQueryArgs<StatObjectArgs>
{
    public StatObjectArgs()
    {
        RequestMethod = HttpMethod.Head;
        RequestPath = "/api/blob/objects/";
    }

    internal long ObjectOffset { get; private set; }
    internal long ObjectLength { get; private set; }
    internal bool OffsetLengthSet { get; set; }

    internal override void Validate()
    {
        base.Validate();
        if (!string.IsNullOrEmpty(NotMatchETag) && !string.IsNullOrEmpty(MatchETag))
            throw new InvalidOperationException("Invalid to set both Etag match conditions " + nameof(NotMatchETag) +
                                                " and " + nameof(MatchETag));

        if (!ModifiedSince.Equals(default) &&
            !UnModifiedSince.Equals(default))
            throw new InvalidOperationException("Invalid to set both modified date match conditions " +
                                                nameof(ModifiedSince) + " and " + nameof(UnModifiedSince));

        if (OffsetLengthSet)
        {
            if (ObjectOffset < 0 || ObjectLength < 0)
                throw new InvalidDataException(nameof(ObjectOffset) + " and " + nameof(ObjectLength) +
                                               "cannot be less than 0.");

            if (ObjectOffset == 0 && ObjectLength == 0)
                throw new InvalidDataException("Either " + nameof(ObjectOffset) + " or " + nameof(ObjectLength) +
                                               " must be greater than 0.");
        }

        Populate();
    }

    private void Populate()
    {
        Headers ??= new Dictionary<string, string>(StringComparer.Ordinal);
        if (OffsetLengthSet)
        {
            // "Range" header accepts byte start index and end index
            if (ObjectLength > 0 && ObjectOffset > 0)
                Headers["Range"] = "bytes=" + ObjectOffset + "-" + (ObjectOffset + ObjectLength - 1);
            else if (ObjectLength == 0 && ObjectOffset > 0)
                Headers["Range"] = "bytes=" + ObjectOffset + "-";
            else if (ObjectLength > 0 && ObjectOffset == 0) Headers["Range"] = "bytes=0-" + (ObjectLength - 1);
        }
    }

    public StatObjectArgs WithOffsetAndLength(long offset, long length)
    {
        OffsetLengthSet = true;
        ObjectOffset = offset < 0 ? 0 : offset;
        ObjectLength = length < 0 ? 0 : length;
        return this;
    }

    public StatObjectArgs WithLength(long length)
    {
        OffsetLengthSet = true;
        ObjectOffset = 0;
        ObjectLength = length < 0 ? 0 : length;
        return this;
    }

    internal override HttpRequestMessageBuilder BuildRequest(HttpRequestMessageBuilder requestMessageBuilder)
    {
        requestMessageBuilder.AddQueryParameter("prefix", Prefix);

        return requestMessageBuilder;
    }
}
