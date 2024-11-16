using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace HubWebApp.Extensions;

public sealed class WebUserContext : ISourceUserContext
{
    private readonly EfUserContext userContext;

    public WebUserContext(HubFactory hubFactory, ICurrentUserName currentUserName)
    {
        userContext = new EfUserContext(hubFactory, currentUserName);
    }

    public Task<AppUserModel> User() => userContext.User();

    public Task<AppUserModel> User(AppUserName userName) => userContext.User(userName);

    public Task<AppUserModel> UserOrAnon(AppUserName userName) => userContext.UserOrAnon(userName);

    public Task<AppRoleModel[]> UserRoles(AppUserModel user, ModifierModel modifier) =>
        userContext.UserRoles(user, modifier);
}