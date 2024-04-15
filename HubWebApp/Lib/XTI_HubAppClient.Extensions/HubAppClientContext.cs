using XTI_App.Abstractions;

namespace XTI_HubAppClient.Extensions;

public sealed class HubAppClientContext
{
    public HubAppClientContext(SystemHubAppClient systemHubClient, InstallationIDAccessor installationIDAccessor, ICurrentUserName currentUserName)
    {
        AppContext = new HcAppContext(systemHubClient.Value, installationIDAccessor);
        UserContext = new HcUserContext(systemHubClient.Value, currentUserName);
    }

    public HcAppContext AppContext { get; }

    public HcUserContext UserContext { get; }
}