namespace XTI_HubWebAppApi.System;

internal sealed class SystemSetUserAccessAction : AppAction<SystemSetUserAccessRequest, EmptyActionResult>
{
    private readonly AppFromSystemUser appFromSystemUser;
    private readonly HubFactory hubFactory;

    public SystemSetUserAccessAction(AppFromSystemUser appFromSystemUser, HubFactory hubFactory)
    {
        this.appFromSystemUser = appFromSystemUser;
        this.hubFactory = hubFactory;
    }

    public async Task<EmptyActionResult> Execute(SystemSetUserAccessRequest model, CancellationToken stoppingToken)
    {
        var appContextModel = await appFromSystemUser.App(model.InstallationID);
        var app = await hubFactory.Apps.App(appContextModel.App.ID);
        var user = await hubFactory.Users.UserByUserName(new AppUserName(model.UserName));
        foreach (var assignment in model.RoleAssignments)
        {
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
