using Microsoft.AspNetCore.OData.Query;
using XTI_HubDB.Entities;
using XTI_ODataQuery.Api;

namespace XTI_HubWebAppApi.Logs;

public sealed class ExpandedSession
{
    public int SessionID { get; set; }
    public string UserName { get; set; } = "";
    public string UserGroupName { get; set; } = "";
    public string RemoteAddress { get; set; } = "";
    public string UserAgent { get; set; } = "";
    public DateTimeOffset TimeStarted { get; set; }
    public DateTimeOffset TimeEnded { get; set; }
    public DateTimeOffset? LastRequestTime { get; set; }
    public int RequestCount { get; set; }
}

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
        return from session in db.Sessions.Retrieve()
               join user in db.Users.Retrieve() on session.UserID equals user.ID
               join userGroup in db.UserGroups.Retrieve() on user.GroupID equals userGroup.ID
               where userGroupIDs.Contains(userGroup.ID)
               let requests = (from r in db.Requests.Retrieve() where r.SessionID == session.ID select r).ToArray()
               let requestCount = requests.Count()
               let lastRequest = requests.Max(r => (DateTimeOffset?)r.TimeStarted)
               select new ExpandedSession
               {
                   SessionID = session.ID,
                   UserName = user.UserName,
                   UserGroupName = userGroup.DisplayText,
                   RemoteAddress = session.RemoteAddress,
                   UserAgent = session.UserAgent,
                   TimeStarted = session.TimeStarted,
                   TimeEnded = session.TimeEnded,
                   RequestCount = requestCount,
                   LastRequestTime = lastRequest
               };
    }
}
