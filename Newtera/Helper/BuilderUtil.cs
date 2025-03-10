﻿/*
 * Newtera .NET Library for Newtera TDM, (C) 2020 Newtera, Inc.
 *
 */

using System.Globalization;
using System.Net;

namespace Newtera.Helper;

public static class BuilderUtil
{
    public static bool IsAwsDualStackEndpoint(string endpoint)
    {
        if (string.IsNullOrEmpty(endpoint))
            throw new ArgumentException($"'{nameof(endpoint)}' cannot be null or empty.", nameof(endpoint));

        return endpoint.Contains(".dualstack.", StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsAwsAccelerateEndpoint(string endpoint)
    {
        if (string.IsNullOrEmpty(endpoint))
            throw new ArgumentException($"'{nameof(endpoint)}' cannot be null or empty.", nameof(endpoint));

        return endpoint.StartsWith("s3-accelerate.", StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsAwsEndpoint(string endpoint)
    {
        if (string.IsNullOrEmpty(endpoint))
            throw new ArgumentException($"'{nameof(endpoint)}' cannot be null or empty.", nameof(endpoint));

        return (endpoint.StartsWith("s3.", StringComparison.OrdinalIgnoreCase) ||
                IsAwsAccelerateEndpoint(endpoint)) &&
               (endpoint.EndsWith(".amazonaws.com", StringComparison.OrdinalIgnoreCase) ||
                endpoint.EndsWith(".amazonaws.com.cn", StringComparison.OrdinalIgnoreCase));
    }

    public static bool IsChineseDomain(string host)
    {
        if (string.IsNullOrEmpty(host))
            throw new ArgumentException($"'{nameof(host)}' cannot be null or empty.", nameof(host));

        return host.EndsWith(".cn", StringComparison.OrdinalIgnoreCase);
    }

    public static string ExtractRegion(string endpoint)
    {
        if (string.IsNullOrEmpty(endpoint))
            throw new ArgumentException($"'{nameof(endpoint)}' cannot be null or empty.", nameof(endpoint));

        var tokens = endpoint.Split('.');
        if (tokens.Length < 2)
            return null;
        var token = tokens[1];

        // If token is "dualstack", then region might be in next token.
        if (token.Equals("dualstack", StringComparison.OrdinalIgnoreCase) && tokens.Length >= 3)
            token = tokens[2];

        // If token is equal to "amazonaws", region is not passed in the endpoint.
        if (token.Equals("amazonaws", StringComparison.OrdinalIgnoreCase))
            return null;

        // Return token as region.
        return token;
    }

    private static bool IsValidSmallInt(string val)
    {
        return byte.TryParse(val, out _);
    }

    private static bool IsValidOctetVal(string val)
    {
        const byte uLimit = 255;
        return byte.Parse(val, NumberStyles.Integer, CultureInfo.InvariantCulture) <= uLimit;
    }

    private static bool IsValidIPv4(string ip)
    {
        var posColon = ip.LastIndexOf(':');
        if (posColon != -1) ip = ip[..posColon];
        var octetsStr = ip.Split('.');
        if (octetsStr.Length != 4) return false;
        var isValidSmallInt = Array.TrueForAll(octetsStr, IsValidSmallInt);
        if (!isValidSmallInt) return false;
        return Array.TrueForAll(octetsStr, IsValidOctetVal);
    }

    private static bool IsValidIP(string host)
    {
        return IPAddress.TryParse(host, out _);
    }

    public static bool IsValidHostnameOrIPAddress(string host)
    {
        // Let's do IP address check first.
        if (string.IsNullOrWhiteSpace(host)) return false;
        // IPv4 first
        if (IsValidIPv4(host)) return true;
        // IPv6 or other IP address format
        if (IsValidIP(host)) return true;
        // Remove any port in endpoint, in such a case.
        var posColon = host.LastIndexOf(':');
        if (posColon != -1)
        {
            try
            {
                var port = int.Parse(host.Substring(posColon + 1, host.Length - posColon - 1),
                    CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return false;
            }

            host = host[..posColon];
        }

        // Check host if it is a hostname.
        return Uri.CheckHostName(host).ToString().Equals("dns", StringComparison.OrdinalIgnoreCase);
    }
}
