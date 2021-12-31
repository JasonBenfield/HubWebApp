using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppUserInquiry;

public sealed class GetUnassignedRolesAction : AppAction<GetUnassignedRolesRequest, AppRoleModel[]>
{
    private readonly AppFromPath appFromPath;
    private readonly AppFactory factory;

    public GetUnassignedRolesAction(AppFromPath appFromPath, AppFactory factory)
    {
        this.appFromPath = appFromPath;
        this.factory = factory;
    }

    public async Task<AppRoleModel[]> Execute(GetUnassignedRolesRequest model)
    {
        var app = await appFromPath.Value();
        var user = await factory.Users.User(model.UserID);
        var modifier = await app.Modifier(model.ModifierID);
        var unassignedRoles = await user.ExplicitlyUnassignedRoles(modifier);
        return unassignedRoles
            .Where(role => !role.Name().Equals(AppRoleName.DenyAccess))
            .Select(role => role.ToModel())
            .ToArray();
    }
}