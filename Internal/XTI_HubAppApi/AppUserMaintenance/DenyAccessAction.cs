using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppUserMaintenance;

internal sealed class DenyAccessAction : AppAction<UserModifierKey, EmptyActionResult>
{
    private readonly AppFromPath appFromPath;
    private readonly AppFactory appFactory;

    public DenyAccessAction(AppFromPath appFromPath, AppFactory appFactory)
    {
        this.appFromPath = appFromPath;
        this.appFactory = appFactory;
    }

    public async Task<EmptyActionResult> Execute(UserModifierKey model)
    {
        var app = await appFromPath.Value();
        var denyAccessRole = await app.Role(AppRoleName.DenyAccess);
        var user = await appFactory.Users.User(model.UserID);
        var modifier = await app.Modifier(model.ModifierID);
        var existingRoles = await user.Modifier(modifier).ExplicitlyAssignedRoles();
        foreach(var role in existingRoles)
        {
            await user.Modifier(modifier).RemoveRole(role);
        }
        await user.Modifier(modifier).AddRole(denyAccessRole);
        return new EmptyActionResult();
    }
}