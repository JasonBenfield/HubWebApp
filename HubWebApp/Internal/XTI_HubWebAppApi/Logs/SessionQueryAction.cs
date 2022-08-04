using Microsoft.AspNetCore.OData.Query;
using XTI_HubDB.Entities;
using XTI_ODataQuery.Api;

namespace XTI_HubWebAppApi.Logs;

internal sealed class SessionQueryAction : QueryAction<EmptyRequest, ExpandedSession>
{
    private readonly CurrentUser currentUser;
    private readonly IHubDbContext db;

    public SessionQueryAction(CurrentUser currentUser, IHubDbContext db)
    {
        this.currentUser = currentUser;
        this.db = db;
    }

    public async Task<IQueryable<ExpandedSession>> Execute(ODataQueryOptions<ExpandedSession> options, EmptyRequest model)
    {
        var userGroupPermissions = await currentUser.GetUserGroupPermissions();
        var userGroupIDs = userGroupPermissions
            .Where(p => p.CanView)
            .Select(p => p.UserGroup.ToModel().ID)
            .ToArray();
        return db.ExpandedSessions.Retrieve().Where(s => userGroupIDs.Contains(s.UserGroupID));
    }
}
