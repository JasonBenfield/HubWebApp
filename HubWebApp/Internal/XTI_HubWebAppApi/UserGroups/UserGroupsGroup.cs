namespace XTI_HubWebAppApi.UserGroups;

public sealed class UserGroupsGroup : AppApiGroupWrapper
{
    public UserGroupsGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        AddUserGroupIfNotExists = source.AddAction
        (
            nameof(AddUserGroupIfNotExists),
            () => sp.GetRequiredService<AddUserGroupIfNotExistsAction>(),
            access: Access.WithAllowed(HubInfo.Roles.AddUserGroup)
        );
        GetUserGroups = source.AddAction
        (
            nameof(GetUserGroups),
            () => sp.GetRequiredService<GetUserGroupsAction>(),
            access: ResourceAccess.AllowAuthenticated()
        );
    }

    public AppApiAction<AddUserGroupIfNotExistsRequest, AppUserGroupModel> AddUserGroupIfNotExists { get; }
    public AppApiAction<EmptyRequest, AppUserGroupModel[]> GetUserGroups { get; }
}