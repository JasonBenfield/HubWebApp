using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub;

public sealed class EfUserContext : IUserContext
{
    private readonly HubFactory hubFactory;
    private readonly AppKey appKey;
    private readonly ICurrentUserName currentUserName;

    public EfUserContext(HubFactory hubFactory, AppKey appKey, ICurrentUserName currentUserName)
    {
        this.hubFactory = hubFactory;
        this.appKey = appKey;
        this.currentUserName = currentUserName;
    }

    public async Task<UserContextModel> User()
    {
        var userName = await currentUserName.Value();
        var user = await User(userName);
        return user;
    }

    public async Task<UserContextModel> User(AppUserName userName)
    {
        var user = await hubFactory.Users.UserByUserName(userName);
        var app = await hubFactory.Apps.App(appKey);
        var userModifiers = await user.Modifiers(app);
        var roleModels = new List<UserContextRoleModel>();
        foreach (var userModifier in userModifiers)
        {
            var roles = await userModifier.AssignedRoles();
            roleModels.Add
            (
                new UserContextRoleModel
                (
                    userModifier.ToModifierModel().ModKey,
                    roles.Select(r => r.ToModel()).ToArray()
                )
            );
        }
        var userContextModel = new UserContextModel
        (
            user.ToModel(),
            roleModels.ToArray()
        );
        return userContextModel;
    }
}