namespace XTI_HubWebAppApi.UserList;

public sealed class UserListGroup : AppApiGroupWrapper
{
    public UserListGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Index = source.AddAction(nameof(Index), () => sp.GetRequiredService<IndexAction>());
        GetUsers = source.AddAction(nameof(GetUsers), () => sp.GetRequiredService<GetUsersAction>());
        AddOrUpdateUser = source.AddAction
        (
            nameof(AddOrUpdateUser),
            () => sp.GetRequiredService<AddOrUpdateUserAction>(),
            () => sp.GetRequiredService<AddUserValidation>(),
            Access.WithAllowed(HubInfo.Roles.AddUser)
        );
    }
    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
    public AppApiAction<EmptyRequest, AppUserModel[]> GetUsers { get; }
    public AppApiAction<AddUserModel, int> AddOrUpdateUser { get; }
}