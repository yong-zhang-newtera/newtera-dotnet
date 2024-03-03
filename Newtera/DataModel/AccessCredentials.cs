/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2021 Newtera, Inc.
 *
 */

using System.Xml.Serialization;
using Newtera.Helper;

namespace Newtera.DataModel;

[Serializable]
[XmlRoot(ElementName = "Credentials")]
public class AccessCredentials
{
    public AccessCredentials(string accessKey, string secretKey,
        string sessionToken, DateTime expiration)
    {
        if (!string.IsNullOrEmpty(accessKey) || !string.IsNullOrEmpty(secretKey) || !string.IsNullOrEmpty(sessionToken))
        {
            AccessKey = accessKey;
            SecretKey = secretKey;
            SessionToken = sessionToken;
            Expiration = expiration.Equals(default) ? null : Utils.To8601String(expiration);
        }
        else
        {
            if (string.IsNullOrEmpty(accessKey))
                throw new ArgumentException($"'{nameof(accessKey)}' cannot be null or empty.", nameof(accessKey));

            if (string.IsNullOrEmpty(secretKey))
                throw new ArgumentException($"'{nameof(secretKey)}' cannot be null or empty.", nameof(secretKey));

            if (string.IsNullOrEmpty(sessionToken))
                throw new ArgumentException($"'{nameof(sessionToken)}' cannot be null or empty.", nameof(sessionToken));
        }
    }

    public AccessCredentials()
    {
    }

    [XmlElement(ElementName = "AccessKeyId", IsNullable = true)]
    public string AccessKey { get; set; }

    [XmlElement(ElementName = "SecretAccessKey", IsNullable = true)]
    public string SecretKey { get; set; }

    [XmlElement(ElementName = "SessionToken", IsNullable = true)]
    public string SessionToken { get; set; }

    // Needs to be stored in ISO8601 format from Datetime
    [XmlElement(ElementName = "Expiration", IsNullable = true)]
    public string Expiration { get; set; }

    public bool AreExpired()
    {
        if (string.IsNullOrWhiteSpace(Expiration)) return false;
        var expiry = Utils.From8601String(Expiration);
        return DateTime.Now.CompareTo(expiry) > 0;
    }
}
