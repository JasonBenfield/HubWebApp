using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTI_HubDB.EF.SqlServer
{
    public partial class LogInstallerIDandRenameEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql
            (
                "delete from Events"
            );
            migrationBuilder.Sql
            (
                "delete from Requests"
            );
            migrationBuilder.Sql
            (
                "delete from Sessions"
            );
            migrationBuilder.AddColumn<int>(
                name: "InstallationID",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LogEntries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", maxLength: 32000, nullable: false),
                    TimeOccurred = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ActualCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LogEntries_Requests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_InstallationID",
                table: "Requests",
                column: "InstallationID");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_EventKey",
                table: "LogEntries",
                column: "EventKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_RequestID",
                table: "LogEntries",
                column: "RequestID");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Installations_InstallationID",
                table: "Requests",
                column: "InstallationID",
                principalTable: "Installations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.Sql
            (
                @"
alter view [dbo].[ExpandedSessions] as
with
RequestCounts as
(
	select SessionID, count(sessionID) RequestCount
	from Requests
	group by sessionid
)
select 
	a.ID SessionID, SessionKey, UserID, RequesterKey, 
	TimeStarted, 
	TimeEnded, 
	dbo.ToEst(TimeStarted) TimeStartedLocal,
	dbo.ToEst(TimeEnded) TimeEndedLocal,
	dbo.TimeElapsedDisplayText(timestarted,timeended) TimeElapsed,
	RemoteAddress, UserAgent,
	b.UserName, b.Password, b.TimeAdded TimeUserAdded, b.Email, b.Name, 
	isnull(c.RequestCount, 0) requestCount
from sessions a
inner join users b
on a.userid = b.id
left outer join RequestCounts c
on a.id = c.sessionid
"
            );
            migrationBuilder.Sql
            (
                @"
CREATE FUNCTION [dbo].[InstallationStatusDisplayText](
    @status INT
)
RETURNS varchar(50)
AS 
BEGIN
    RETURN case @Status 
		when 10 then 'InstallPending' when 20 then 'InstallStarted' when 30 then 'Installed' 
		when 40 then 'DeletePending' when 50 then 'DeleteStarted' when 60 then 'Deleted' 
		else 'Unknown' end
END;
"
            );
            migrationBuilder.Sql
            (
                @"
ALTER view [dbo].[ExpandedRequests] as
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
	a.id RequestID, RequestKey, Path, 
	a.TimeStarted RequestTimeStarted, 
	a.TimeEnded RequestTimeEnded,
	dbo.ToEst(a.TimeStarted) RequestTimeStartedLocal,
	dbo.ToEst(a.TimeEnded) RequestTimeEndedLocal,
	dbo.TimeElapsedDisplayText(a.TimeStarted, a.TimeEnded) RequestTimeElapsed,
	a.ActualCount,
	cast(case when a.TimeEnded < '9999-12-31 23:59:59.9999999 +00:00' and errorCounts.ErrorCount is null then 1 else 0 end as bit) Succeeded,
	isnull(criticalErrorCounts.LogEntryCount, 0) CriticalErrorCount,
	isnull(accessDeniedCounts.LogEntryCount, 0) AccessDeniedCount,
	isnull(appErrorCounts.LogEntryCount, 0) AppErrorCount,
	isnull(validationFailedCounts.LogEntryCount, 0) ValidationFailedCount,
	isnull(errorCounts.ErrorCount, 0) TotalErrorCount,
	isnull(informationCounts.LogEntryCount, 0) InformationMessageCount,
	SessionID, b.SessionKey, b.RequesterKey, 
	b.TimeStarted SessionTimeStarted, 
	b.TimeEnded SessionTimeEnded, 
	dbo.ToEst(b.TimeStarted) SessionTimeStartedLocal,
	dbo.ToEst(b.TimeEnded) SessionTimeEndedLocal,
	dbo.TimeElapsedDisplayText(b.timestarted,b.timeended) SessionTimeElapsed,
	b.RemoteAddress, b.UserAgent,
	b.UserID, c.UserName, c.Name UserPersonalName, c.Email, c.TimeAdded TimeUserAdded,
	inst.ID InstallationID, inst.Domain, inst.IsCurrent, inst.Status InstallationStatus, dbo.InstallationStatusDisplayText(inst.Status) InstallationStatusDisplayText, inst.TimeAdded,
	loc.ID InstallLocationID, loc.QualifiedMachineName InstallLocation,
	ResourceID, d.Name ResourceName, 
	d.ResultType, dbo.ResourceResultTypeDisplayText(d.ResultType) ResultTypeText,
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
inner join Installations inst
on a.InstallationID = inst.id
inner join InstallLocations loc
on inst.LocationID = loc.id
left outer join LogEntrySeverityCounts criticalErrorCounts
on a.ID = criticalErrorCounts.RequestID and criticalErrorCounts.Severity = 100
left outer join LogEntrySeverityCounts accessDeniedCounts
on a.ID = accessDeniedCounts.RequestID and accessDeniedCounts.Severity = 80
left outer join LogEntrySeverityCounts appErrorCounts
on a.ID = appErrorCounts.RequestID and appErrorCounts.Severity = 70
left outer join LogEntrySeverityCounts validationFailedCounts
on a.ID = validationFailedCounts.RequestID and validationFailedCounts.Severity = 60
left outer join LogEntrySeverityCounts informationCounts
on a.ID = informationCounts.RequestID and informationCounts.Severity = 50
left outer join ErrorCounts errorCounts
on a.ID = errorCounts.RequestID
"
            );
            migrationBuilder.Sql
            (
                @"
drop view [dbo].[ExpandedEvents]
"
            );
            migrationBuilder.Sql
            (
                @"
CREATE view [dbo].[ExpandedLogEntries] as
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
	inst.ID InstallationID, inst.Domain, inst.IsCurrent, inst.Status InstallationStatus, dbo.InstallationStatusDisplayText(inst.Status) InstallationStatusDisplayText, inst.TimeAdded,
	loc.ID InstallLocationID, loc.QualifiedMachineName InstallLocation,
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
from LogEntries j
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
inner join Installations inst
on a.InstallationID = inst.id
inner join InstallLocations loc
on inst.LocationID = loc.id
"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Installations_InstallationID",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "LogEntries");

            migrationBuilder.DropIndex(
                name: "IX_Requests_InstallationID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "InstallationID",
                table: "Requests");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActualCount = table.Column<int>(type: "int", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", maxLength: 32000, nullable: false),
                    EventKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    TimeOccurred = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Events_Requests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventKey",
                table: "Events",
                column: "EventKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_RequestID",
                table: "Events",
                column: "RequestID");
        }
    }
}
