namespace XTI_HubWebAppApiActions.System;

public sealed class SetUserAccessAction : AppAction<SystemSetUserAccessRequest, EmptyActionResult>
{
    private readonly AppFromSystemUser appFromSystemUser;
    private readonly EfHubDB db;
    private readonly IUserCacheManagement userCacheManagement;

    public SetUserAccessAction(AppFromSystemUser appFromSystemUser, EfHubDB db, IUserCacheManagement userCacheManagement)
    {
        this.appFromSystemUser = appFromSystemUser;
        this.db = db;
        this.userCacheManagement = userCacheManagement;
    }

    public async Task<EmptyActionResult> Execute(SystemSetUserAccessRequest requestData, CancellationToken stoppingToken)
    {
        var app = await appFromSystemUser.App(stoppingToken);
        var user = await db.Users.UserByUserName(new AppUserName(requestData.UserName), stoppingToken);
        foreach (var assignment in requestData.RoleAssignments)
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
