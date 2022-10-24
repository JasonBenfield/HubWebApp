namespace XTI_HubWebAppApi.AppInstall;

public sealed class SetUserAccessRequest
{
    public AppUserName UserName { get; set; } = AppUserName.Anon;
    public SetUserAccessRoleRequest[] RoleAssignments { get; set; } = new SetUserAccessRoleRequest[0];
}
