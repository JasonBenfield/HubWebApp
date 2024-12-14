// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserGroupsGroup : AppClientGroup
{
    public UserGroupsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "UserGroups")
    {
        Actions = new UserGroupsGroupActions(AddUserGroupIfNotExists: CreatePostAction<AddUserGroupIfNotExistsRequest, AppUserGroupModel>("AddUserGroupIfNotExists"), GetUserDetailOrAnon: CreatePostAction<AppUserNameRequest, AppUserDetailModel>("GetUserDetailOrAnon"), GetUserGroupForUser: CreatePostAction<AppUserIDRequest, AppUserGroupModel>("GetUserGroupForUser"), GetUserGroups: CreatePostAction<EmptyRequest, AppUserGroupModel[]>("GetUserGroups"), Index: CreateGetAction<EmptyRequest>("Index"), UserQuery: CreateGetAction<UserGroupKey>("UserQuery"));
    }

    public UserGroupsGroupActions Actions { get; }

    public Task<AppUserGroupModel> AddUserGroupIfNotExists(AddUserGroupIfNotExistsRequest model, CancellationToken ct = default) => Actions.AddUserGroupIfNotExists.Post("", model, ct);
    public Task<AppUserDetailModel> GetUserDetailOrAnon(AppUserNameRequest model, CancellationToken ct = default) => Actions.GetUserDetailOrAnon.Post("", model, ct);
    public Task<AppUserGroupModel> GetUserGroupForUser(AppUserIDRequest model, CancellationToken ct = default) => Actions.GetUserGroupForUser.Post("", model, ct);
    public Task<AppUserGroupModel[]> GetUserGroups(CancellationToken ct = default) => Actions.GetUserGroups.Post("", new EmptyRequest(), ct);
    public sealed record UserGroupsGroupActions(AppClientPostAction<AddUserGroupIfNotExistsRequest, AppUserGroupModel> AddUserGroupIfNotExists, AppClientPostAction<AppUserNameRequest, AppUserDetailModel> GetUserDetailOrAnon, AppClientPostAction<AppUserIDRequest, AppUserGroupModel> GetUserGroupForUser, AppClientPostAction<EmptyRequest, AppUserGroupModel[]> GetUserGroups, AppClientGetAction<EmptyRequest> Index, AppClientGetAction<UserGroupKey> UserQuery);
}