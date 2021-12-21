using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppUserMaintenance;

public sealed class UnassignRoleAction : AppAction<UserRoleRequest, EmptyActionResult>
{
    private readonly AppFromPath appFromPath;
    private readonly AppFactory factory;

    public UnassignRoleAction(AppFromPath appFromPath, AppFactory factory)
    {
        this.appFromPath = appFromPath;
        this.factory = factory;
    }

    public async Task<EmptyActionResult> Execute(UserRoleRequest model)
    {
        var app = await appFromPath.Value();
        var user = await factory.Users.User(model.UserID);
        var role = await app.Role(model.RoleID);
        await user.RemoveRole(role);
        return new EmptyActionResult();
    }
}