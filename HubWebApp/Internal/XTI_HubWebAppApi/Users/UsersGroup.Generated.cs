using XTI_HubWebAppApiActions.UserList;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Users;
public sealed partial class UsersGroup : AppApiGroupWrapper
{
    internal UsersGroup(AppApiGroup source, UsersGroupBuilder builder) : base(source)
    {
        AddOrUpdateUser = builder.AddOrUpdateUser.Build();
        AddUser = builder.AddUser.Build();
        GetUserGroup = builder.GetUserGroup.Build();
        GetUsers = builder.GetUsers.Build();
        Index = builder.Index.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<AddOrUpdateUserRequest, AppUserModel> AddOrUpdateUser { get; }
    public AppApiAction<AddUserForm, AppUserModel> AddUser { get; }
    public AppApiAction<EmptyRequest, AppUserGroupModel> GetUserGroup { get; }
    public AppApiAction<EmptyRequest, AppUserModel[]> GetUsers { get; }
    public AppApiAction<UsersIndexRequest, WebViewResult> Index { get; }
}