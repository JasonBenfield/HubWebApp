using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace HubWebApp.Extensions;

public sealed class WebUserContext : ISourceUserContext
{
    private readonly EfUserContext userContext;

    public WebUserContext(HubFactory hubFactory, AppKey appKey, ICurrentUserName currentUserName)
    {
        userContext = new EfUserContext(hubFactory, appKey, currentUserName);
    }

    public Task<UserContextModel> User() => userContext.User();

    public Task<UserContextModel> User(AppUserName userName) => userContext.User(userName);
}