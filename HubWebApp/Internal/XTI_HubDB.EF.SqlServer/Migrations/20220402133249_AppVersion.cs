using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTI_HubDB.EF.SqlServer
{
    public partial class AppVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "XtiVersions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Major = table.Column<int>(type: "int", nullable: false),
                    Minor = table.Column<int>(type: "int", nullable: false),
                    Patch = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeAdded = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XtiVersions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AppXtiVersions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppID = table.Column<int>(type: "int", nullable: false),
                    VersionID = table.Column<int>(type: "int", nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppXtiVersions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AppXtiVersions_Apps_AppID",
                        column: x => x.AppID,
                        principalTable: "Apps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppXtiVersions_XtiVersions_VersionID",
                        column: x => x.VersionID,
                        principalTable: "XtiVersions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.Sql
            (
@"
    insert into XtiVersions
    (VersionKey,GroupName,Major,Minor,Patch,Status,Type,Description,TimeAdded)
    select 
        a.VersionKey, 
	    b.Name + 
	    case b.type
	    when 10 then 'WebApp'
	    when 15 then 'ServiceApp'
	    when 25 then 'ConsoleApp'
	    else ''
	    end GroupName,
	    a.Major,a.Minor,a.Patch,a.Status,a.Type,a.Description,a.TimeAdded
    from Versions a
    inner join Apps b
    on a.AppID = b.ID
"
            );

            migrationBuilder.Sql
            (
@"
    insert into AppXtiVersions
    (AppID,VersionID,Domain)
    select a.AppID, c.ID VersionID, a.Domain
    from Versions a
    inner join Apps b
    on a.AppID = b.ID
	inner join XtiVersions c
	on 
	    b.Name + 
	    case b.type
	        when 10 then 'WebApp'
	        when 15 then 'ServiceApp'
	        when 25 then 'ConsoleApp'
	        else ''
	    end = c.GroupName and a.VersionKey = c.VersionKey
"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Installations_Versions_VersionID",
                table: "Installations");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceGroups_Versions_VersionID",
                table: "ResourceGroups");

            migrationBuilder.AddColumn<int>(
                name: "AppVersionID",
                table: "ResourceGroups",
                type: "int", 
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.Sql
            (
@"
    merge ResourceGroups as tgt
    using
    (
	    select a.ID VersionID, d.ID AppVersionID
	    from Versions a
	    inner join Apps b
	    on a.AppID = b.ID
	    inner join XtiVersions c
	    on 
		    b.Name + 
		    case b.type
			    when 10 then 'WebApp'
			    when 15 then 'ServiceApp'
			    when 25 then 'ConsoleApp'
			    else ''
		    end = c.GroupName and a.VersionKey = c.VersionKey
	    inner join AppXtiVersions d
	    on c.ID = d.VersionID and a.AppID = d.AppID
    ) as src
    on tgt.VersionID = src.VersionID
    when matched then update
	    set
		    tgt.AppVersionID = src.AppVersionID;
"
            );

            migrationBuilder.DropIndex(
                name: "IX_ResourceGroups_VersionID_Name",
                table: "ResourceGroups");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceGroups_AppVersionID_Name",
                table: "ResourceGroups",
                columns: new[] { "AppVersionID", "Name" },
                unique: true
            );

            migrationBuilder.AddColumn<int>(
                name: "AppVersionID",
                table: "Installations",
                type: "int",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.Sql
            (
@"
    merge Installations as tgt
    using
    (
	    select a.ID VersionID, d.ID AppVersionID
	    from Versions a
	    inner join Apps b
	    on a.AppID = b.ID
	    inner join XtiVersions c
	    on 
		    b.Name + 
		    case b.type
			    when 10 then 'WebApp'
			    when 15 then 'ServiceApp'
			    when 25 then 'ConsoleApp'
			    else ''
		    end = c.GroupName and a.VersionKey = c.VersionKey
	    inner join AppXtiVersions d
	    on c.ID = d.VersionID and a.AppID = d.AppID
    ) as src
    on tgt.VersionID = src.VersionID
    when matched then update
	    set
		    tgt.AppVersionID = src.AppVersionID;
"
            );

            migrationBuilder.DropIndex(
                name: "IX_Installations_VersionID",
                table: "Installations");

            migrationBuilder.CreateIndex(
                name: "IX_Installations_AppVersionID",
                table: "Installations",
                columns: new[] { "AppVersionID" }
            );

            migrationBuilder.DropIndex(
                name: "IX_Installations_LocationID_VersionID_IsCurrent",
                table: "Installations");

            migrationBuilder.CreateIndex(
                name: "IX_Installations_LocationID_AppVersionID_IsCurrent",
                table: "Installations",
                columns: new[] { "LocationID", "AppVersionID", "IsCurrent" },
                unique: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Domain",
                table: "Apps",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AppXtiVersions_AppID_VersionID",
                table: "AppXtiVersions",
                columns: new[] { "AppID", "VersionID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppXtiVersions_VersionID",
                table: "AppXtiVersions",
                column: "VersionID");

            migrationBuilder.CreateIndex(
                name: "IX_XtiVersions_GroupName_VersionKey",
                table: "XtiVersions",
                columns: new[] { "GroupName", "VersionKey" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_AppXtiVersions_AppVersionID",
                table: "Installations",
                column: "AppVersionID",
                principalTable: "AppXtiVersions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceGroups_AppXtiVersions_AppVersionID",
                table: "ResourceGroups",
                column: "AppVersionID",
                principalTable: "AppXtiVersions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropColumn(
                name: "VersionID",
                table: "ResourceGroups");

            migrationBuilder.DropColumn(
                name: "VersionID",
                table: "Installations");

            migrationBuilder.DropTable(
                name: "Versions");

            migrationBuilder.Sql
            (
@"
CREATE FUNCTION [dbo].[AppTypeDisplayText](
    @type INT
)
RETURNS varchar(50)
AS 
BEGIN
    RETURN case @type when 10 then 'Web App' when 15 then 'Service App' when 20 then 'Package' when 25 then 'Console App' else 'Unknown' end
END;
"
            );
            migrationBuilder.Sql
            (
@"
CREATE FUNCTION [dbo].[EventSeverityDisplayText](
    @severity int
)
RETURNS varchar(50)
AS 
BEGIN
    RETURN case @severity when 100 then 'Critical Error' when 80 then 'Access Denied' when 70 then 'App Error' when 60 then 'Validation Failed' else 'Unknown' end
END;
"
            );
            migrationBuilder.Sql
            (
@"
CREATE FUNCTION [dbo].[ResourceResultTypeDisplayText](
    @type INT
)
RETURNS varchar(50)
AS 
BEGIN
    RETURN case @type when 1 then 'View' when 2 then 'PartialView'when 3 then 'Redirect' when 4 then 'Json' else 'Unknown' end
END;
"
            );
            migrationBuilder.Sql
            (
@"
CREATE FUNCTION [dbo].[TimeElapsedDisplayText](
    @timestarted datetimeoffset,
	@timeended datetimeoffset
)
RETURNS varchar(50)
AS 
BEGIN
    RETURN 
	case 
	when @timeended >= '9999-12-31' then null
	when datediff(year,@timestarted,@timeended) > 1 then cast(datediff(month, @TimeStarted, @TimeEnded) as varchar) + ' month'
	when datediff(day,@timestarted,@timeended) > 7 then format((datediff(hour, @TimeStarted, @TimeEnded) / 24.0), 'F2') + ' day'
	when datediff(hour,@timestarted,@timeended) > 1 then format((datediff(minute, @TimeStarted, @TimeEnded) / 60.0), 'F2') + ' hr'
	when datediff(minute,@timestarted,@timeended) > 1 then format((datediff(second, @TimeStarted, @TimeEnded) / 60.0), 'F2') + ' min'
	when datediff(second,@timestarted,@timeended) > 1 then format((datediff(millisecond, @TimeStarted, @TimeEnded) / 1000.0), 'F3') + ' s'
	else cast(datediff(millisecond, @TimeStarted, @TimeEnded) as varchar)  + ' ms'
	end
END;
"
            );
            migrationBuilder.Sql
            (
@"
CREATE FUNCTION [dbo].[ToEST](
    @dt datetimeoffset
)
RETURNS datetime
AS 
BEGIN
    RETURN cast(@dt at time zone 'Eastern Standard Time' as datetime)
END;
"
            );
            migrationBuilder.Sql
            (
@"
CREATE FUNCTION [dbo].[VersionStatusDisplayText](
    @status INT
)
RETURNS varchar(50)
AS 
BEGIN
    RETURN case @Status when 1 then 'New' when 2 then 'Publishing' when 3 then 'Old' when 4 then 'Current' else 'Unknown' end
END;
"
            );
            migrationBuilder.Sql
            (
@"
CREATE FUNCTION [dbo].[VersionTypeDisplayText](
    @type INT
)
RETURNS varchar(50)
AS 
BEGIN
    RETURN case @type when 1 then 'Major' when 2 then 'Minor' when 3 then 'Patch' else 'Unknown' end
END;
"
            );
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
            migrationBuilder.DropForeignKey(
                name: "FK_Installations_AppXtiVersions_AppVersionID",
                table: "Installations");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceGroups_AppXtiVersions_AppVersionID",
                table: "ResourceGroups");

            migrationBuilder.DropTable(
                name: "AppXtiVersions");

            migrationBuilder.DropTable(
                name: "XtiVersions");

            migrationBuilder.RenameColumn(
                name: "AppVersionID",
                table: "ResourceGroups",
                newName: "VersionID");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceGroups_AppVersionID_Name",
                table: "ResourceGroups",
                newName: "IX_ResourceGroups_VersionID_Name");

            migrationBuilder.RenameColumn(
                name: "AppVersionID",
                table: "Installations",
                newName: "VersionID");

            migrationBuilder.RenameIndex(
                name: "IX_Installations_LocationID_AppVersionID_IsCurrent",
                table: "Installations",
                newName: "IX_Installations_LocationID_VersionID_IsCurrent");

            migrationBuilder.RenameIndex(
                name: "IX_Installations_AppVersionID",
                table: "Installations",
                newName: "IX_Installations_VersionID");

            migrationBuilder.AlterColumn<string>(
                name: "Domain",
                table: "Apps",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "Versions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Major = table.Column<int>(type: "int", nullable: false),
                    Minor = table.Column<int>(type: "int", nullable: false),
                    Patch = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TimeAdded = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    VersionKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Versions_Apps_AppID",
                        column: x => x.AppID,
                        principalTable: "Apps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Versions_AppID_VersionKey",
                table: "Versions",
                columns: new[] { "AppID", "VersionKey" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_Versions_VersionID",
                table: "Installations",
                column: "VersionID",
                principalTable: "Versions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceGroups_Versions_VersionID",
                table: "ResourceGroups",
                column: "VersionID",
                principalTable: "Versions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
