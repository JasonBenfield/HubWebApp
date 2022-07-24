namespace XTI_HubWebAppApi.UserGroups;

public sealed class UserGroupsGroup : AppApiGroupWrapper
{
    public UserGroupsGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Index = source.AddAction
        (
            nameof(Index),
            () => sp.GetRequiredService<IndexView>(),
            access: ResourceAccess.AllowAuthenticated()
        );
        UserQuery = source.AddAction
        (
            nameof(UserQuery),
            () => sp.GetRequiredService<UserQueryView>(),
            access: ResourceAccess.AllowAuthenticated()
        );
        AddUserGroupIfNotExists = source.AddAction
        (
            nameof(AddUserGroupIfNotExists),
            () => sp.GetRequiredService<AddUserGroupIfNotExistsAction>(),
            () => sp.GetRequiredService<AddUserGroupIfNotExistsValidation>(),
            access: Access.WithAllowed(HubInfo.Roles.AddUserGroup)
        );
        GetUserGroups = source.AddAction
        (
            nameof(GetUserGroups),
            () => sp.GetRequiredService<GetUserGroupsAction>(),
            access: ResourceAccess.AllowAuthenticated()
        );
    }

    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
    public AppApiAction<UserGroupKey, WebViewResult> UserQuery { get; }
    public AppApiAction<AddUserGroupIfNotExistsRequest, AppUserGroupModel> AddUserGroupIfNotExists { get; }
    public AppApiAction<EmptyRequest, AppUserGroupModel[]> GetUserGroups { get; }
}