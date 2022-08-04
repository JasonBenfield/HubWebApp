// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UsersGroup : AppClientGroup
{
    public UsersGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Users")
    {
        Actions = new UsersGroupActions(Index: CreateGetAction<GetUserRequest>("Index"), GetUserGroup: CreatePostAction<EmptyRequest, AppUserGroupModel>("GetUserGroup"), GetUsers: CreatePostAction<EmptyRequest, AppUserModel[]>("GetUsers"), AddOrUpdateUser: CreatePostAction<AddOrUpdateUserModel, int>("AddOrUpdateUser"), AddUser: CreatePostAction<AddUserForm, AppUserModel>("AddUser"));
    }

    public UsersGroupActions Actions { get; }

    public Task<AppUserGroupModel> GetUserGroup(string modifier) => Actions.GetUserGroup.Post(modifier, new EmptyRequest());
    public Task<AppUserModel[]> GetUsers(string modifier) => Actions.GetUsers.Post(modifier, new EmptyRequest());
    public Task<int> AddOrUpdateUser(string modifier, AddOrUpdateUserModel model) => Actions.AddOrUpdateUser.Post(modifier, model);
    public Task<AppUserModel> AddUser(string modifier, AddUserForm model) => Actions.AddUser.Post(modifier, model);
    public sealed record UsersGroupActions(AppClientGetAction<GetUserRequest> Index, AppClientPostAction<EmptyRequest, AppUserGroupModel> GetUserGroup, AppClientPostAction<EmptyRequest, AppUserModel[]> GetUsers, AppClientPostAction<AddOrUpdateUserModel, int> AddOrUpdateUser, AppClientPostAction<AddUserForm, AppUserModel> AddUser);
}