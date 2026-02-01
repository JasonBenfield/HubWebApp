namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class SetUserAccessAction : AppAction<SetUserAccessRequest, EmptyActionResult>
{
    private readonly EfHubDB hubFactory;
    private readonly IUserCacheManagement userCacheManagement;

    public SetUserAccessAction(EfHubDB hubFactory, IUserCacheManagement userCacheManagement)
    {
        this.hubFactory = hubFactory;
        this.userCacheManagement = userCacheManagement;
    }

    public async Task<EmptyActionResult> Execute(SetUserAccessRequest model, CancellationToken stoppingToken)
    {
        var user = await hubFactory.Users.UserByUserName(new AppUserName(model.UserName), stoppingToken);
        foreach (var assignment in model.RoleAssignments)
        {
            var app = await hubFactory.Apps.App(assignment.AppKey.ToAppKey(), stoppingToken);
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
