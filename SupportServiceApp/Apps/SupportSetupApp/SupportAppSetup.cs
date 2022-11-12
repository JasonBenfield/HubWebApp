using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubAppClient;
using XTI_SupportServiceAppApi;

namespace SupportSetupApp;

internal sealed class SupportAppSetup : IAppSetup
{
    private readonly HubAppClient hubClient;

    public SupportAppSetup(HubAppClient hubClient)
    {
        this.hubClient = hubClient;
    }

    public async Task Run(AppVersionKey versionKey)
    {
        var systemUserName = new SystemUserName(SupportInfo.AppKey, Environment.MachineName);
        await hubClient.Install.SetUserAccess
        (
            new SetUserAccessRequest
            (
                systemUserName.Value,
                new SetUserAccessRoleRequest
                (
                    AppKey.WebApp(hubClient.AppName),
                    new AppRoleName(hubClient.RoleNames.PermanentLog)
                )
            )
        );
    }
}
