// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UsersGroup : AppClientGroup
{
    public UsersGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Users")
    {
        Actions = new UsersGroupActions(Index: CreateGetAction<UsersIndexRequest>("Index"), GetUserGroup: CreatePostAction<EmptyRequest, AppUserGroupModel>("GetUserGroup"), GetUsers: CreatePostAction<EmptyRequest, AppUserModel[]>("GetUsers"), AddOrUpdateUser: CreatePostAction<AddOrUpdateUserRequest, AppUserModel>("AddOrUpdateUser"), AddUser: CreatePostAction<AddUserForm, AppUserModel>("AddUser"));
    }

    public UsersGroupActions Actions { get; }

    public Task<AppUserGroupModel> GetUserGroup(string modifier, CancellationToken ct = default) => Actions.GetUserGroup.Post(modifier, new EmptyRequest(), ct);
    public Task<AppUserModel[]> GetUsers(string modifier, CancellationToken ct = default) => Actions.GetUsers.Post(modifier, new EmptyRequest(), ct);
    public Task<AppUserModel> AddOrUpdateUser(string modifier, AddOrUpdateUserRequest model, CancellationToken ct = default) => Actions.AddOrUpdateUser.Post(modifier, model, ct);
    public Task<AppUserModel> AddUser(string modifier, AddUserForm model, CancellationToken ct = default) => Actions.AddUser.Post(modifier, model, ct);
    public sealed record UsersGroupActions(AppClientGetAction<UsersIndexRequest> Index, AppClientPostAction<EmptyRequest, AppUserGroupModel> GetUserGroup, AppClientPostAction<EmptyRequest, AppUserModel[]> GetUsers, AppClientPostAction<AddOrUpdateUserRequest, AppUserModel> AddOrUpdateUser, AppClientPostAction<AddUserForm, AppUserModel> AddUser);
}