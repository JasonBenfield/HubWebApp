using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppUserMaintenance;

internal sealed class DenyAccessAction : AppAction<UserModifierKey, EmptyActionResult>
{
    private readonly AppFromPath appFromPath;
    private readonly AppFactory appFactory;
    private readonly ICachedUserContext userContext;

    public DenyAccessAction(AppFromPath appFromPath, AppFactory appFactory, ICachedUserContext userContext)
    {
        this.appFromPath = appFromPath;
        this.appFactory = appFactory;
        this.userContext = userContext;
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
            await user.Modifier(modifier).UnassignRole(role);
        }
        await user.Modifier(modifier).AssignRole(denyAccessRole);
        userContext.ClearCache(user.UserName());
        return new EmptyActionResult();
    }
}