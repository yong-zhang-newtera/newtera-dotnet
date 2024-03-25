/*
 * Newtera .NET Library for Newtera TDM, (C) 2020, 2021 Newtera, Inc.
 *
 */

using Newtera.Helper;

namespace Newtera.DataModel.Args;

internal class NewMultipartUploadCopyArgs : NewMultipartUploadArgs<NewMultipartUploadCopyArgs>
{
    internal bool ReplaceMetadataDirective { get; set; }
    internal bool ReplaceTagsDirective { get; set; }
    internal ObjectStat SourceObjectInfo { get; set; }

    internal override void Validate()
    {
        base.Validate();
        if (SourceObjectInfo is null)
            throw new InvalidOperationException(nameof(SourceObjectInfo) +
                                                " need to be initialized for a NewMultipartUpload operation to work.");

        Populate();
    }

    private void Populate()
    {
        //Concat as Headers may have byte range info .etc.
        if (!ReplaceMetadataDirective && SourceObjectInfo.MetaData?.Count > 0)
            Headers = SourceObjectInfo.MetaData.Concat(Headers).GroupBy(item => item.Key, StringComparer.Ordinal)
                .ToDictionary(item => item.Key, item => item.First().Value, StringComparer.Ordinal);
        else if (ReplaceMetadataDirective) Headers ??= new Dictionary<string, string>(StringComparer.Ordinal);
        if (Headers is not null)
        {
            var newKVList = new List<Tuple<string, string>>();
            foreach (var item in Headers)
            {
                var key = item.Key;
                if (!OperationsUtil.IsSupportedHeader(item.Key) &&
                    !item.Key.StartsWith("newtera-meta", StringComparison.OrdinalIgnoreCase))
                    newKVList.Add(new Tuple<string, string>("newtera-meta-" + key.ToLowerInvariant(), item.Value));
            }

            foreach (var item in newKVList) Headers[item.Item1] = item.Item2;
        }
    }

    public new NewMultipartUploadCopyArgs WithHeaders(IDictionary<string, string> headers)
    {
        _ = base.WithHeaders(headers);
        return this;
    }

    internal NewMultipartUploadCopyArgs WithReplaceMetadataDirective(bool replace)
    {
        ReplaceMetadataDirective = replace;
        return this;
    }

    internal NewMultipartUploadCopyArgs WithReplaceTagsDirective(bool replace)
    {
        ReplaceTagsDirective = replace;
        return this;
    }

    public NewMultipartUploadCopyArgs WithSourceObjectInfo(ObjectStat stat)
    {
        SourceObjectInfo = stat;
        return this;
    }

    internal override HttpRequestMessageBuilder BuildRequest(HttpRequestMessageBuilder requestMessageBuilder)
    {
        requestMessageBuilder.AddQueryParameter("uploads", "");

        if (ReplaceMetadataDirective)
            requestMessageBuilder.AddOrUpdateHeaderParameter("newtera-metadata-directive", "REPLACE");

        return requestMessageBuilder;
    }
}
