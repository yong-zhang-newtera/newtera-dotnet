/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

namespace Newtera.DataModel;

[Serializable]
public class Part
{
    private string etag;

    public int PartNumber { get; set; }
    public long Size { get; set; }
    public DateTime LastModified { get; set; }

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

    public long PartSize()
    {
        return Size;
    }
}
