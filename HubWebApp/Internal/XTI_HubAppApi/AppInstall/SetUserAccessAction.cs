using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppInstall;

public sealed class SetUserAccessRequest
{
    public int UserID { get; set; }
    public SetUserAccessRoleRequest[] RoleAssignments { get; set; } = new SetUserAccessRoleRequest[0];
}

public sealed class SetUserAccessRoleRequest
{
    public AppKey AppKey { get; set; } = AppKey.Unknown;
    public AppRoleName[] RoleNames { get; set; } = new AppRoleName[0];
}

internal sealed class SetUserAccessAction : AppAction<SetUserAccessRequest, EmptyActionResult>
{
    private readonly AppFactory appFactory;

    public SetUserAccessAction(AppFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<EmptyActionResult> Execute(SetUserAccessRequest model)
    {
        var user = await appFactory.Users.User(model.UserID);
        foreach(var assignment in model.RoleAssignments)
        {
            var app = await appFactory.Apps.App(assignment.AppKey);
            foreach(var roleName in assignment.RoleNames)
            {
                var role = await app.Role(roleName);
                await user.AssignRole(role);
            }
        }
        return new EmptyActionResult();
    }
}
