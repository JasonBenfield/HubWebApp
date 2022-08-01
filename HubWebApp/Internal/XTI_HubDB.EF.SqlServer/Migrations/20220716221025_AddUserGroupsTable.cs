using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTI_HubDB.EF.SqlServer
{
    public partial class AddUserGroupsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupID",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.ID);
                });

            migrationBuilder.Sql
            (
                @"insert into UserGroups (GroupName, DisplayText) values ('xti', 'XTI')"
            );

            migrationBuilder.Sql
            (
                @"insert into UserGroups (GroupName, DisplayText) values ('general', 'General')"
            );

            migrationBuilder.Sql
            (
                @"
update users
set GroupID = (select ID from UserGroups where GroupName = 'xti')
where UserName like 'xti_%'
"
            );

            migrationBuilder.Sql
            (
                @"
update users
set GroupID = (select ID from UserGroups where GroupName = 'general')
where UserName not like 'xti_%'
"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupID",
                table: "Users",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupName",
                table: "UserGroups",
                column: "GroupName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserGroups_GroupID",
                table: "Users",
                column: "GroupID",
                principalTable: "UserGroups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

			migrationBuilder.Sql
			(
				@"
CREATE OR ALTER     FUNCTION [ToEST](
    @dt datetimeoffset
)
RETURNS datetime
AS 
BEGIN
    RETURN case when datepart(year,@dt) < 9999 or datepart(year,@dt) > 1900
		then cast(@dt at time zone 'Eastern Standard Time' as datetime) 
		else cast(@dt as datetime)
		end
END;
"
			);

			migrationBuilder.Sql
			(
				@"
CREATE or alter FUNCTION [TimeElapsedDisplayText](
    @timestarted datetimeoffset,
	@timeended datetimeoffset
)
RETURNS varchar(50)
AS 
BEGIN
    RETURN 
	case 
	when @timeended < '1-1-2' or @timeended >= '9999-12-31' or @timestarted < '1-1-2' or @timestarted >= '9999-12-31' then ''
	when datediff(year,@timestarted,@timeended) > 1 then cast(datediff(month, @TimeStarted, @TimeEnded) as varchar) + ' month'
	when datediff(day,@timestarted,@timeended) > 7 then format((datediff(hour, @TimeStarted, @TimeEnded) / 24.0), 'F2') + ' day'
	when datediff(hour,@timestarted,@timeended) > 1 then format((datediff(minute, @TimeStarted, @TimeEnded) / 60.0), 'F2') + ' hr'
	when datediff(minute,@timestarted,@timeended) > 1 then format((datediff(second, @TimeStarted, @TimeEnded) / 60.0), 'F2') + ' min'
	when datediff(second,@timestarted,@timeended) > 1 then format((datediff(millisecond, @TimeStarted, @TimeEnded) / 1000.0), 'F3') + ' s'
	else cast(datediff(millisecond, @TimeStarted, @TimeEnded) as varchar)  + ' ms'
	end
END
"
			);

            migrationBuilder.Sql
            (
				@"
CREATE OR ALTER view [ExpandedSessions] as
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
	TimeStarted, 
	TimeEnded, 
	dbo.ToEst(TimeStarted) TimeStartedLocal,
	dbo.ToEst(TimeEnded) TimeEndedLocal,
	dbo.TimeElapsedDisplayText(timestarted,timeended) TimeElapsed,
	RemoteAddress, UserAgent,
	users.GroupID UserGroupID, userGroups.GroupName UserGroupName, userGroups.DisplayText UserGroupDisplayText,
	users.UserName, users.TimeAdded TimeUserAdded, users.Email, users.Name, 
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
"
			);

            migrationBuilder.Sql
            (
				@"
CREATE OR ALTER view [ExpandedRequests] as
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
	ModifierCategories.AppID, Apps.Name AppName, Apps.TimeAdded TimeAppAdded, Apps.Title AppTitle, 
	Apps.Type AppType,
	dbo.AppTypeDisplayText(Apps.Type) AppTypeText,
	XtiVersions.VersionName, XtiVersions.VersionKey, XtiVersions.Type VersionType, 
	dbo.VersionTypeDisplayText(XtiVersions.Type) VersionTypeText,
	XtiVersions.Status VersionStatus, 
	dbo.VersionStatusDisplayText(XtiVersions.Status) VersionStatusText,
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

            migrationBuilder.Sql
            (
				@"
CREATE OR ALTER view [ExpandedLogEntries] as
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
	ModifierCategories.AppID, Apps.Name AppName, Apps.TimeAdded TimeAppAdded, Apps.Title AppTitle, 
	Apps.Type AppType,
	dbo.AppTypeDisplayText(Apps.Type) AppTypeText,
	XtiVersions.ID VersionID,
	XtiVersions.Type VersionType, 
	dbo.VersionTypeDisplayText(XtiVersions.Type) VersionTypeText,
	XtiVersions.Status VersionStatus, 
	dbo.VersionStatusDisplayText(XtiVersions.Status) VersionStatusText,
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserGroups_GroupID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_Users_GroupID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "Users");
        }
    }
}
