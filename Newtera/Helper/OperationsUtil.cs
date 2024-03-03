/*
 * Newtera .NET Library for Newtera TDM, (C) 2020 Newtera, Inc.
 *
 */

namespace Newtera.Helper;

public static class OperationsUtil
{
    private static readonly List<string> supportedHeaders = new()
    {
        "cache-control",
        "content-encoding",
        "content-type",
        "x-amz-acl",
        "content-disposition",
        "x-newtera-extract"
    };

    internal static bool IsSupportedHeader(string hdr, IEqualityComparer<string> comparer = null)
    {
        comparer ??= StringComparer.OrdinalIgnoreCase;
        return supportedHeaders.Contains(hdr, comparer);
    }
}
