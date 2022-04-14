// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UsersGroup : AppClientGroup
{
    public UsersGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "Users")
    {
    }

    public Task<AppUserModel[]> GetUsers() => Post<AppUserModel[], EmptyRequest>("GetUsers", "", new EmptyRequest());
    public Task<AppUserModel[]> GetSystemUsers(AppKey model) => Post<AppUserModel[], AppKey>("GetSystemUsers", "", model);
    public Task<int> AddOrUpdateUser(AddUserModel model) => Post<int, AddUserModel>("AddOrUpdateUser", "", model);
}