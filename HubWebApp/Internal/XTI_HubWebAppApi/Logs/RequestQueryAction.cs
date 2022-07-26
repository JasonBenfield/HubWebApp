using Microsoft.AspNetCore.OData.Query;
using XTI_Core;
using XTI_HubDB.Entities;
using XTI_ODataQuery.Api;

namespace XTI_HubWebAppApi.Logs;

public sealed class ExpandedRequest
{
    public int RequestID { get; set; }
    public string Path { get; set; } = "";
    public string AppName { get; set; } = "";
    public string AppType { get; set; } = "";
    public string ResourceGroupName { get; set; } = "";
    public string ResourceName { get; set; } = "";
    public string ModCategoryName { get; set; } = "";
    public string ModKey { get; set; } = "";
    public string ModTargetKey { get; set; } = "";
    public string ModDisplayText { get; set; } = "";
    public int ActualCount { get; set; }
    public string UserName { get; set; } = "";
    public string UserGroupName { get; set; } = "";
    public DateTimeOffset TimeStarted { get; set; }
    public DateTimeOffset TimeEnded { get; set; }
    public TimeSpan? TimeElapsed { get; set; }
    public bool Succeeded { get; set; }
    public int CriticalErrorCount { get; set; }
    public string VersionName { get; set; } = "";
    public string VersionKey { get; set; } = "";
    public string VersionType { get; set; } = "";
    public string InstallLocation { get; set; } = "";
    public bool IsCurrentInstallation { get; set; }
}

internal sealed class RequestQueryAction : QueryAction<EmptyRequest, ExpandedRequest>
{
    private readonly CurrentUser currentUser;
    private readonly IHubDbContext db;

    public RequestQueryAction(CurrentUser currentUser, IHubDbContext db)
    {
        this.currentUser = currentUser;
        this.db = db;
    }

    public async Task<IQueryable<ExpandedRequest>> Execute(ODataQueryOptions<ExpandedRequest> options, EmptyRequest model)
    {
        var userGroupPermissions = await currentUser.GetUserGroupPermissions();
        var userGroupIDs = userGroupPermissions
            .Where(p => p.CanView)
            .Select(p => p.UserGroup.ToModel().ID)
            .ToArray();
        var appPermissions = await currentUser.GetAppPermissions();
        var appIDs = appPermissions
            .Where(p => p.CanView)
            .Select(p => p.App.ToModel().ID)
            .ToArray();
        return from request in db.Requests.Retrieve()
               join session in db.Sessions.Retrieve()
               on request.SessionID equals session.ID
               join user in db.Users.Retrieve()
               on session.UserID equals user.ID
               join userGroup in db.UserGroups.Retrieve()
               on new { UserGroupID = user.GroupID, Contains = true } equals new { UserGroupID = userGroup.ID, Contains = userGroupIDs.Contains(userGroup.ID) }
               join installation in db.Installations.Retrieve()
               on request.InstallationID equals installation.ID
               join appVersion in db.AppVersions.Retrieve()
               on installation.AppVersionID equals appVersion.ID
               join app in db.Apps.Retrieve()
               on new { AppID = appVersion.AppID, Contains = true } equals new { AppID = app.ID, Contains = appIDs.Contains(app.ID) }
               join resource in db.Resources.Retrieve()
               on request.ResourceID equals resource.ID
               join resourceGroup in db.ResourceGroups.Retrieve()
               on resource.GroupID equals resourceGroup.ID
               join modifier in db.Modifiers.Retrieve()
               on request.ModifierID equals modifier.ID
               join modCategory in db.ModifierCategories.Retrieve()
               on modifier.CategoryID equals modCategory.ID
               join version in db.Versions.Retrieve()
               on appVersion.VersionID equals version.ID
               join installLocation in db.InstallLocations.Retrieve()
               on installation.LocationID equals installLocation.ID
               let logEntries = (
                from logEntry in db.LogEntries.Retrieve()
                where logEntry.RequestID == request.ID
                select logEntry
                ).ToArray()
               select new ExpandedRequest
               {
                   RequestID = request.ID,
                   Path = request.Path,
                   TimeStarted = request.TimeStarted,
                   TimeEnded = request.TimeEnded,
                   TimeElapsed = request.TimeEnded.Year < 9999 ? request.TimeEnded - request.TimeStarted : null,
                   Succeeded = request.TimeEnded.Year < 9999 && !logEntries.Any(le => le.Severity > AppEventSeverity.Values.Information.Value),
                   CriticalErrorCount = logEntries.Count(le => le.Severity == AppEventSeverity.Values.CriticalError.Value),
                   ActualCount = request.ActualCount,
                   UserName = user.UserName,
                   UserGroupName = userGroup.DisplayText,
                   AppName = app.Title,
                   ResourceGroupName = resourceGroup.Name,
                   ResourceName = resource.Name,
                   ModCategoryName = modCategory.Name,
                   ModKey = modifier.ModKey,
                   ModTargetKey = modifier.TargetKey,
                   ModDisplayText = modifier.DisplayText,
                   VersionName = version.VersionName,
                   VersionKey = version.VersionKey,
                   VersionType =
                        version.Type == AppVersionType.Values.Major.Value ? "Major"
                        : version.Type == AppVersionType.Values.Minor.Value ? "Minor"
                        : version.Type == AppVersionType.Values.Patch.Value ? "Patch"
                        : "",
                   AppType = app.Type == AppType.Values.WebApp.Value ? "Web App" : "",
                   InstallLocation = installLocation.QualifiedMachineName,
                   IsCurrentInstallation = installation.IsCurrent
               };
    }
}
