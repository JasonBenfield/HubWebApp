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
        await hubClient.Install.SetUserAccess
        (
            new SetUserAccessRequest
            {
                UserID = systemUser.ID,
                RoleAssignments = new[]
                {
                    new SetUserAccessRoleRequest
                    {
                        AppKey = AppKey.WebApp(hubClient.AppName),
                        RoleNames = new []
                        {
                            new AppRoleName(hubClient.RoleNames.PermanentLog)
                        }
                    }
                }
            }
        );
    }
}
