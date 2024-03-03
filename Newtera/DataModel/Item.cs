/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Globalization;

namespace Newtera.DataModel;

[Serializable]
public class Item
{
    private string etag;

    public string Key { get; set; }
    public string LastModified { get; set; }

    public string ETag
    {
        get => etag;
        set
        {
            if (value is not null)
                etag = value.Replace("\"", string.Empty, StringComparison.OrdinalIgnoreCase);
            else
                etag = null;
        }
    }

    public ulong Size { get; set; }

    public bool IsDir { get; set; }

    public string VersionId { get; set; }
    public bool IsLatest { get; set; }

    public DateTime? LastModifiedDateTime
    {
        get
        {
            DateTime? dt = null;
            if (!string.IsNullOrEmpty(LastModified)) dt = DateTime.Parse(LastModified, CultureInfo.InvariantCulture);
            return dt;
        }
    }
}
