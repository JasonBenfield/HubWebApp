using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppUserMaintenance;

internal sealed class AssignRoleAction : AppAction<UserRoleRequest, int>
{
    private readonly AppFromPath appFromPath;
    private readonly AppFactory appFactory;

    public AssignRoleAction(AppFromPath appFromPath, AppFactory appFactory)
    {
        this.appFromPath = appFromPath;
        this.appFactory = appFactory;
    }

    public async Task<int> Execute(UserRoleRequest model)
    {
        var app = await appFromPath.Value();
        var role = await app.Role(model.RoleID);
        var user = await appFactory.Users.User(model.UserID);
        var modifier = await app.Modifier(model.ModifierID);
        await user.Modifier(modifier).AddRole(role);
        return role.ID.Value;
    }
}