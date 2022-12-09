namespace XTI_HubWebAppApi.System;

internal sealed class GetUsersWithAnyRoleAction : AppAction<GetUsersWithAnyRoleRequest, AppUserModel[]>
{
    private readonly AppFromSystemUser appFromSystemUser;
    private readonly HubFactory hubFactory;

    public GetUsersWithAnyRoleAction(AppFromSystemUser appFromSystemUser, HubFactory hubFactory)
    {
        this.appFromSystemUser = appFromSystemUser;
        this.hubFactory = hubFactory;
    }

    public async Task<AppUserModel[]> Execute(GetUsersWithAnyRoleRequest model, CancellationToken stoppingToken)
    {
        var appContextModel = await appFromSystemUser.App(AppVersionKey.Current);
        var app = await hubFactory.Apps.App(appContextModel.App.ID);
        var modCategroy = await app.ModCategory(new ModifierCategoryName(model.ModCategoryName));
        var modifier = await modCategroy.ModifierByModKey(new ModifierKey(model.ModKey));
        var roleNames = model.RoleNames.Select(rn => new AppRoleName(rn)).ToArray();
        var roles = await app.Roles(roleNames);
        var users = await hubFactory.Users.UsersWithAnyRole(modifier, roles);
        return users.Select(u => u.ToModel()).ToArray();
    }
}
