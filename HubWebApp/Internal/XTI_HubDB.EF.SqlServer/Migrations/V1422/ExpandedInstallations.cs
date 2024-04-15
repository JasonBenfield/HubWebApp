namespace XTI_HubDB.EF.SqlServer.Migrations.V1422;

internal static class ExpandedInstallations
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
select a.id InstallationID, a.IsCurrent, dbo.ToEst(a.TimeAdded) TimeInstallationAddedEst, a.Domain,
	dbo.InstallationStatusDisplayText(a.Status) InstallationStatusDisplayText,
	e.QualifiedMachineName,
	dbo.AppKeyDisplayText(c.DisplayText, c.Type) AppKey,
	dbo.AppKeyDisplayText(c.DisplayText, c.Type) AppTitle, 
	dbo.AppTypeDisplayText(c.Type) AppTypeDisplayText,
	d.VersionName, d.VersionKey, dbo.VersionTypeDisplayText(d.Type) VersionTypeText, 
	dbo.VersionStatusDisplayText(d.Status) VersionStatusText,
	dbo.VersionReleaseDisplayText(d.Major, d.Minor, d.Patch) VersionRelease,
	dbo.ToEst(d.TimeAdded) TimeVersionAddedEst,
	a.status InstallationStatus, a.TimeAdded TimeInstallationAdded, 
	e.ID LocationID,
	c.ID AppID, c.DisplayText AppName, c.Type AppType,
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
";
}
