/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

using System.Text;

namespace Newtera.Tests;

internal static class TestHelper
{
    internal const string Endpoint = "localhost";
    internal const string AccessKey = "demo1";
    internal const string SecretKey = "888";

    private static readonly Random rnd = new();

    // Generate a random string
    public static string GetRandomName(int length = 5)
    {
        var characters = "0123456789abcdefghijklmnopqrstuvwxyz";
        var result = new StringBuilder(length);

        for (var i = 0; i < length; i++) _ = result.Append(characters[rnd.Next(characters.Length)]);
        return result.ToString();
    }
}
