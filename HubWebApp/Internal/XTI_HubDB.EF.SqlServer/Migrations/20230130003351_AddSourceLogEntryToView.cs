using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTIHubDB.EF.SqlServer
{
    /// <inheritdoc />
    public partial class AddSourceLogEntryToView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SourceLogEntries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceID = table.Column<int>(type: "int", nullable: false),
                    TargetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceLogEntries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SourceLogEntries_LogEntries_SourceID",
                        column: x => x.SourceID,
                        principalTable: "LogEntries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SourceLogEntries_LogEntries_TargetID",
                        column: x => x.TargetID,
                        principalTable: "LogEntries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SourceLogEntries_SourceID",
                table: "SourceLogEntries",
                column: "SourceID");

            migrationBuilder.CreateIndex(
                name: "IX_SourceLogEntries_TargetID",
                table: "SourceLogEntries",
                column: "TargetID");

            migrationBuilder.Sql
            (
                @"
CREATE OR ALTER   view ExpandedLogEntries as
with
Sources as
(
	select a.TargetID, a.SourceID, b.Caption SourceCaption, b.Message SourceMessage, b.Detail SourceDetail
	from SourceLogEntries a
	inner join LogEntries b
	on a.SourceID = b.ID
)
select 
	LogEntries.ID EventID, 
	case when isnull(SourceCaption, '') = '' then LogEntries.caption else '[Source] ' + SourceCaption end Caption, 
	case when isnull(SourceMessage, '') = '' then LogEntries.message else '[Source] ' + SourceMessage end Message, 
	LogEntries.detail, 
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
	XtiVersions.TimeAdded TimeVersionAdded, XtiVersions.Major, XtiVersions.Minor, XtiVersions.Patch,
	isnull(Sources.SourceID, 0) SourceID
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
left outer join Sources
on LogEntries.ID = Sources.TargetID
"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SourceLogEntries");
        }
    }
}
