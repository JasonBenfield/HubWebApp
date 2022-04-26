using Microsoft.EntityFrameworkCore.Migrations;

namespace XTI_HubDB.EF.SqlServer
{
    public partial class AppLogViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql
            (
                @"
create view ExpandedSessions
as
select 
	a.ID SessionID, SessionKey, UserID, RequesterKey, 
	TimeStarted, 
	TimeEnded, 
	cast(TimeStarted at time zone 'Eastern Standard Time' as datetime) TimeStartedLocal,
	cast(TimeEnded at time zone 'Eastern Standard Time' as datetime) TimeEndedLocal,
	case when datediff(month,timestarted,timeended) < 1 then (datediff(minute, TimeStarted, TimeEnded) / 60.0) else null end TimeElapsed,
	RemoteAddress, UserAgent,
	b.UserName, b.TimeAdded TimeUserAdded, b.Email, b.Name
from sessions a
inner join users b
on a.userid = b.id
"
            );
            migrationBuilder.Sql
            (
                @"
create view ExpandedRequests
as
select 
	a.id RequestID, RequestKey, Path, 
	a.TimeStarted RequestTimeStarted, 
	a.TimeEnded RequestTimeEnded,
	cast(a.TimeStarted at time zone 'Eastern Standard Time' as datetime) RequestTimeStartedLocal,
	cast(a.TimeEnded at time zone 'Eastern Standard Time' as datetime) RequestTimeEndedLocal,
	case when a.TimeEnded < dateadd(year, 1, getdate()) then datediff(millisecond, a.TimeStarted, a.TimeEnded) else null end RequestTimeElapsed,
	SessionID, b.SessionKey, b.RequesterKey, 
	b.TimeStarted SessionTimeStarted, 
	b.TimeEnded SessionTimeEnded, 
	cast(b.TimeStarted at time zone 'Eastern Standard Time' as datetime) SessionTimeStartedLocal,
	cast(b.TimeEnded at time zone 'Eastern Standard Time' as datetime) SessionTimeEndedLocal,
	case when datediff(month,b.timestarted,b.timeended) < 1 then (datediff(minute, b.TimeStarted, b.TimeEnded) / 60.0) else null end SessionTimeElapsed,
	b.RemoteAddress, b.UserAgent,
	b.UserID, c.UserName, c.Name UserPersonalName, c.Email, c.TimeAdded TimeUserAdded,
	ResourceID, d.Name ResourceName, 
	d.ResultType, 
	case when d.ResultType = 1 then 'View' when d.ResultType = 2 then 'PartialView'when d.ResultType = 3 then 'Redirect' when d.ResultType = 4 then 'Json' else 'Unknown' end ResultTypeText,
	d.IsAnonymousAllowed IsAnonymousAllowedToResource,
	d.GroupID, e.Name ResourceGroupName, e.ModCategoryID ResourceGroupModCategoryID, e.IsAnonymousAllowed IsAnonymousAllowedToResourceGroup, e.VersionID,
	ModifierID, f.ModKey, f.TargetKey, f.DisplayText,
	f.CategoryID ModCategoryID, g.Name ModCategoryName, 
	g.AppID, h.Name AppName, h.TimeAdded TimeAppAdded, h.Title AppTitle, 
	h.Type AppType,
	case when h.Type = 10 then 'Web App' when h.Type = 15 then 'Service' when h.Type = 20 then 'Package' else 'Unknown' end AppTypeText,
	i.Type VersionType, 
	case when i.Type = 1 then 'Major' when i.type = 2 then 'Minor' when i.Type = 3 then 'Patch' else 'Unknown' end VersionTypeText,
	i.Status VersionStatus, 
	case when i.Status = 1 then 'New' when i.Status = 2 then 'Publishing' when i.Status = 3 then 'Old' when i.Status = 4 then 'Current' else 'Unknown' end VersionStatusText,
	i.TimeAdded TimeVersionAdded, i.Major, i.Minor, i.Patch
from requests a
inner join sessions b
on a.SessionID = b.id
inner join users c
on b.userid = c.id
inner join Resources d
on a.ResourceID = d.ID
inner join ResourceGroups e
on d.GroupID = e.ID 
inner join Modifiers f
on a.ModifierID = f.ID
inner join ModifierCategories g
on f.CategoryID = g.ID
inner join Versions i
on e.VersionID = i.ID
inner join Apps h
on i.AppID = h.ID
"
            );
            migrationBuilder.Sql
            (
                @"
create view ExpandedEvents
as
select 
	j.ID EventID, j.Caption, j.Message, j.Detail, 
	j.TimeOccurred, 
	cast(j.TimeOccurred at time zone 'Eastern Standard Time' as datetime) TimeOccurredLocal,
	j.Severity,
	case when j.Severity = 100 then 'Critical Error' when j.Severity = 80 then 'Access Denied' when j.Severity = 70 then 'App Error' when j.Severity = 60 then 'Validation Failed' when j.Severity = 50 then 'Information' else 'Unknown' end SeverityText,
	j.RequestID, RequestKey, Path, 
	a.TimeStarted RequestTimeStarted, 
	a.TimeEnded RequestTimeEnded,
	cast(a.TimeStarted at time zone 'Eastern Standard Time' as datetime) RequestTimeStartedLocal,
	cast(a.TimeEnded at time zone 'Eastern Standard Time' as datetime) RequestTimeEndedLocal,
	case when a.TimeEnded < dateadd(year, 1, getdate()) then datediff(millisecond, a.TimeStarted, a.TimeEnded) else null end RequestTimeElapsed,
	SessionID, b.SessionKey, b.RequesterKey, 
	b.TimeStarted SessionTimeStarted, 
	b.TimeEnded SessionTimeEnded, 
	cast(b.TimeStarted at time zone 'Eastern Standard Time' as datetime) SessionTimeStartedLocal,
	cast(b.TimeEnded at time zone 'Eastern Standard Time' as datetime) SessionTimeEndedLocal,
	case when datediff(month,b.timestarted,b.timeended) < 1 then (datediff(minute, b.TimeStarted, b.TimeEnded) / 60.0) else null end SessionTimeElapsed,
	b.RemoteAddress, b.UserAgent,
	b.UserID, c.UserName, c.Name UserPersonalName, c.Email, c.TimeAdded TimeUserAdded,
	ResourceID, d.Name ResourceName, 
	d.ResultType, 
	case when d.ResultType = 1 then 'View' when d.ResultType = 2 then 'PartialView'when d.ResultType = 3 then 'Redirect' when d.ResultType = 4 then 'Json' else 'Unknown' end ResultTypeText,
	d.IsAnonymousAllowed IsAnonymousAllowedToResource,
	d.GroupID, e.Name ResourceGroupName, e.ModCategoryID ResourceGroupModCategoryID, e.IsAnonymousAllowed IsAnonymousAllowedToResourceGroup, e.VersionID,
	ModifierID, f.ModKey, f.TargetKey, f.DisplayText,
	f.CategoryID ModCategoryID, g.Name ModCategoryName, 
	g.AppID, h.Name AppName, h.TimeAdded TimeAppAdded, h.Title AppTitle, 
	h.Type AppType,
	case when h.Type = 10 then 'Web App' when h.Type = 15 then 'Service' when h.Type = 20 then 'Package' else 'Unknown' end AppTypeText,
	i.Type VersionType, 
	case when i.Type = 1 then 'Major' when i.type = 2 then 'Minor' when i.Type = 3 then 'Patch' else 'Unknown' end VersionTypeText,
	i.Status VersionStatus, 
	case when i.Status = 1 then 'New' when i.Status = 2 then 'Publishing' when i.Status = 3 then 'Old' when i.Status = 4 then 'Current' else 'Unknown' end VersionStatusText,
	i.TimeAdded TimeVersionAdded, i.Major, i.Minor, i.Patch
from events j
inner join requests a
on j.requestid = a.id
inner join sessions b
on a.SessionID = b.id
inner join users c
on b.userid = c.id
inner join Resources d
on a.ResourceID = d.ID
inner join ResourceGroups e
on d.GroupID = e.ID 
inner join Modifiers f
on a.ModifierID = f.ID
inner join ModifierCategories g
on f.CategoryID = g.ID
inner join Versions i
on e.VersionID = i.ID
inner join Apps h
on i.AppID = h.ID
"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
