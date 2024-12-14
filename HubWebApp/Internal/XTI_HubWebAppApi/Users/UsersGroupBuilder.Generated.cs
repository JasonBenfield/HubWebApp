using XTI_HubWebAppApiActions.UserList;

// Generated Code
namespace XTI_HubWebAppApi.Users;
public sealed partial class UsersGroupBuilder
{
    private readonly AppApiGroup source;
    internal UsersGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        AddOrUpdateUser = source.AddAction<AddOrUpdateUserRequest, AppUserModel>("AddOrUpdateUser").WithExecution<AddOrUpdateUserAction>().WithValidation<AddOrUpdateUserValidation>();
        AddUser = source.AddAction<AddUserForm, AppUserModel>("AddUser").WithExecution<AddUserAction>();
        GetUserGroup = source.AddAction<EmptyRequest, AppUserGroupModel>("GetUserGroup").WithExecution<GetUserGroupAction>();
        GetUsers = source.AddAction<EmptyRequest, AppUserModel[]>("GetUsers").WithExecution<GetUsersAction>();
        Index = source.AddAction<UsersIndexRequest, WebViewResult>("Index").WithExecution<IndexAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<AddOrUpdateUserRequest, AppUserModel> AddOrUpdateUser { get; }
    public AppApiActionBuilder<AddUserForm, AppUserModel> AddUser { get; }
    public AppApiActionBuilder<EmptyRequest, AppUserGroupModel> GetUserGroup { get; }
    public AppApiActionBuilder<EmptyRequest, AppUserModel[]> GetUsers { get; }
    public AppApiActionBuilder<UsersIndexRequest, WebViewResult> Index { get; }

    public UsersGroup Build() => new UsersGroup(source, this);
}