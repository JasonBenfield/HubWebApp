using Microsoft.AspNetCore.OData.Query;
using XTI_HubDB.Entities;
using XTI_ODataQuery.Api;

namespace XTI_HubWebAppApiActions.UserGroups;

public sealed class UserQueryAction : QueryAction<UserGroupKey, ExpandedUser>
{
    private readonly CurrentAppUser currentUser;
    private readonly EfHubDB db;

    public UserQueryAction(CurrentAppUser currentUser, EfHubDB db)
    {
        this.currentUser = currentUser;
        this.db = db;
    }

    public async Task<IQueryable<ExpandedUser>> Execute(ODataQueryOptions<ExpandedUser> options, UserGroupKey model, CancellationToken ct)
    {
        var user = await currentUser.Value(ct);
        var userPermissions = await user.GetUserGroupPermissions(ct);
        var userGroupModels = userPermissions
            .Where(p => p.CanView)
            .Select(p => p.EfUserGroup.ToModel());
        var userGroupIDs = userGroupModels
            .Where
            (
                ug =>
                    string.IsNullOrWhiteSpace(model.UserGroupName) ||
                    ug.GroupName.Equals(model.UserGroupName)
            )
            .Select(ug => ug.ID)
            .ToArray();
        return db.ExpandedUsers.Retrieve()
               .Where(u => userGroupIDs.Contains(u.UserGroupID));
    }
}
