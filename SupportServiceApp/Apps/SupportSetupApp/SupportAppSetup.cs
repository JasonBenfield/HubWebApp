using XTI_App.Abstractions;
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
        var systemUser = await hubClient.UserInquiry.GetUserByUserName(systemUserName.Value.Value);
        var app = await hubClient.Apps.GetAppByAppKey
        (
            new GetAppByAppKeyRequest { AppKey = AppKey.WebApp("Hub") }
        );
        var permLogRole = await hubClient.App.GetRole(app.ModKey, "PermanentLog");
        await hubClient.AppUserMaintenance.AssignRole
        (
            app.ModKey, 
            new UserRoleRequest
            {
                UserID = systemUser.ID,
                RoleID = permLogRole.ID
            }
        );
    }
}
