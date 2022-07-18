// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserGroupsGroup : AppClientGroup
{
    public UserGroupsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "UserGroups")
    {
        Actions = new UserGroupsActions(clientUrl);
    }

    public UserGroupsActions Actions { get; }

    public Task<AppUserGroupModel> AddUserGroupIfNotExists(AddUserGroupIfNotExistsRequest model) => Post<AppUserGroupModel, AddUserGroupIfNotExistsRequest>("AddUserGroupIfNotExists", "", model);
    public Task<AppUserGroupModel[]> GetUserGroups() => Post<AppUserGroupModel[], EmptyRequest>("GetUserGroups", "", new EmptyRequest());
}