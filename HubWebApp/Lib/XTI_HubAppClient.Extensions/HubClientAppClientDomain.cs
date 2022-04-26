using XTI_WebApp.Abstractions;

namespace XTI_HubAppClient.Extensions;

internal sealed class HubClientAppClientDomain : IAppClientDomain
{
    private readonly HubAppClient hubClient;

    public HubClientAppClientDomain(HubAppClient hubClient)
    {
        this.hubClient = hubClient;
    }

    public Task<string> Value(string appName, string version) =>
        hubClient.Apps.GetAppDomain
        (
            new GetAppDomainRequest { AppName = appName, Version = version }
        );
}