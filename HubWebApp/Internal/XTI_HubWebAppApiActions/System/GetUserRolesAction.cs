namespace XTI_HubWebAppApiActions.System;

public sealed class GetUserRolesAction : AppAction<GetUserRolesRequest, AppRoleModel[]>
{
    private readonly EfHubDB db;

    public GetUserRolesAction(EfHubDB db)
    {
        this.db = db;
    }

    public async Task<AppRoleModel[]> Execute(GetUserRolesRequest getRequest, CancellationToken stoppingToken)
    {
        var user = await db.Users.User(getRequest.UserID, stoppingToken);
        var modifier = await db.Modifiers.Modifier(getRequest.ModifierID, stoppingToken);
        var roles = await user.Modifier(modifier).AssignedRoles(stoppingToken);
        return roles.Select(r => r.ToModel()).ToArray();
    }
}
