using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_HubDB.Entities;

namespace XTI_HubWebAppApi.UserRoles;

internal sealed class GetUserRoleDetailAction : AppAction<int, UserRoleDetailModel>
{
    private readonly CurrentAppUser currentUser;
    private readonly HubFactory hubFactory;

    public GetUserRoleDetailAction(CurrentAppUser currentUser, HubFactory hubFactory)
    {
        this.currentUser = currentUser;
        this.hubFactory = hubFactory;
    }

    public async Task<UserRoleDetailModel> Execute(int userRoleID, CancellationToken stoppingToken)
    {
        var userRole = await hubFactory.UserRoles.UserRole(userRoleID);
        var user = await userRole.User();
        var userGroup = await user.UserGroup();
        var userGroupPermission = await currentUser.GetPermissionsToUserGroup(userGroup);
        if (!userGroupPermission.CanView)
        {
            throw new AccessDeniedException($"Access denied to user '{userGroup.ToModel().GroupName}'");
        }
        var role = await userRole.Role();
        var app = await role.App();
        var appPermission = await currentUser.GetPermissionsToApp(app);
        if (!appPermission.CanView)
        {
            throw new AccessDeniedException($"Access denied to app '{app.ToModel().AppKey.Format()}'");
        }
        var modifier = await userRole.Modifier();
        var modCategory = await modifier.Category();
        return new UserRoleDetailModel
        (
            userRole.ID, 
            user.ToModel(), 
            app.ToModel(), 
            role.ToModel(),
            modCategory.ToModel(),
            modifier.ToModel()
        );
    }
}
