namespace XTI_HubWebAppApi.AppInstall;

public sealed class SetUserAccessRoleRequest
{
    public AppKey AppKey { get; set; } = AppKey.Unknown;
    public AppRoleName[] RoleNames { get; set; } = new AppRoleName[0];
}
