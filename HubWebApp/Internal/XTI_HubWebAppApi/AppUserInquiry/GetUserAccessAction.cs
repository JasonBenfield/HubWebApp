namespace XTI_HubWebAppApi.AppUserInquiry;

public sealed class GetUserAccessAction : AppAction<UserModifierKey, UserAccessModel>
{
    private readonly AppFromPath appFromPath;
    private readonly HubFactory factory;

    public GetUserAccessAction(AppFromPath appFromPath, HubFactory factory)
    {
        this.appFromPath = appFromPath;
        this.factory = factory;
    }

    public async Task<UserAccessModel> Execute(UserModifierKey model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var user = await factory.Users.User(model.UserID);
        var modifier = await app.Modifier(model.ModifierID);
        var roles = await user.Modifier(modifier).ExplicitlyAssignedRoles();
        var roleModels = roles
            .Where(r => !r.Name().Equals(AppRoleName.DenyAccess))
            .Select(r => r.ToModel())
            .ToArray();
        var hasAccess = !roles.Any(r => r.Name().Equals(AppRoleName.DenyAccess));
        return new UserAccessModel(hasAccess, roleModels);
    }
}