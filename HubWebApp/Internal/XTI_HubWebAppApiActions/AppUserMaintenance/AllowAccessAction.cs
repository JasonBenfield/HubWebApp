namespace XTI_HubWebAppApiActions.AppUserMaintenance;

public sealed class AllowAccessAction : AppAction<UserModifierKey, EmptyActionResult>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly HubFactory hubFactory;
    private readonly CurrentAppUser currentUser;
    private readonly IUserCacheManagement userCacheManagement;

    public AllowAccessAction(UserGroupFromPath userGroupFromPath, HubFactory hubFactory, CurrentAppUser currentUser, IUserCacheManagement userCacheManagement)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.hubFactory = hubFactory;
        this.currentUser = currentUser;
        this.userCacheManagement = userCacheManagement;
    }

    public async Task<EmptyActionResult> Execute(UserModifierKey allowRequest, CancellationToken stoppingToken)
    {
        var modifier = await hubFactory.Modifiers.Modifier(allowRequest.ModifierID, stoppingToken);
        var app = await modifier.App(stoppingToken);
        var permission = await currentUser.GetPermissionsToApp(app, stoppingToken);
        if (!permission.CanView)
        {
            throw new AccessDeniedException("Access denied to this user");
        }
        var denyAccessRole = await app.Role(AppRoleName.DenyAccess, stoppingToken);
        var userGroup = await userGroupFromPath.Value(stoppingToken);
        var user = await userGroup.User(allowRequest.UserID, stoppingToken);
        await user.Modifier(modifier).UnassignRole(denyAccessRole, stoppingToken);
        await userCacheManagement.ClearCache(user.ToModel().UserName, stoppingToken);
        return new EmptyActionResult();
    }
}