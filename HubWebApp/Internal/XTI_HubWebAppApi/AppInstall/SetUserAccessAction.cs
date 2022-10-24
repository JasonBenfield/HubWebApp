namespace XTI_HubWebAppApi.AppInstall;

internal sealed class SetUserAccessAction : AppAction<SetUserAccessRequest, EmptyActionResult>
{
    private readonly HubFactory hubFactory;

    public SetUserAccessAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<EmptyActionResult> Execute(SetUserAccessRequest model, CancellationToken stoppingToken)
    {
        var user = await hubFactory.Users.UserByUserName(new AppUserName(model.UserName));
        foreach (var assignment in model.RoleAssignments)
        {
            var app = await hubFactory.Apps.App(assignment.AppKey);
            var modCategory = await app.ModCategory(new ModifierCategoryName(assignment.ModCategoryName));
            var modifier = await modCategory.ModifierByModKey(new ModifierKey(assignment.ModKey));
            foreach (var roleName in assignment.RoleNames)
            {
                var role = await app.Role(new AppRoleName(roleName));
                await user.Modifier(modifier).AssignRole(role);
            }
        }
        return new EmptyActionResult();
    }
}
