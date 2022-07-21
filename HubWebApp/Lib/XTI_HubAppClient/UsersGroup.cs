// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UsersGroup : AppClientGroup
{
    public UsersGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Users")
    {
        Actions = new UsersGroupActions(Index: CreateGetAction<GetUserRequest>("Index"), GetUsers: CreatePostAction<EmptyRequest, AppUserModel[]>("GetUsers"), AddOrUpdateUser: CreatePostAction<AddUserModel, int>("AddOrUpdateUser"));
    }

    public UsersGroupActions Actions { get; }

    public Task<AppUserModel[]> GetUsers(string modifier) => Actions.GetUsers.Post(modifier, new EmptyRequest());
    public Task<int> AddOrUpdateUser(string modifier, AddUserModel model) => Actions.AddOrUpdateUser.Post(modifier, model);
    public sealed record UsersGroupActions(AppClientGetAction<GetUserRequest> Index, AppClientPostAction<EmptyRequest, AppUserModel[]> GetUsers, AppClientPostAction<AddUserModel, int> AddOrUpdateUser);
}