namespace XTI_HubWebAppApi.UserList;

public sealed class UserListGroup : AppApiGroupWrapper
{
    public UserListGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Index = source.AddAction
        (
            nameof(Index),
            () => sp.GetRequiredService<IndexAction>(),
            access: Access.WithAllowed(HubInfo.Roles.ViewUser)
        );
        GetUserGroup = source.AddAction
        (
            nameof(GetUserGroup),
            () => sp.GetRequiredService<GetUserGroupAction>(),
            access: Access.WithAllowed(HubInfo.Roles.ViewUser)
        );
        GetUsers = source.AddAction
        (
            nameof(GetUsers),
            () => sp.GetRequiredService<GetUsersAction>(),
            access: Access.WithAllowed(HubInfo.Roles.ViewUser)
        );
        AddOrUpdateUser = source.AddAction
        (
            nameof(AddOrUpdateUser),
            () => sp.GetRequiredService<AddOrUpdateUserAction>(),
            () => sp.GetRequiredService<AddOrUpdateUserValidation>(),
            Access.WithAllowed(HubInfo.Roles.AddUser)
        );
        AddUser = source.AddAction
        (
            nameof(AddUser),
            () => sp.GetRequiredService<AddUserAction>(),
            access: Access.WithAllowed(HubInfo.Roles.AddUser)
        );
    }
    public AppApiAction<GetUserRequest, WebViewResult> Index { get; }
    public AppApiAction<EmptyRequest, AppUserGroupModel> GetUserGroup { get; }
    public AppApiAction<EmptyRequest, AppUserModel[]> GetUsers { get; }
    public AppApiAction<AddOrUpdateUserModel, int> AddOrUpdateUser { get; }
    public AppApiAction<AddUserForm, AppUserModel> AddUser { get; }
}