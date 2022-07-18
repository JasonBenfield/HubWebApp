namespace XTI_HubWebAppApi.AppInstall;

public sealed class SetUserAccessRequest
{
    public AppUserName UserName { get; set; } = AppUserName.Anon;
    public SetUserAccessRoleRequest[] RoleAssignments { get; set; } = new SetUserAccessRoleRequest[0];
}

public sealed class SetUserAccessRoleRequest
{
    public AppKey AppKey { get; set; } = AppKey.Unknown;
    public AppRoleName[] RoleNames { get; set; } = new AppRoleName[0];
}

internal sealed class SetUserAccessAction : AppAction<SetUserAccessRequest, EmptyActionResult>
{
    private readonly HubFactory hubFactory;

    public SetUserAccessAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<EmptyActionResult> Execute(SetUserAccessRequest model, CancellationToken stoppingToken)
    {
        var user = await hubFactory.Users.UserByUserName(model.UserName);
        foreach (var assignment in model.RoleAssignments)
        {
            var app = await hubFactory.Apps.App(assignment.AppKey);
            foreach (var roleName in assignment.RoleNames)
            {
                var role = await app.Role(roleName);
                await user.AssignRole(role);
            }
        }
        return new EmptyActionResult();
    }
}
