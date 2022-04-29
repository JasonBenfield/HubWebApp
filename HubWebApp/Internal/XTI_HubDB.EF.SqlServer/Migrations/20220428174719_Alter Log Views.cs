using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTI_HubDB.EF.SqlServer
{
    public partial class AlterLogViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql
			(
@"
ALTER view [dbo].[ExpandedEvents]
as
select 
	j.ID EventID, j.caption, j.message, j.detail, 
	j.TimeOccurred, 
	dbo.ToEst(j.TimeOccurred) TimeOccurredLocal,
	j.Severity,
	dbo.EventSeverityDisplayText(j.Severity) SeverityText,
	j.ActualCount,
	j.RequestID, RequestKey, Path, 
	a.TimeStarted RequestTimeStarted, 
	a.TimeEnded RequestTimeEnded,
	dbo.ToEst(a.TimeStarted) RequestTimeStartedLocal,
	dbo.ToEst(a.TimeEnded) RequestTimeEndedLocal,
	dbo.TimeElapsedDisplayText(a.TimeStarted, a.TimeEnded) RequestTimeElapsed,
	SessionID, b.SessionKey, b.RequesterKey, 
	b.TimeStarted SessionTimeStarted, 
	b.TimeEnded SessionTimeEnded, 
	dbo.ToEst(b.TimeStarted) SessionTimeStartedLocal,
	dbo.ToEst(b.TimeEnded) SessionTimeEndedLocal,
	dbo.TimeElapsedDisplayText(b.timestarted,b.timeended) SessionTimeElapsed,
	b.RemoteAddress, b.UserAgent,
	b.UserID, c.UserName, c.Name UserPersonalName, c.Email, c.TimeAdded TimeUserAdded,
	ResourceID, d.Name ResourceName, 
	d.ResultType, 
	dbo.ResourceResultTypeDisplayText(d.ResultType) ResultTypeText,
	d.IsAnonymousAllowed IsAnonymousAllowedToResource,
	d.GroupID, e.Name ResourceGroupName, e.ModCategoryID ResourceGroupModCategoryID, e.IsAnonymousAllowed IsAnonymousAllowedToResourceGroup,
	ModifierID, f.ModKey, f.TargetKey, f.DisplayText,
	f.CategoryID ModCategoryID, g.Name ModCategoryName, 
	g.AppID, h.Name AppName, h.TimeAdded TimeAppAdded, h.Title AppTitle, 
	h.Type AppType,
	dbo.AppTypeDisplayText(h.Type) AppTypeText,
	i.ID VersionID,
	i.Type VersionType, 
	dbo.VersionTypeDisplayText(i.Type) VersionTypeText,
	i.Status VersionStatus, 
	dbo.VersionStatusDisplayText(i.Status) VersionStatusText,
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
inner join AppXtiVersions k
on e.AppVersionID = k.ID
inner join XtiVersions i
on i.ID = k.VersionID
inner join Apps h
on h.ID = k.AppID
"
			);
			migrationBuilder.Sql
			(
@"
ALTER view [dbo].[ExpandedRequests]
as
select 
	a.id RequestID, RequestKey, Path, 
	a.TimeStarted RequestTimeStarted, 
	a.TimeEnded RequestTimeEnded,
	dbo.ToEst(a.TimeStarted) RequestTimeStartedLocal,
	dbo.ToEst(a.TimeEnded) RequestTimeEndedLocal,
	dbo.TimeElapsedDisplayText(a.TimeStarted, a.TimeEnded) RequestTimeElapsed,
	a.ActualCount,
	SessionID, b.SessionKey, b.RequesterKey, 
	b.TimeStarted SessionTimeStarted, 
	b.TimeEnded SessionTimeEnded, 
	dbo.ToEst(b.TimeStarted) SessionTimeStartedLocal,
	dbo.ToEst(b.TimeEnded) SessionTimeEndedLocal,
	dbo.TimeElapsedDisplayText(b.timestarted,b.timeended) SessionTimeElapsed,
	b.RemoteAddress, b.UserAgent,
	b.UserID, c.UserName, c.Name UserPersonalName, c.Email, c.TimeAdded TimeUserAdded,
	ResourceID, d.Name ResourceName, 
	d.ResultType, 
	dbo.ResourceResultTypeDisplayText(d.ResultType) ResultTypeText,
	d.IsAnonymousAllowed IsAnonymousAllowedToResource,
	d.GroupID, e.Name ResourceGroupName, e.ModCategoryID ResourceGroupModCategoryID, e.IsAnonymousAllowed IsAnonymousAllowedToResourceGroup, k.VersionID,
	ModifierID, f.ModKey, f.TargetKey, f.DisplayText,
	f.CategoryID ModCategoryID, g.Name ModCategoryName, 
	g.AppID, h.Name AppName, h.TimeAdded TimeAppAdded, h.Title AppTitle, 
	h.Type AppType,
	dbo.AppTypeDisplayText(h.Type) AppTypeText,
	i.Type VersionType, 
	dbo.VersionTypeDisplayText(i.Type) VersionTypeText,
	i.Status VersionStatus, 
	dbo.VersionStatusDisplayText(i.Status) VersionStatusText,
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
inner join AppXtiVersions k
on e.AppVersionID = k.ID
inner join XtiVersions i
on i.ID = k.VersionID
inner join Apps h
on h.ID = k.AppID
"
			);
			migrationBuilder.Sql
			(
@"
ALTER view [dbo].[ExpandedSessions]
as
select 
	a.ID SessionID, SessionKey, UserID, RequesterKey, 
	TimeStarted, 
	TimeEnded, 
	dbo.ToEst(TimeStarted) TimeStartedLocal,
	dbo.ToEst(TimeEnded) TimeEndedLocal,
	dbo.TimeElapsedDisplayText(timestarted,timeended) TimeElapsed,
	RemoteAddress, UserAgent,
	b.UserName, b.Password, b.TimeAdded TimeUserAdded, b.Email, b.Name
from sessions a
inner join users b
on a.userid = b.id
"
			);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
