using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTI_HubDB.EF.SqlServer
{
    public partial class AddRequestsToExpandedInstallations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql
			(
				@"
CREATE or ALTER FUNCTION [dbo].[AppKeyDisplayText](
	@name varchar(50),
    @type INT
)
RETURNS varchar(100)
AS 
BEGIN
    RETURN @name + ' ' + dbo.AppTypeDisplayText(@type)
END
"
			);
            migrationBuilder.Sql
            (
                @"
CREATE or alter FUNCTION [dbo].[VersionReleaseDisplayText](
    @major INT,
    @minor INT,
    @patch INT
)
RETURNS varchar(50)
AS 
BEGIN
    RETURN 'v' + cast(@major as varchar) + '.' + cast(@minor as varchar) + '.' + cast(@patch as varchar)
END"
            );
            migrationBuilder.Sql
            (
                @"
CREATE OR ALTER   view [dbo].[ExpandedInstallations] as
with
LatestRequests as
(
	select InstallationID, max(TimeStarted) MaxTimeStarted, count(InstallationID) RequestCount
	from Requests
	group by InstallationID
)
select a.id InstallationID, a.IsCurrent, dbo.ToEst(a.TimeAdded) TimeInstallationAddedEst, a.Domain,
	dbo.InstallationStatusDisplayText(a.Status) InstallationStatusDisplayText,
	e.QualifiedMachineName,
	dbo.AppKeyDisplayText(c.Name, c.Type) AppKey,
	c.title AppTitle, dbo.AppTypeDisplayText(c.Type) AppTypeDisplayText,
	d.VersionName, d.VersionKey, dbo.VersionTypeDisplayText(d.Type) VersionTypeText, 
	dbo.VersionStatusDisplayText(d.Status) VersionStatusText,
	dbo.VersionReleaseDisplayText(d.Major, d.Minor, d.Patch) VersionRelease,
	dbo.ToEst(d.TimeAdded) TimeVersionAddedEst,
	a.status InstallationStatus, a.TimeAdded TimeInstallationAdded, 
	e.ID LocationID,
	c.ID AppID, c.Name AppName, c.Type AppType,
	d.TimeAdded TimeVersionAdded, d.Type VersionType, d.Status VersionStatus,
	LatestRequests.MaxTimeStarted LastRequestTime, 
	datediff(day, LatestRequests.MaxTimeStarted, SysDateTimeOffset()) LastRequestDaysAgo,
	isnull(LatestRequests.RequestCount, 0) RequestCount
from installations a
inner join AppXtiVersions b
on a.appversionid = b.id
inner join apps c
on b.appid = c.id
inner join xtiVersions d
on b.VersionID = d.ID
inner join InstallLocations e
on a.LocationID = e.ID
left outer join LatestRequests
on a.ID = LatestRequests.InstallationID
"
            );
            migrationBuilder.Sql
            (
                @"
CREATE OR ALTER   view [dbo].[ExpandedLogEntries] as
select 
	LogEntries.ID EventID, LogEntries.caption, LogEntries.message, LogEntries.detail, 
	LogEntries.TimeOccurred, 
	dbo.ToEst(LogEntries.TimeOccurred) TimeOccurredLocal,
	LogEntries.Severity,
	dbo.EventSeverityDisplayText(LogEntries.Severity) SeverityText,
	LogEntries.ActualCount,
	LogEntries.RequestID, RequestKey, Path, 
	requests.TimeStarted RequestTimeStarted, 
	requests.TimeEnded RequestTimeEnded,
	dbo.ToEst(requests.TimeStarted) RequestTimeStartedLocal,
	dbo.ToEst(requests.TimeEnded) RequestTimeEndedLocal,
	dbo.TimeElapsedDisplayText(requests.TimeStarted, requests.TimeEnded) RequestTimeElapsed,
	SessionID, sessions.SessionKey, sessions.RequesterKey, 
	sessions.TimeStarted SessionTimeStarted, 
	sessions.TimeEnded SessionTimeEnded, 
	dbo.ToEst(sessions.TimeStarted) SessionTimeStartedLocal,
	dbo.ToEst(sessions.TimeEnded) SessionTimeEndedLocal,
	dbo.TimeElapsedDisplayText(sessions.timestarted,sessions.timeended) SessionTimeElapsed,
	sessions.RemoteAddress, sessions.UserAgent,
	sessions.UserID, users.UserName, users.Name UserPersonalName, users.Email, users.TimeAdded TimeUserAdded,
	UserGroups.ID UserGroupID, UserGroups.GroupName UserGroupName, UserGroups.DisplayText UserGroupDisplayText,
	inst.ID InstallationID, inst.Domain, inst.IsCurrent IsCurrentInstallation, inst.Status InstallationStatus, dbo.InstallationStatusDisplayText(inst.Status) InstallationStatusDisplayText, inst.TimeAdded,
	loc.ID InstallLocationID, loc.QualifiedMachineName InstallLocation,
	ResourceID, Resources.Name ResourceName, 
	Resources.ResultType, 
	dbo.ResourceResultTypeDisplayText(Resources.ResultType) ResultTypeText,
	Resources.IsAnonymousAllowed IsAnonymousAllowedToResource,
	Resources.GroupID, ResourceGroups.Name ResourceGroupName, ResourceGroups.ModCategoryID ResourceGroupModCategoryID, ResourceGroups.IsAnonymousAllowed IsAnonymousAllowedToResourceGroup,
	ModifierID, Modifiers.ModKey, Modifiers.TargetKey ModTargetKey, Modifiers.DisplayText ModDisplayText,
	Modifiers.CategoryID ModCategoryID, ModifierCategories.Name ModCategoryName, 
	ModifierCategories.AppID, 
	dbo.AppKeyDisplayText(Apps.Name, Apps.Type) AppKey,
	Apps.Name AppName, Apps.TimeAdded TimeAppAdded, Apps.Title AppTitle, 
	Apps.Type AppType,
	dbo.AppTypeDisplayText(Apps.Type) AppTypeText,
	XtiVersions.ID VersionID, XtiVersions.VersionName, XtiVersions.VersionKey,
	XtiVersions.Type VersionType, 
	dbo.VersionTypeDisplayText(XtiVersions.Type) VersionTypeText,
	XtiVersions.Status VersionStatus, 
	dbo.VersionStatusDisplayText(XtiVersions.Status) VersionStatusText,
	dbo.VersionReleaseDisplayText(XtiVersions.Major, XtiVersions.Minor, XtiVersions.Patch) VersionRelease,
	XtiVersions.TimeAdded TimeVersionAdded, XtiVersions.Major, XtiVersions.Minor, XtiVersions.Patch
from LogEntries
inner join requests
on LogEntries.requestid = requests.id
inner join sessions
on requests.SessionID = sessions.id
inner join users
on sessions.userid = users.id
inner join UserGroups 
on users.GroupID = UserGroups.ID
inner join Resources
on requests.ResourceID = Resources.ID
inner join ResourceGroups
on Resources.GroupID = ResourceGroups.ID 
inner join Modifiers
on requests.ModifierID = Modifiers.ID
inner join ModifierCategories
on Modifiers.CategoryID = ModifierCategories.ID
inner join AppXtiVersions
on ResourceGroups.AppVersionID = AppXtiVersions.ID
inner join XtiVersions
on XtiVersions.ID = AppXtiVersions.VersionID
inner join Apps
on Apps.ID = AppXtiVersions.AppID
inner join Installations inst
on requests.InstallationID = inst.id
inner join InstallLocations loc
on inst.LocationID = loc.id
"
            );
			migrationBuilder.Sql
			(
                @"
CREATE OR ALTER   view [dbo].[ExpandedRequests] as
with 
LogEntrySeverityCounts as
(
	select RequestID, Severity, count(RequestID) LogEntryCount
	from LogEntries
	group by RequestID, Severity
),
ErrorCounts as
(
	select RequestID, count(RequestID) ErrorCount
	from LogEntries
	where Severity > 50
	group by RequestID
)
select 
	Requests.id RequestID, RequestKey, Path, 
	Requests.TimeStarted RequestTimeStarted, 
	Requests.TimeEnded RequestTimeEnded,
	dbo.ToEst(Requests.TimeStarted) RequestTimeStartedLocal,
	dbo.ToEst(Requests.TimeEnded) RequestTimeEndedLocal,
	dbo.TimeElapsedDisplayText(Requests.TimeStarted, Requests.TimeEnded) RequestTimeElapsed,
	Requests.ActualCount,
	cast(case when datepart(year,Requests.TimeEnded) < 9999 and errorCounts.ErrorCount is null then 1 else 0 end as bit) Succeeded,
	isnull(criticalErrorCounts.LogEntryCount, 0) CriticalErrorCount,
	isnull(accessDeniedCounts.LogEntryCount, 0) AccessDeniedCount,
	isnull(appErrorCounts.LogEntryCount, 0) AppErrorCount,
	isnull(validationFailedCounts.LogEntryCount, 0) ValidationFailedCount,
	isnull(errorCounts.ErrorCount, 0) TotalErrorCount,
	isnull(informationCounts.LogEntryCount, 0) InformationMessageCount,
	SessionID, sessions.SessionKey, sessions.RequesterKey, 
	sessions.TimeStarted SessionTimeStarted, 
	sessions.TimeEnded SessionTimeEnded, 
	dbo.ToEst(sessions.TimeStarted) SessionTimeStartedLocal,
	dbo.ToEst(sessions.TimeEnded) SessionTimeEndedLocal,
	dbo.TimeElapsedDisplayText(sessions.timestarted,sessions.timeended) SessionTimeElapsed,
	sessions.RemoteAddress, sessions.UserAgent,
	sessions.UserID, users.UserName, users.Name UserPersonalName, users.Email, users.TimeAdded TimeUserAdded,
	UserGroups.ID UserGroupID, UserGroups.GroupName UserGroupName, UserGroups.DisplayText UserGroupDisplayText,
	inst.ID InstallationID, inst.Domain, inst.IsCurrent IsCurrentInstallation, inst.Status InstallationStatus, dbo.InstallationStatusDisplayText(inst.Status) InstallationStatusDisplayText, inst.TimeAdded,
	loc.ID InstallLocationID, loc.QualifiedMachineName InstallLocation,
	ResourceID, Resources.Name ResourceName, 
	Resources.ResultType, dbo.ResourceResultTypeDisplayText(Resources.ResultType) ResultTypeText,
	Resources.IsAnonymousAllowed IsAnonymousAllowedToResource,
	Resources.GroupID, ResourceGroups.Name ResourceGroupName, 
	ResourceGroups.ModCategoryID ResourceGroupModCategoryID, 
	ResourceGroups.IsAnonymousAllowed IsAnonymousAllowedToResourceGroup, AppXtiVersions.VersionID,
	ModifierID, Modifiers.ModKey, Modifiers.TargetKey ModTargetKey, Modifiers.DisplayText ModDisplayText,
	Modifiers.CategoryID ModCategoryID, ModifierCategories.Name ModCategoryName, 
	ModifierCategories.AppID, 
	dbo.AppKeyDisplayText(Apps.Name, Apps.Type) AppKey,
	Apps.Name AppName, Apps.TimeAdded TimeAppAdded, Apps.Title AppTitle, 
	Apps.Type AppType,
	dbo.AppTypeDisplayText(Apps.Type) AppTypeText,
	XtiVersions.VersionName, XtiVersions.VersionKey, XtiVersions.Type VersionType, 
	dbo.VersionTypeDisplayText(XtiVersions.Type) VersionTypeText,
	XtiVersions.Status VersionStatus, 
	dbo.VersionStatusDisplayText(XtiVersions.Status) VersionStatusText,
	dbo.VersionReleaseDisplayText(XtiVersions.Major, XtiVersions.Minor, XtiVersions.Patch) VersionRelease,
	XtiVersions.TimeAdded TimeVersionAdded, XtiVersions.Major, XtiVersions.Minor, XtiVersions.Patch
from requests
inner join sessions
on Requests.SessionID = sessions.id
inner join users
on sessions.userid = users.id
inner join UserGroups 
on users.GroupID = UserGroups.ID
inner join Resources
on Requests.ResourceID = Resources.ID
inner join ResourceGroups
on Resources.GroupID = ResourceGroups.ID 
inner join Modifiers
on Requests.ModifierID = Modifiers.ID
inner join ModifierCategories
on Modifiers.CategoryID = ModifierCategories.ID
inner join AppXtiVersions
on ResourceGroups.AppVersionID = AppXtiVersions.ID
inner join XtiVersions
on XtiVersions.ID = AppXtiVersions.VersionID
inner join Apps
on Apps.ID = AppXtiVersions.AppID
inner join Installations inst
on Requests.InstallationID = inst.id
inner join InstallLocations loc
on inst.LocationID = loc.id
left outer join LogEntrySeverityCounts criticalErrorCounts
on Requests.ID = criticalErrorCounts.RequestID and criticalErrorCounts.Severity = 100
left outer join LogEntrySeverityCounts accessDeniedCounts
on Requests.ID = accessDeniedCounts.RequestID and accessDeniedCounts.Severity = 80
left outer join LogEntrySeverityCounts appErrorCounts
on Requests.ID = appErrorCounts.RequestID and appErrorCounts.Severity = 70
left outer join LogEntrySeverityCounts validationFailedCounts
on Requests.ID = validationFailedCounts.RequestID and validationFailedCounts.Severity = 60
left outer join LogEntrySeverityCounts informationCounts
on Requests.ID = informationCounts.RequestID and informationCounts.Severity = 50
left outer join ErrorCounts errorCounts
on Requests.ID = errorCounts.RequestID
"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
