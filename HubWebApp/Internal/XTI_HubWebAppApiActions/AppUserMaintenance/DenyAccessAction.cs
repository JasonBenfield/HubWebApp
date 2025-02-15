﻿namespace XTI_HubWebAppApiActions.AppUserMaintenance;

public sealed class DenyAccessAction : AppAction<UserModifierKey, EmptyActionResult>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly HubFactory hubFactory;
    private readonly CurrentAppUser currentUser;
    private readonly IUserCacheManagement userCacheManagement;

    public DenyAccessAction(UserGroupFromPath userGroupFromPath, HubFactory hubFactory, CurrentAppUser currentUser, IUserCacheManagement userCacheManagement)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.hubFactory = hubFactory;
        this.currentUser = currentUser;
        this.userCacheManagement = userCacheManagement;
    }

    public async Task<EmptyActionResult> Execute(UserModifierKey denyRequest, CancellationToken stoppingToken)
    {
        var modifier = await hubFactory.Modifiers.Modifier(denyRequest.ModifierID);
        var app = await modifier.App();
        var permission = await currentUser.GetPermissionsToApp(app);
        if (!permission.CanView)
        {
            throw new AccessDeniedException("Access denied to this user");
        }
        var denyAccessRole = await app.Role(AppRoleName.DenyAccess);
        var userGroup = await userGroupFromPath.Value();
        var user = await userGroup.User(denyRequest.UserID);
        var existingRoles = await user.Modifier(modifier).ExplicitlyAssignedRoles();
        foreach(var role in existingRoles)
        {
            await user.Modifier(modifier).UnassignRole(role);
        }
        await user.Modifier(modifier).AssignRole(denyAccessRole);
        await userCacheManagement.ClearCache(user.ToModel().UserName, stoppingToken);
        return new EmptyActionResult();
    }
}