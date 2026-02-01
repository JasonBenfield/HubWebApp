namespace XTI_HubWebAppApiActions.System;

public sealed class SetUserAccessAction : AppAction<SystemSetUserAccessRequest, EmptyActionResult>
{
    private readonly AppFromSystemUser appFromSystemUser;
    private readonly EfHubDB hubFactory;
    private readonly IUserCacheManagement userCacheManagement;

    public SetUserAccessAction(AppFromSystemUser appFromSystemUser, EfHubDB hubFactory, IUserCacheManagement userCacheManagement)
    {
        this.appFromSystemUser = appFromSystemUser;
        this.hubFactory = hubFactory;
        this.userCacheManagement = userCacheManagement;
    }

    public async Task<EmptyActionResult> Execute(SystemSetUserAccessRequest model, CancellationToken stoppingToken)
    {
        var appContextModel = await appFromSystemUser.App(model.InstallationID, stoppingToken);
        var app = await hubFactory.Apps.App(appContextModel.App.ID, stoppingToken);
        var user = await hubFactory.Users.UserByUserName(new AppUserName(model.UserName), stoppingToken);
        foreach (var assignment in model.RoleAssignments)
        {
            var modCategory = await app.ModCategory(new ModifierCategoryName(assignment.ModCategoryName), stoppingToken);
            var modifier = await modCategory.ModifierByModKey(new ModifierKey(assignment.ModKey), stoppingToken);
            foreach (var roleName in assignment.RoleNames)
            {
                var role = await app.Role(new AppRoleName(roleName), stoppingToken);
                await user.Modifier(modifier).AssignRole(role, stoppingToken);
            }
        }
        await userCacheManagement.ClearCache(user.ToModel().UserName, stoppingToken);
        return new EmptyActionResult();
    }
}
