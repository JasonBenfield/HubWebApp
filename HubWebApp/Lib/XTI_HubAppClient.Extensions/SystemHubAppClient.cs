namespace XTI_HubAppClient.Extensions;

public sealed class SystemHubAppClient
{
    public SystemHubAppClient(HubAppClientFactory hubClientFactory)
    {
        Value = hubClientFactory.Create();
        Value.UseToken<SystemUserXtiToken>();
    }

    public HubAppClient Value { get; }
}
