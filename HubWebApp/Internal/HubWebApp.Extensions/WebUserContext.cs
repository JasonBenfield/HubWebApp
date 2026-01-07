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

    public Task<AppUserModel> User(CancellationToken ct) => userContext.User(ct);

    public Task<AppUserModel> User(AppUserName userName, CancellationToken ct) => userContext.User(userName, ct);

    public Task<AppUserModel> UserOrAnon(AppUserName userName, CancellationToken ct) => userContext.UserOrAnon(userName, ct);

    public Task<AppRoleModel[]> UserRoles(AppUserModel user, ModifierModel modifier, CancellationToken ct) =>
        userContext.UserRoles(user, modifier, ct);
}