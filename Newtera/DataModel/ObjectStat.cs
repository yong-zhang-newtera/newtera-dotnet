/*
 * Newtera .NET Library for Newtera TDM, (C) 2017-2021 Newtera, Inc.
 *
 */

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtera.Helper;

namespace Newtera.DataModel;

public class ObjectStat
{
    private ObjectStat()
    {
        MetaData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        ExtraHeaders = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }

    public string ObjectName { get; private set; }
    public long Size { get; private set; }
    public DateTime LastModified { get; private set; }
    public string ETag { get; private set; }
    public string ContentType { get; private set; }

    [SuppressMessage("Design", "MA0016:Prefer returning collection abstraction instead of implementation",
        Justification = "Needs to be concrete type for XML deserialization")]
    public Dictionary<string, string> MetaData { get; }

    public string VersionId { get; private set; }
    public bool DeleteMarker { get; private set; }

    [SuppressMessage("Design", "MA0016:Prefer returning collection abstraction instead of implementation",
        Justification = "Needs to be concrete type for XML deserialization")]
    public Dictionary<string, string> ExtraHeaders { get; }

    public uint? TaggingCount { get; private set; }
    public string ArchiveStatus { get; private set; }
    public DateTime? Expires { get; private set; }
    public string ReplicationStatus { get; }
    public DateTime? ObjectLockRetainUntilDate { get; private set; }
    public bool? LegalHoldEnabled { get; private set; }

    public static ObjectStat FromResponseHeaders(string objectName, IDictionary<string, string> responseHeaders)
    {
        if (string.IsNullOrEmpty(objectName))
            throw new ArgumentNullException(nameof(objectName), "Name of an object cannot be empty");
        if (responseHeaders is null) throw new ArgumentNullException(nameof(responseHeaders));

        var objInfo = new ObjectStat { ObjectName = objectName };
        foreach (var paramName in responseHeaders.Keys)
        {
            var paramValue = responseHeaders[paramName];
            switch (paramName.ToLowerInvariant())
            {
                case "content-length":
                    objInfo.Size = long.Parse(paramValue, NumberStyles.Number, CultureInfo.InvariantCulture);
                    break;
                case "last-modified":
                    objInfo.LastModified = DateTime.Parse(paramValue, CultureInfo.InvariantCulture);
                    break;
                case "etag":
                    objInfo.ETag = paramValue.Replace("\"", string.Empty, StringComparison.OrdinalIgnoreCase);
                    break;
                case "content-type":
                    objInfo.ContentType = paramValue;
                    objInfo.MetaData["Content-Type"] = objInfo.ContentType;
                    break;
                default:
                    if (OperationsUtil.IsSupportedHeader(paramName))
                        objInfo.MetaData[paramName] = paramValue;
                    else if (paramName.StartsWith("newtera-meta-", StringComparison.OrdinalIgnoreCase))
                        objInfo.MetaData[paramName["newtera-meta-".Length..]] = paramValue;
                    else
                        objInfo.ExtraHeaders[paramName] = paramValue;
                    break;
            }
        }

        return objInfo;
    }

    public override string ToString()
    {
        var versionInfo = "VersionId(None)";
        var legalHold = "LegalHold(None)";
        var taggingCount = "Tagging-Count(0)";
        var expires = "Expiry(None)";
        var objectLockInfo = "ObjectLock(None)";
        var archiveStatus = "Archive Status(None)";
        var replicationStatus = "Replication Status(None)";
        if (!string.IsNullOrWhiteSpace(VersionId))
        {
            versionInfo = $"Version ID({VersionId})";
            if (DeleteMarker) versionInfo = $"Version ID({VersionId}, deleted)";
        }

        if (Expires is not null) expires = "Expiry(" + Utils.To8601String(Expires.Value) + ")";

        if (TaggingCount is not null) taggingCount = "Tagging-Count(" + TaggingCount.Value + ")";
        if (LegalHoldEnabled is not null)
            legalHold = "LegalHold(" + (LegalHoldEnabled.Value ? "Enabled" : "Disabled") + ")";
        if (!string.IsNullOrWhiteSpace(ReplicationStatus))
            replicationStatus = "Replication Status(" + ReplicationStatus + ")";
        if (!string.IsNullOrWhiteSpace(ArchiveStatus)) archiveStatus = "Archive Status(" + ArchiveStatus + ")";
        var lineTwo = $"{expires} {objectLockInfo} {legalHold} {taggingCount} {archiveStatus} {replicationStatus}";

        return
            $"{ObjectName} : {versionInfo} Size({Size}) LastModified({LastModified}) ETag({ETag}) Content-Type({ContentType})" +
            (string.IsNullOrWhiteSpace(lineTwo) ? "" : "\n" + lineTwo);
    }
}
