namespace XTI_HubDB.EF.SqlServer.V1423;

internal static class ExpandedRequestsView
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
	Requests.id RequestID, Requests.RequestKey,
	Requests.Path, 
	dbo.GetLocalDateTime(Requests.TimeStarted) RequestTimeStarted, 
	dbo.GetLocalDateTime(Requests.TimeEnded) RequestTimeEnded,
	dbo.TimeElapsedDisplayText(Requests.TimeStarted, Requests.TimeEnded) RequestTimeElapsed,
	Requests.ActualCount,
	cast(case when datepart(year,Requests.TimeEnded) < 9999 and errorCounts.ErrorCount is null then 1 else 0 end as bit) Succeeded, 
	isnull(srcreq.SourceRequestID, 0) SourceRequestID,
	case isnull(srcreq.SourceRequestID, 0) when 0 then 0 else 1 end HasSourceRequest,
	srcreq.Path SourceRequestPath,
	isnull(criticalErrorCounts.LogEntryCount, 0) CriticalErrorCount,
	isnull(accessDeniedCounts.LogEntryCount, 0) AccessDeniedCount,
	isnull(appErrorCounts.LogEntryCount, 0) AppErrorCount,
	isnull(validationFailedCounts.LogEntryCount, 0) ValidationFailedCount,
	isnull(errorCounts.ErrorCount, 0) TotalErrorCount,
	isnull(informationCounts.LogEntryCount, 0) InformationMessageCount,
	SessionID, sessions.SessionKey, sessions.RequesterKey, 
	dbo.GetLocalDateTime(sessions.TimeStarted) SessionTimeStarted, 
	dbo.GetLocalDateTime(sessions.TimeEnded) SessionTimeEnded, 
	dbo.TimeElapsedDisplayText(sessions.timestarted,sessions.timeended) SessionTimeElapsed,
	sessions.RemoteAddress, sessions.UserAgent,
	users.ID UserID, 
	users.UserName, 
	users.Name UserPersonalName, users.Email, 
	dbo.GetLocalDateTime(users.TimeAdded) TimeUserAdded, 
	dbo.GetLocalDateTime(users.TimeLoggedIn) TimeUserLoggedIn, 
	dbo.GetLocalDateTime(users.TimeDeactivated) TimeUserDeactivated,
	UserGroups.ID UserGroupID, UserGroups.GroupName UserGroupName, UserGroups.DisplayText UserGroupDisplayText,
	inst.ID InstallationID, inst.Domain, inst.IsCurrent IsCurrentInstallation, inst.Status InstallationStatusValue, dbo.InstallationStatusDisplayText(inst.Status) InstallationStatus, 
	dbo.GetLocalDateTime(inst.TimeAdded) TimeInstallationAdded,
	loc.ID InstallLocationID, loc.QualifiedMachineName InstallLocation,
	ResourceID, Resources.DisplayText ResourceName, 
	Resources.ResultType ResourceResultTypeValue, 
	dbo.ResourceResultTypeDisplayText(Resources.ResultType) ResourceResultType,
	Resources.IsAnonymousAllowed IsAnonymousAllowedToResource,
	Resources.GroupID, ResourceGroups.DisplayText ResourceGroupName, 
	ResourceGroups.ModCategoryID ResourceGroupModCategoryID, 
	ResourceGroups.IsAnonymousAllowed IsAnonymousAllowedToResourceGroup, AppXtiVersions.VersionID,
	ModifierID, mod.ModKeyDisplayText ModKey, mod.TargetKey ModTargetKey, mod.DisplayText ModDisplayText,
	mod.CategoryID ModCategoryID, modcat.DisplayText ModCategoryName, 
	Apps.ID AppID, 
	dbo.AppKeyDisplayText(Apps.DisplayText, Apps.Type) AppKey,
	Apps.DisplayText AppName, dbo.GetLocalDateTime(Apps.TimeAdded) TimeAppAdded, Apps.Title AppTitle, 
	Apps.Type AppTypeValue,
	dbo.AppTypeDisplayText(Apps.Type) AppType,
	XtiVersions.VersionName, XtiVersions.VersionKey, XtiVersions.Type VersionTypeValue, 
	dbo.VersionTypeDisplayText(XtiVersions.Type) VersionType,
	XtiVersions.Status VersionStatusValue, 
	dbo.VersionStatusDisplayText(XtiVersions.Status) VersionStatus,
	dbo.VersionReleaseDisplayText(XtiVersions.Major, XtiVersions.Minor, XtiVersions.Patch) VersionRelease,
	dbo.GetLocalDateTime(XtiVersions.TimeAdded) TimeVersionAdded, XtiVersions.Major, XtiVersions.Minor, XtiVersions.Patch
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
inner join Modifiers mod
on Requests.ModifierID = mod.ID
inner join ModifierCategories modcat
on mod.CategoryID = modcat.ID
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
left outer join SourceRequests srcreq
on Requests.ID = srcreq.TargetID
""";
}
