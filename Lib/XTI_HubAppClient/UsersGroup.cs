// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UsersGroup : AppClientGroup
{
    public UsersGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, AppClientUrl clientUrl) : base(httpClientFactory, xtiToken, clientUrl, "Users")
    {
    }

    public Task<AppUserModel[]> GetUsers() => Post<AppUserModel[], EmptyRequest>("GetUsers", "", new EmptyRequest());
    public Task<AppUserModel[]> GetSystemUsers(AppKey model) => Post<AppUserModel[], AppKey>("GetSystemUsers", "", model);
    public Task<int> AddOrUpdateUser(AddUserModel model) => Post<int, AddUserModel>("AddOrUpdateUser", "", model);
}