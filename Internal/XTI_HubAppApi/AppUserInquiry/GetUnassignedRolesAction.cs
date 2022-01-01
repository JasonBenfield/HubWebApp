using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppUserInquiry;

public sealed class GetUnassignedRolesAction : AppAction<UserModifierKey, AppRoleModel[]>
{
    private readonly AppFromPath appFromPath;
    private readonly AppFactory factory;

    public GetUnassignedRolesAction(AppFromPath appFromPath, AppFactory factory)
    {
        this.appFromPath = appFromPath;
        this.factory = factory;
    }

    public async Task<AppRoleModel[]> Execute(UserModifierKey model)
    {
        var app = await appFromPath.Value();
        var user = await factory.Users.User(model.UserID);
        var modifier = await app.Modifier(model.ModifierID);
        var unassignedRoles = await user.Modifier(modifier).ExplicitlyUnassignedRoles();
        return unassignedRoles
            .Where(role => !role.Name().Equals(AppRoleName.DenyAccess))
            .Select(role => role.ToModel())
            .ToArray();
    }
}