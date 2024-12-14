using Microsoft.AspNetCore.OData.Query;
using XTI_HubDB.Entities;
using XTI_ODataQuery.Api;

namespace XTI_HubWebAppApiActions.Logs;

public sealed class LogEntryQueryAction : QueryAction<LogEntryQueryRequest, ExpandedLogEntry>
{
    private readonly CurrentAppUser currentUser;
    private readonly IHubDbContext db;

    public LogEntryQueryAction(CurrentAppUser currentUser, IHubDbContext db)
    {
        this.currentUser = currentUser;
        this.db = db;
    }

    public async Task<IQueryable<ExpandedLogEntry>> Execute(ODataQueryOptions<ExpandedLogEntry> options, LogEntryQueryRequest model)
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
        var query = db.ExpandedLogEntries.Retrieve()
            .Where(e => userGroupIDs.Contains(e.UserGroupID) && appIDs.Contains(e.AppID));
        if (model.RequestID.HasValue)
        {
            query = query.Where(e => e.RequestID == model.RequestID.Value);
        }
        if (model.InstallationID.HasValue)
        {
            query = query.Where(e => e.InstallationID == model.InstallationID.Value);
        }
        return query;
    }
}
