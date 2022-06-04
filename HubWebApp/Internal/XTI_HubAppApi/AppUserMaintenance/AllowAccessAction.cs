namespace XTI_HubAppApi.AppUserMaintenance;

internal sealed class AllowAccessAction : AppAction<UserModifierKey, EmptyActionResult>
{
    private readonly AppFromPath appFromPath;
    private readonly HubFactory appFactory;
    private readonly ICachedUserContext userContext;

    public AllowAccessAction(AppFromPath appFromPath, HubFactory appFactory, ICachedUserContext userContext)
    {
        this.appFromPath = appFromPath;
        this.appFactory = appFactory;
        this.userContext = userContext;
    }

    public async Task<EmptyActionResult> Execute(UserModifierKey model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var denyAccessRole = await app.Role(AppRoleName.DenyAccess);
        var user = await appFactory.Users.User(model.UserID);
        var modifier = await app.Modifier(model.ModifierID);
        await user.Modifier(modifier).UnassignRole(denyAccessRole);
        userContext.ClearCache(user.UserName());
        return new EmptyActionResult();
    }
}