using Microsoft.AspNetCore.OData.Query;
using XTI_HubDB.Entities;
using XTI_ODataQuery.Api;

namespace XTI_HubWebAppApi.Logs;

internal sealed class LogEntryQueryAction : QueryAction<EmptyRequest, ExpandedLogEntry>
{
    private readonly CurrentUser currentUser;
    private readonly IHubDbContext db;

    public LogEntryQueryAction(CurrentUser currentUser, IHubDbContext db)
    {
        this.currentUser = currentUser;
        this.db = db;
    }

    public async Task<IQueryable<ExpandedLogEntry>> Execute(ODataQueryOptions<ExpandedLogEntry> options, EmptyRequest model)
    {
        var userGroupPermissions = await currentUser.GetUserGroupPermissions();
        var userGroupIDs = userGroupPermissions
            .Where(p => p.CanView)
            .Select(p => p.UserGroup.ToModel().ID)
            .ToArray();
        var appPermissions = await currentUser.GetAppPermissions();
        var appIDs = appPermissions
            .Where(p => p.CanView)
            .Select(p => p.App.ToModel().ID)
            .ToArray();
        return db.ExpandedLogEntries.Retrieve()
            .Where(r => userGroupIDs.Contains(r.UserGroupID) && appIDs.Contains(r.AppID));
    }
}
