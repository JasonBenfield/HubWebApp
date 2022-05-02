using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub;

public sealed class DefaultUserContext : IUserContext
{
    private readonly HubFactory appFactory;
    private readonly Func<string> getUserName;

    public DefaultUserContext(HubFactory appFactory, string userName)
        : this(appFactory, () => userName)
    {
    }

    public DefaultUserContext(HubFactory appFactory, Func<string> getUserName)
    {
        this.appFactory = appFactory;
        this.getUserName = getUserName;
    }

    public Task<IAppUser> User()
    {
        var userNameValue = getUserName();
        var userName = string.IsNullOrWhiteSpace(userNameValue)
            ? AppUserName.Anon
            : new AppUserName(userNameValue);
        return User(userName);
    }

    public async Task<IAppUser> User(AppUserName userName) => 
        await appFactory.Users.UserByUserName(userName);
}