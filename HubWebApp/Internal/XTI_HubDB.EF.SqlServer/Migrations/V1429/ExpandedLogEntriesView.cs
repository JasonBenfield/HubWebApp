namespace XTI_HubDB.EF.SqlServer.Migrations.V1429;

internal static class ExpandedLogEntriesView
{
    public static readonly string Sql = $@"
CREATE OR ALTER view ExpandedLogEntries as
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
	LogEntries.caption TargetCaption, LogEntries.message TargetMessage,
	LogEntries.detail, LogEntries.Category,
	dbo.GetLocalDateTime(LogEntries.TimeOccurred) TimeOccurred, 
	LogEntries.Severity SeverityValue,
	dbo.EventSeverityDisplayText(LogEntries.Severity) Severity,
	LogEntries.ActualCount,
	LogEntries.RequestID, RequestKey, Path, 
	dbo.GetLocalDateTime(requests.TimeStarted) RequestTimeStarted, 
	dbo.GetLocalDateTime(requests.TimeEnded) RequestTimeEnded,
	dbo.TimeElapsedDisplayText(requests.TimeStarted, requests.TimeEnded) RequestTimeElapsed,
	Requests.RequestData, Requests.ResultData,
	SessionID, sessions.SessionKey, sessions.RequesterKey, 
	dbo.GetLocalDateTime(sessions.TimeStarted) SessionTimeStarted, 
	dbo.GetLocalDateTime(sessions.TimeEnded) SessionTimeEnded, 
	dbo.TimeElapsedDisplayText(sessions.timestarted,sessions.timeended) SessionTimeElapsed,
	sessions.RemoteAddress, sessions.UserAgent,
	sessions.UserID, users.UserName, users.Name UserPersonalName, users.Email, users.TimeAdded TimeUserAdded,
	UserGroups.ID UserGroupID, UserGroups.GroupName UserGroupName, UserGroups.DisplayText UserGroupDisplayText,
	inst.ID InstallationID, inst.Domain, inst.IsCurrent IsCurrentInstallation, inst.Status InstallationStatusValue, dbo.InstallationStatusDisplayText(inst.Status) InstallationStatus, inst.TimeAdded,
	loc.ID InstallLocationID, loc.QualifiedMachineName InstallLocation,
	ResourceID, Resources.DisplayText ResourceName, 
	Resources.ResultType ResourceResultTypeValue, 
	dbo.ResourceResultTypeDisplayText(Resources.ResultType) ResourceResultType,
	Resources.IsAnonymousAllowed IsAnonymousAllowedToResource,
	Resources.GroupID, ResourceGroups.DisplayText ResourceGroupName, ResourceGroups.ModCategoryID ResourceGroupModCategoryID, ResourceGroups.IsAnonymousAllowed IsAnonymousAllowedToResourceGroup,
	ModifierID, Modifiers.ModKeyDisplayText ModKey, Modifiers.TargetKey ModTargetKey, Modifiers.DisplayText ModDisplayText,
	Modifiers.CategoryID ModCategoryID, ModifierCategories.DisplayText ModCategoryName, 
	ModifierCategories.AppID, 
	dbo.AppKeyDisplayText(Apps.DisplayText, Apps.Type) AppKey,
	Apps.DisplayText AppName, dbo.GetLocalDateTime(Apps.TimeAdded) TimeAppAdded, 
	dbo.AppKeyDisplayText(Apps.DisplayText, Apps.Type) AppTitle, 
	Apps.Type AppTypeValue,
	dbo.AppTypeDisplayText(Apps.Type) AppType,
	XtiVersions.ID VersionID, XtiVersions.VersionName, XtiVersions.VersionKey,
	XtiVersions.Type VersionTypeValue, 
	dbo.VersionTypeDisplayText(XtiVersions.Type) VersionType,
	dbo.VersionStatusDisplayText(XtiVersions.Status) VersionStatus,
	XtiVersions.Status VersionStatusValue, 
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
";
}
