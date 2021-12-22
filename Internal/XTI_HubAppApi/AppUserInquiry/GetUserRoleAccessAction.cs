using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppUserInquiry;

public sealed class GetUserRoleAccessAction : AppAction<GetUserRoleAccessRequest, UserRoleAccessModel>
{
    private readonly AppFromPath appFromPath;
    private readonly AppFactory factory;

    public GetUserRoleAccessAction(AppFromPath appFromPath, AppFactory factory)
    {
        this.appFromPath = appFromPath;
        this.factory = factory;
    }

    public async Task<UserRoleAccessModel> Execute(GetUserRoleAccessRequest model)
    {
        var app = await appFromPath.Value();
        var user = await factory.Users.User(model.UserID);
        var modifier = await app.Modifier(model.ModifierID);
        var unassignedRoles = await user.ExplicitlyUnassignedRoles(modifier);
        var assignedRoles = await user.ExplicitlyAssignedRoles(modifier);
        return new UserRoleAccessModel
        (
            unassignedRoles.Select(role => role.ToModel()).ToArray(),
            assignedRoles.Select(role => role.ToModel()).ToArray()
        );
    }
}