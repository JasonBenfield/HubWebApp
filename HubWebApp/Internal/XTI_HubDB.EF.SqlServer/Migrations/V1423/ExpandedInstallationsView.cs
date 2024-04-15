namespace XTI_HubDB.EF.SqlServer.Migrations.V1423;

internal static class ExpandedInstallationsView
{
    public static readonly string Sql = $@"
CREATE OR ALTER view ExpandedInstallations as
with
LatestRequests as
(
	select InstallationID, max(TimeStarted) MaxTimeStarted, count(InstallationID) RequestCount
	from Requests
	group by InstallationID
)
select inst.id InstallationID, inst.IsCurrent, 
	dbo.GetLocalDateTime(inst.TimeAdded) TimeInstallationAdded, inst.Domain,
	dbo.InstallationStatusDisplayText(inst.Status) InstallationStatus,
	instloc.QualifiedMachineName,
	dbo.AppKeyDisplayText(Apps.DisplayText, Apps.Type) AppKey,
	dbo.AppKeyDisplayText(Apps.DisplayText, Apps.Type) AppTitle, 
	dbo.AppTypeDisplayText(Apps.Type) AppType,
	vrs.VersionName, vrs.VersionKey, dbo.VersionTypeDisplayText(vrs.Type) VersionType, 
	dbo.VersionStatusDisplayText(vrs.Status) VersionStatus,
	dbo.VersionReleaseDisplayText(vrs.Major, vrs.Minor, vrs.Patch) VersionRelease,
	dbo.GetLocalDateTime(vrs.TimeAdded) TimeVersionAdded,
	inst.status InstallationStatusValue, 
	instloc.ID LocationID,
	Apps.ID AppID, Apps.DisplayText AppName, Apps.Type AppTypeValue,
	vrs.Type VersionTypeValue, vrs.Status VersionStatusValue,
	LatestRequests.MaxTimeStarted LastRequestTime, 
	datediff(day, LatestRequests.MaxTimeStarted, SysDateTimeOffset()) LastRequestDaysAgo,
	isnull(LatestRequests.RequestCount, 0) RequestCount
from Installations inst
inner join AppXtiVersions appvrs
on inst.appversionid = appvrs.id
inner join Apps
on appvrs.appid = Apps.id
inner join xtiVersions vrs
on appvrs.VersionID = vrs.ID
inner join InstallLocations instloc
on inst.LocationID = instloc.ID
left outer join LatestRequests
on inst.ID = LatestRequests.InstallationID
";
}
