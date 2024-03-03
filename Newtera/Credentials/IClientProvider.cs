/*
 * Newtera .NET Library for Newtera TDM,
 * (C) 2021 Newtera, Inc.
 *
 */

using Newtera.DataModel;

namespace Newtera.Credentials;

public interface IClientProvider
{
    AccessCredentials GetCredentials();
    ValueTask<AccessCredentials> GetCredentialsAsync();
}
