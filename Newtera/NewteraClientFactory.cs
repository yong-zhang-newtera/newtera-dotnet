/*
 * Newtera .NET Library for Newtera TDM, (C) 2017 Newtera, Inc.
 *
 */

namespace Newtera;

public class NewteraClientFactory : INewteraClientFactory
{
    private const string DefaultEndpoint = "play.min.io";
    private readonly Action<INewteraClient> defaultConfigureClient;

    public NewteraClientFactory(Action<INewteraClient> defaultConfigureClient)
    {
        this.defaultConfigureClient =
            defaultConfigureClient ?? throw new ArgumentNullException(nameof(defaultConfigureClient));
    }

    public INewteraClient CreateClient()
    {
        return CreateClient(defaultConfigureClient);
    }

    public INewteraClient CreateClient(Action<INewteraClient> configureClient)
    {
        if (configureClient == null) throw new ArgumentNullException(nameof(configureClient));

        var client = new NewteraClient()
            .WithSSL();

        configureClient(client);

        if (string.IsNullOrEmpty(client.Config.Endpoint))
            _ = client.WithEndpoint(DefaultEndpoint);

        _ = client.Build();


        return client;
    }
}
