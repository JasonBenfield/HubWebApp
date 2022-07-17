using XTI_HubWebAppApi.UserGroups;

namespace XTI_HubWebAppApi;

partial class HubAppApi
{
    private UserGroupsGroup? _UserGroups;

    public UserGroupsGroup UserGroups { get => _UserGroups ?? throw new ArgumentNullException(nameof(_UserGroups)); }

    partial void createUserGroupsGroup(IServiceProvider sp)
    {
        _UserGroups = new UserGroupsGroup
        (
            source.AddGroup(nameof(UserGroups)),
            sp
        );
    }
}