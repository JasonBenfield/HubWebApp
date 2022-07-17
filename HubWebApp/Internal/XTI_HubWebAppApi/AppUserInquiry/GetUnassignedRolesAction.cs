namespace XTI_HubWebAppApi.AppUserInquiry;

public sealed class GetUnassignedRolesAction : AppAction<UserModifierKey, AppRoleModel[]>
{
    private readonly AppFromPath appFromPath;
    private readonly HubFactory factory;

    public GetUnassignedRolesAction(AppFromPath appFromPath, HubFactory factory)
    {
        this.appFromPath = appFromPath;
        this.factory = factory;
    }

    public async Task<AppRoleModel[]> Execute(UserModifierKey model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var user = await factory.Users.User(model.UserID);
        var modifier = await app.Modifier(model.ModifierID);
        var unassignedRoles = await user.Modifier(modifier).ExplicitlyUnassignedRoles();
        return unassignedRoles
            .Where(role => !role.IsDenyAccess())
            .Select(role => role.ToModel())
            .ToArray();
    }
}