using Microsoft.AspNetCore.OData.Query;
using XTI_HubDB.Entities;
using XTI_ODataQuery.Api;

namespace XTI_HubWebAppApiActions.UserRoles;

public sealed class UserRoleQueryAction : QueryAction<UserRoleQueryRequest, ExpandedUserRole>
{
    private readonly CurrentAppUser currentUser;
    private readonly EfHubDB db;

    public UserRoleQueryAction(CurrentAppUser currentUser, EfHubDB db)
    {
        this.currentUser = currentUser;
        this.db = db;
    }

    public async Task<IQueryable<ExpandedUserRole>> Execute(ODataQueryOptions<ExpandedUserRole> options, UserRoleQueryRequest queryRequest, CancellationToken ct)
    {
        var userGroupPermissions = await currentUser.GetUserGroupPermissions(ct);
        var userGroupIDs = userGroupPermissions
            .Where(p => p.CanView)
            .Select(p => p.UserGroup.ToModel().ID)
            .ToArray();
        var appPermissions = await currentUser.GetAppPermissions(ct);
        var appIDs = appPermissions
            .Where(p => p.CanView)
            .Select(p => p.App.ToModel().ID)
            .ToArray();
        var query = db.ExpandedUserRoles.Retrieve()
            .Where(e => userGroupIDs.Contains(e.UserGroupID) && appIDs.Contains(e.AppID));
        if (queryRequest.AppID.HasValue)
        {
            query = query.Where(e => e.AppID == queryRequest.AppID.Value);
        }
        return query;
    }
}
