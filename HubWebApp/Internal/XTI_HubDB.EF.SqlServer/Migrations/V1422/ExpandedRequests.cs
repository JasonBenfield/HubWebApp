namespace XTI_HubDB.EF.SqlServer.Migrations.V1422;

internal static class ExpandedRequests
{
    public static readonly string Sql = """
CREATE OR ALTER view ExpandedRequests as
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
	ResourceID, Resources.DisplayText ResourceName, 
	Resources.ResultType, dbo.ResourceResultTypeDisplayText(Resources.ResultType) ResultTypeText,
	Resources.IsAnonymousAllowed IsAnonymousAllowedToResource,
	Resources.GroupID, ResourceGroups.DisplayText ResourceGroupName, 
	ResourceGroups.ModCategoryID ResourceGroupModCategoryID, 
	ResourceGroups.IsAnonymousAllowed IsAnonymousAllowedToResourceGroup, AppXtiVersions.VersionID,
	ModifierID, Modifiers.ModKeyDisplayText ModKey, Modifiers.TargetKey ModTargetKey, Modifiers.DisplayText ModDisplayText,
	Modifiers.CategoryID ModCategoryID, ModifierCategories.DisplayText ModCategoryName, 
	ModifierCategories.AppID, 
	dbo.AppKeyDisplayText(Apps.DisplayText, Apps.Type) AppKey,
	Apps.DisplayText AppName, Apps.TimeAdded TimeAppAdded, Apps.Title AppTitle, 
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
""";
}
