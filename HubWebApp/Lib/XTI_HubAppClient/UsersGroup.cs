// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UsersGroup : AppClientGroup
{
    public UsersGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Users")
    {
        Actions = new UsersGroupActions(AddOrUpdateUser: CreatePostAction<AddOrUpdateUserRequest, AppUserModel>("AddOrUpdateUser"), AddUser: CreatePostAction<AddUserForm, AppUserModel>("AddUser"), GetUserGroup: CreatePostAction<EmptyRequest, AppUserGroupModel>("GetUserGroup"), GetUsers: CreatePostAction<EmptyRequest, AppUserModel[]>("GetUsers"), Index: CreateGetAction<UsersIndexRequest>("Index"));
    }

    public UsersGroupActions Actions { get; }

    public Task<AppUserModel> AddOrUpdateUser(string modifier, AddOrUpdateUserRequest requestData, CancellationToken ct = default) => Actions.AddOrUpdateUser.Post(modifier, requestData, ct);
    public Task<AppUserModel> AddUser(string modifier, AddUserForm requestData, CancellationToken ct = default) => Actions.AddUser.Post(modifier, requestData, ct);
    public Task<AppUserGroupModel> GetUserGroup(string modifier, CancellationToken ct = default) => Actions.GetUserGroup.Post(modifier, new EmptyRequest(), ct);
    public Task<AppUserModel[]> GetUsers(string modifier, CancellationToken ct = default) => Actions.GetUsers.Post(modifier, new EmptyRequest(), ct);
    public sealed record UsersGroupActions(AppClientPostAction<AddOrUpdateUserRequest, AppUserModel> AddOrUpdateUser, AppClientPostAction<AddUserForm, AppUserModel> AddUser, AppClientPostAction<EmptyRequest, AppUserGroupModel> GetUserGroup, AppClientPostAction<EmptyRequest, AppUserModel[]> GetUsers, AppClientGetAction<UsersIndexRequest> Index);
}