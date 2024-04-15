using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTI_HubWebAppApi.AppUserInquiry;

internal sealed class GetAssignedRolesAction : AppAction<UserModifierKey, AppRoleModel[]>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly HubFactory factory;
    private readonly CurrentAppUser currentUser;

    public GetAssignedRolesAction(UserGroupFromPath userGroupFromPath, HubFactory factory, CurrentAppUser currentUser)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.factory = factory;
        this.currentUser = currentUser;
    }

    public async Task<AppRoleModel[]> Execute(UserModifierKey model, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value();
        var user = await userGroup.User(model.UserID);
        var modifier = await factory.Modifiers.Modifier(model.ModifierID);
        var app = await modifier.App();
        var permission = await currentUser.GetPermissionsToApp(app);
        if (!permission.CanView)
        {
            throw new AccessDeniedException("Access denied to this user");
        }
        var assignedRoles = await user.Modifier(modifier).AssignedRoles();
        return assignedRoles
            .Where(role => !role.IsDenyAccess())
            .Select(role => role.ToModel())
            .ToArray();
    }
}
