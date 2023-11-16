namespace XTI_HubDB.EF.SqlServer.V1423;

internal static class ExpandedSessionsView
{
    public static readonly string Sql = """
create or alter view ExpandedSessions as
with
RequestCounts as
(
	select SessionID, count(sessionID) RequestCount
	from Requests
	group by sessionid
),
MostRecentRequests as
(
	select SessionID, max(TimeStarted) LastRequestTime
	from Requests
	group by sessionid
)
select 
	sessions.ID SessionID, SessionKey, UserID, RequesterKey, 
	dbo.GetLocalDateTime(TimeStarted) TimeSessionStarted,
	dbo.GetLocalDateTime(TimeEnded) TimeSessionEnded,
	dbo.TimeElapsedDisplayText(TimeStarted, TimeEnded) TimeElapsed,
	RemoteAddress, UserAgent,
	users.GroupID UserGroupID, userGroups.GroupName UserGroupName, 
	userGroups.DisplayText UserGroupDisplayText,
	users.UserName, dbo.GetLocalDateTime(users.TimeAdded) TimeUserAdded, 
	dbo.GetLocalDateTime(users.TimeDeactivated) TimeUserDeactivated, 
	users.Email, users.Name, 
	isnull(RequestCounts.RequestCount, 0) requestCount,
	MostRecentRequests.LastRequestTime
from sessions
inner join users users
on sessions.userid = users.id
inner join userGroups
on users.groupid = userGroups.id
left outer join RequestCounts
on sessions.id = RequestCounts.sessionid
left outer join MostRecentRequests
on sessions.id = MostRecentRequests.sessionid
""";
}
