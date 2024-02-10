// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserGroupsGroup : AppClientGroup
{
    public UserGroupsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "UserGroups")
    {
        Actions = new UserGroupsGroupActions(Index: CreateGetAction<EmptyRequest>("Index"), UserQuery: CreateGetAction<UserGroupKey>("UserQuery"), AddUserGroupIfNotExists: CreatePostAction<AddUserGroupIfNotExistsRequest, AppUserGroupModel>("AddUserGroupIfNotExists"), GetUserGroups: CreatePostAction<EmptyRequest, AppUserGroupModel[]>("GetUserGroups"), GetUserDetailOrAnon: CreatePostAction<AppUserNameRequest, AppUserDetailModel>("GetUserDetailOrAnon"));
    }

    public UserGroupsGroupActions Actions { get; }

    public Task<AppUserGroupModel> AddUserGroupIfNotExists(AddUserGroupIfNotExistsRequest model, CancellationToken ct = default) => Actions.AddUserGroupIfNotExists.Post("", model, ct);
    public Task<AppUserGroupModel[]> GetUserGroups(CancellationToken ct = default) => Actions.GetUserGroups.Post("", new EmptyRequest(), ct);
    public Task<AppUserDetailModel> GetUserDetailOrAnon(AppUserNameRequest model, CancellationToken ct = default) => Actions.GetUserDetailOrAnon.Post("", model, ct);
    public sealed record UserGroupsGroupActions(AppClientGetAction<EmptyRequest> Index, AppClientGetAction<UserGroupKey> UserQuery, AppClientPostAction<AddUserGroupIfNotExistsRequest, AppUserGroupModel> AddUserGroupIfNotExists, AppClientPostAction<EmptyRequest, AppUserGroupModel[]> GetUserGroups, AppClientPostAction<AppUserNameRequest, AppUserDetailModel> GetUserDetailOrAnon);
}