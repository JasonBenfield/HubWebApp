using XTI_HubDB.Entities;
using XTI_HubWebAppApi.Logs;
using XTI_HubWebAppApi.UserRoles;
using XTI_ODataQuery.Api;

namespace XTI_HubWebAppApi;

partial class HubAppApi
{
    private UserRolesGroup? _UserRoles;

    public UserRolesGroup UserRoles { get => _UserRoles ?? throw new ArgumentNullException(nameof(_UserRoles)); }

    private ODataGroup<UserRoleQueryRequest, ExpandedUserRole>? _UserRoleQuery;

    public ODataGroup<UserRoleQueryRequest, ExpandedUserRole> UserRoleQuery
    {
        get => _UserRoleQuery ?? throw new ArgumentNullException(nameof(_UserRoleQuery));
    }

    partial void createUserRolesGroup(IServiceProvider sp)
    {
        _UserRoles = new UserRolesGroup
        (
            source.AddGroup(nameof(UserRoles)),
            sp
        );
        _UserRoleQuery = new ODataGroup<UserRoleQueryRequest, ExpandedUserRole>
        (
            source.AddGroup(nameof(UserRoleQuery)),
            () => sp.GetRequiredService<UserRoleQueryAction>()
        );
    }
}