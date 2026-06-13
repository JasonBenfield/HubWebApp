namespace XTI_HubWebAppApiActions.System;

public sealed class GetUsersWithAnyRoleAction : AppAction<SystemGetUsersWithAnyRoleRequest, AppUserModel[]>
{
    private readonly AppFromSystemUser appFromSystemUser;
    private readonly EfHubDB db;

    public GetUsersWithAnyRoleAction(AppFromSystemUser appFromSystemUser, EfHubDB db)
    {
        this.appFromSystemUser = appFromSystemUser;
        this.db = db;
    }

    public async Task<AppUserModel[]> Execute(SystemGetUsersWithAnyRoleRequest requestData, CancellationToken stoppingToken)
    {
        var efApp = await appFromSystemUser.App(stoppingToken);
        var efModCategroy = await efApp.ModCategory(new ModifierCategoryName(requestData.ModCategoryName), stoppingToken);
        var efModifier = await efModCategroy.ModifierByModKey(new ModifierKey(requestData.ModKey), stoppingToken);
        var roleNames = requestData.RoleNames.Select(rn => new AppRoleName(rn)).ToArray();
        var efRoles = await efApp.Roles(roleNames, stoppingToken);
        var efUsers = await db.Users.UsersWithAnyRole(efModifier, efRoles, stoppingToken);
        return efUsers.Select(u => u.ToModel()).ToArray();
    }
}
