using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTI_HubDB.EF.SqlServer
{
    public partial class ExpandedInstallationsView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql
            (
				@"
CREATE OR ALTER   FUNCTION [TimeElapsedDisplayText](
    @timestarted datetimeoffset,
	@timeended datetimeoffset
)
RETURNS varchar(50)
AS 
BEGIN
    RETURN 
	case 
	when @timeended < '1-1-2' or @timeended >= '9999-12-31' or @timestarted < '1-1-2' or @timestarted >= '9999-12-31' then null
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
create or alter view ExpandedInstallations as
select a.id InstallationID, a.IsCurrent, dbo.ToEst(a.TimeAdded) TimeInstallationAddedEst, a.Domain,
	dbo.InstallationStatusDisplayText(a.Status) InstallationStatusDisplayText,
	e.QualifiedMachineName,
	c.title AppTitle, dbo.AppTypeDisplayText(c.Type) AppTypeDisplayText,
	d.VersionName, d.VersionKey, dbo.VersionTypeDisplayText(d.Type) VersionTypeDisplayText, 
	dbo.VersionStatusDisplayText(d.Status) VersionStatusDisplayText,
	d.Major, d.Minor, d.Patch, dbo.ToEst(d.TimeAdded) TimeVersionAddedEst,
	a.status InstallationStatus, a.TimeAdded TimeInstallationAdded, 
	e.ID LocationID,
	c.ID AppID, c.Name AppName, c.Type AppType,
	d.TimeAdded TimeVersionAdded, d.Type VersionType, d.Status VersionStatus
from installations a
inner join AppXtiVersions b
on a.appversionid = b.id
inner join apps c
on b.appid = c.id
inner join xtiVersions d
on b.VersionID = d.ID
inner join InstallLocations e
on a.LocationID = e.ID
"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
