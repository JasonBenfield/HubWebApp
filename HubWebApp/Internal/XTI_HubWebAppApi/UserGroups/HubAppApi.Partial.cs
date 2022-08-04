using XTI_HubWebAppApi.UserGroups;
using XTI_ODataQuery.Api;

namespace XTI_HubWebAppApi;

partial class HubAppApi
{
    private UserGroupsGroup? _UserGroups;

    public UserGroupsGroup UserGroups
    {
        get => _UserGroups ?? throw new ArgumentNullException(nameof(_UserGroups));
    }

    private ODataGroup<UserGroupKey, ExpandedUser>? _UserQuery;

    public ODataGroup<UserGroupKey, ExpandedUser> UserQuery
    {
        get => _UserQuery ?? throw new ArgumentNullException(nameof(_UserQuery));
    }

    partial void createUserGroupsGroup(IServiceProvider sp)
    {
        _UserGroups = new UserGroupsGroup
        (
            source.AddGroup(nameof(UserGroups)),
            sp
        );
        _UserQuery = new ODataGroup<UserGroupKey, ExpandedUser>
        (
            source.AddGroup(nameof(UserQuery)),
            () => sp.GetRequiredService<UserQueryAction>()
        );
    }
}