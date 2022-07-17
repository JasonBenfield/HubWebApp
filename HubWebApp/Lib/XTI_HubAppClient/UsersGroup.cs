// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UsersGroup : AppClientGroup
{
    public UsersGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "Users")
    {
        Actions = new UsersActions(clientUrl);
    }

    public UsersActions Actions { get; }

    public Task<AppUserModel[]> GetUsers(string modifier) => Post<AppUserModel[], EmptyRequest>("GetUsers", modifier, new EmptyRequest());
    public Task<int> AddOrUpdateUser(string modifier, AddUserModel model) => Post<int, AddUserModel>("AddOrUpdateUser", modifier, model);
}