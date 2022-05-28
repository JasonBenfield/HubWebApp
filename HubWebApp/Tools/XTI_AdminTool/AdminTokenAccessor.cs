using XTI_Admin;
using XTI_HubAppClient;
using XTI_HubAppClient.Extensions;
using XTI_WebAppClient;

namespace XTI_AdminTool;

internal sealed class AdminTokenAccessor : IAdminTokenAccessor
{
    private readonly HubAppClient hubClient;

    public AdminTokenAccessor(HubAppClient hubClient)
    {
        this.hubClient = hubClient;
    }

    public void UseAnonymousToken() => hubClient.UseToken<AnonymousXtiToken>();

    public void UseInstallerToken() => hubClient.UseToken<InstallationUserXtiToken>();
}
