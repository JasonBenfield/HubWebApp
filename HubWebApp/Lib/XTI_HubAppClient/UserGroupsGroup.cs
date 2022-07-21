// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserGroupsGroup : AppClientGroup
{
    public UserGroupsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "UserGroups")
    {
        Actions = new UserGroupsGroupActions(Index: CreateGetAction<EmptyRequest>("Index"), UserQuery: CreateGetAction<UserGroupKey>("UserQuery"), AddUserGroupIfNotExists: CreatePostAction<AddUserGroupIfNotExistsRequest, AppUserGroupModel>("AddUserGroupIfNotExists"), GetUserGroups: CreatePostAction<EmptyRequest, AppUserGroupModel[]>("GetUserGroups"));
    }

    public UserGroupsGroupActions Actions { get; }

    public Task<AppUserGroupModel> AddUserGroupIfNotExists(AddUserGroupIfNotExistsRequest model) => Actions.AddUserGroupIfNotExists.Post("", model);
    public Task<AppUserGroupModel[]> GetUserGroups() => Actions.GetUserGroups.Post("", new EmptyRequest());
    public sealed record UserGroupsGroupActions(AppClientGetAction<EmptyRequest> Index, AppClientGetAction<UserGroupKey> UserQuery, AppClientPostAction<AddUserGroupIfNotExistsRequest, AppUserGroupModel> AddUserGroupIfNotExists, AppClientPostAction<EmptyRequest, AppUserGroupModel[]> GetUserGroups);
}