/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Globalization;

namespace Newtera.DataModel;

[Serializable]
public class Bucket
{
    public string Name { get; set; }
    public string CreationDate { get; set; }

    public DateTime CreationDateDateTime => DateTime.Parse(CreationDate, CultureInfo.InvariantCulture);
}
