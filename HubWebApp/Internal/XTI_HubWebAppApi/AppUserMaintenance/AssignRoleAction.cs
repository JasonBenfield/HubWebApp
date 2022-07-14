namespace XTI_HubWebAppApi.AppUserMaintenance;

internal sealed class AssignRoleAction : AppAction<UserRoleRequest, int>
{
    private readonly AppFromPath appFromPath;
    private readonly HubFactory appFactory;
    private readonly ICachedUserContext userContext;

    public AssignRoleAction(AppFromPath appFromPath, HubFactory appFactory, ICachedUserContext userContext)
    {
        this.appFromPath = appFromPath;
        this.appFactory = appFactory;
        this.userContext = userContext;
    }

    public async Task<int> Execute(UserRoleRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var role = await app.Role(model.RoleID);
        var user = await appFactory.Users.User(model.UserID);
        Modifier modifier;
        if (model.ModifierID > 0)
        {
            modifier = await app.Modifier(model.ModifierID);
        }
        else
        {
            modifier = await app.DefaultModifier();
        }
        await user.Modifier(modifier).AssignRole(role);
        userContext.ClearCache(user.UserName());
        return role.ID;
    }
}