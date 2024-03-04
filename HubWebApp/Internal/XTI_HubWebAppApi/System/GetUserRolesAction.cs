namespace XTI_HubWebAppApi.System;

internal sealed class GetUserRolesAction : AppAction<GetUserRolesRequest, AppRoleModel[]>
{
    private readonly HubFactory hubFactory;

    public GetUserRolesAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AppRoleModel[]> Execute(GetUserRolesRequest getRequest, CancellationToken stoppingToken)
    {
        var user = await hubFactory.Users.User(getRequest.UserID);
        var modifier = await hubFactory.Modifiers.Modifier(getRequest.ModifierID);
        var roles = await user.Modifier(modifier).AssignedRoles();
        return roles.Select(r => r.ToModel()).ToArray();
    }
}
