namespace XTI_HubWebAppApiActions.System;

public sealed class GetUsersWithAnyRoleAction : AppAction<SystemGetUsersWithAnyRoleRequest, AppUserModel[]>
{
    private readonly AppFromSystemUser appFromSystemUser;
    private readonly EfHubDB hubFactory;

    public GetUsersWithAnyRoleAction(AppFromSystemUser appFromSystemUser, EfHubDB hubFactory)
    {
        this.appFromSystemUser = appFromSystemUser;
        this.hubFactory = hubFactory;
    }

    public async Task<AppUserModel[]> Execute(SystemGetUsersWithAnyRoleRequest model, CancellationToken stoppingToken)
    {
        var appContextModel = await appFromSystemUser.App(model.InstallationID, stoppingToken);
        var app = await hubFactory.Apps.App(appContextModel.App.ID, stoppingToken);
        var modCategroy = await app.ModCategory(new ModifierCategoryName(model.ModCategoryName), stoppingToken);
        var modifier = await modCategroy.ModifierByModKey(new ModifierKey(model.ModKey), stoppingToken);
        var roleNames = model.RoleNames.Select(rn => new AppRoleName(rn)).ToArray();
        var roles = await app.Roles(roleNames, stoppingToken);
        var users = await hubFactory.Users.UsersWithAnyRole(modifier, roles, stoppingToken);
        return users.Select(u => u.ToModel()).ToArray();
    }
}
