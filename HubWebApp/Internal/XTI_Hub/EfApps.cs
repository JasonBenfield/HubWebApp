using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfApps
{
    private readonly EfHubDB db;

    internal EfApps(EfHubDB db)
    {
        this.db = db;
    }

    internal async Task AddUnknownIfNotFound(CancellationToken ct)
    {
        var efApp = await AddOrUpdate(AppVersionName.Unknown, "", "", AppKey.Unknown, DateTimeOffset.Now, ct);
        var efVersion = await db.Versions.AddIfNotFound
        (
            AppVersionName.Unknown,
            AppVersionKey.Current,
            DateTimeOffset.Now,
            AppVersionStatus.Values.Current,
            AppVersionType.Values.Major,
            new AppVersionNumber(1, 0, 0),
            ct
        );
        await db.Versions.AddVersionToAppIfNotFound(efApp, efVersion, ct);
        var efCurrentVersion = await efApp.CurrentVersion(ct);
        await db.InstallLocations.AddUnknownIfNotFound(efCurrentVersion, ct);
        var efDefaultModCategory = await efApp.AddOrUpdateModCategory(ModifierCategoryName.Default, ct);
        await efDefaultModCategory.AddDefaultModifierIfNotFound(ct);
        var efGroup = await efCurrentVersion.AddOrUpdateResourceGroup(ResourceGroupName.Unknown, efDefaultModCategory, ct);
        await efGroup.AddOrUpdateResource(ResourceName.Unknown, ResourceResultType.Values.None, ct);
    }

    public async Task<EfApp> AddOrUpdate(AppVersionName versionName, string repoOwner, string repoName, AppKey appKey, DateTimeOffset timeAdded, CancellationToken ct)
    {
        EfApp efApp;
        var title = appKey.Format();
        var app = await GetAppByKey(appKey, ct);
        if (app == null)
        {
            efApp = await Add(versionName, repoOwner, repoName, appKey, title, timeAdded, ct);
        }
        else
        {
            await db.Context.Apps.Update
            (
                app,
                r =>
                {
                    r.DisplayText = appKey.Name.DisplayText;
                    r.VersionName = versionName.Value;
                    if (!string.IsNullOrWhiteSpace(repoOwner))
                    {
                        r.RepoOwner = repoOwner;
                    }
                    if (!string.IsNullOrWhiteSpace(repoName))
                    {
                        r.RepoName = repoName;
                    }
                    r.Title = title.Trim();
                },
                ct
            );
            efApp = new EfApp(db, app);
        }
        var efVersion = await db.Versions.AddCurrentVersionIfNotFound(versionName, timeAdded, ct);
        await efApp.AddVersionIfNotFound(efVersion, ct);
        return efApp;
    }

    private async Task<EfApp> Add(AppVersionName versionName, string repoOwner, string repoName, AppKey appKey, string title, DateTimeOffset timeAdded, CancellationToken ct)
    {
        var app = await AddEntity(versionName, repoOwner, repoName, appKey, title, timeAdded, ct);
        var efApp = new EfApp(db, app);
        if (!appKey.IsAnyAppType(AppType.Values.Package, AppType.Values.WebPackage))
        {
            var efDefaultModCategory = await efApp.AddOrUpdateModCategory(ModifierCategoryName.Default, ct);
            await efDefaultModCategory.AddDefaultModifierIfNotFound(ct);
        }
        return efApp;
    }

    private async Task<AppEntity> AddEntity(AppVersionName versionName, string repoOwner, string repoName, AppKey appKey, string title, DateTimeOffset timeAdded, CancellationToken ct)
    {
        var app = new AppEntity
        {
            Name = appKey.Name.Value,
            DisplayText = appKey.Name.DisplayText,
            Type = appKey.Type.Value,
            Title = title.Trim(),
            RepoOwner = repoOwner,
            RepoName = repoName,
            VersionName = versionName.Value,
            TimeAdded = timeAdded
        };
        await db.Context.Apps.Create(app, ct);
        return app;
    }

    public async Task<IEnumerable<EfApp>> All(CancellationToken ct)
    {
        var apps = await db.Context.Apps.Retrieve()
            .OrderBy(a => a.DisplayText)
            .ToArrayAsync(ct);
        return apps.Select(a => new EfApp(db, a));
    }

    public async Task<EfApp> App(int appID, CancellationToken ct)
    {
        var app = await db.Context.Apps.Retrieve()
            .Where(a => a.ID == appID)
            .FirstOrDefaultAsync(ct);
        return new EfApp(db, app ?? throw new Exception($"App {appID} not found"));
    }

    public async Task<EfApp> App(AppKey appKey, CancellationToken ct)
    {
        var app = await GetAppByKey(appKey, ct);
        return new EfApp
        (
            db,
            app ?? throw new ArgumentNullException($"App '{appKey.Name.DisplayText} {appKey.Type.DisplayText}' not found")
        );
    }

    private Task<AppEntity?> GetAppByKey(AppKey appKey, CancellationToken ct) =>
        db.Context.Apps.Retrieve()
            .Where(a => a.Name == appKey.Name.Value && a.Type == appKey.Type.Value)
            .FirstOrDefaultAsync(ct);

    public async Task<EfApp> AppOrUnknown(AppKey appKey, CancellationToken ct)
    {
        var app = await GetAppByKey(appKey, ct);
        if (app == null && !appKey.Equals(AppKey.Unknown))
        {
            app = await GetAppByKey(AppKey.Unknown, ct);
        }
        return new EfApp
        (
            db,
            app ?? throw new ArgumentNullException($"App '{appKey.Name.DisplayText} {appKey.Type.DisplayText}' not found")
        );
    }

    public Task<EfApp[]> WebAppsWithOpenSessions(EfAppUser user, CancellationToken ct)
    {
        var sessionIDs = db.Context.Sessions.Retrieve()
            .Where(s => s.UserID == user.ID && s.TimeEnded > DateTimeOffset.Now.AddDays(1))
            .Select(s => s.ID);
        var appIDs = db.Context.Requests.Retrieve()
            .Where(r => sessionIDs.Any(id => id == r.SessionID))
            .Join
            (
                db.Context.Resources.Retrieve(),
                req => req.ResourceID,
                res => res.ID,
                (req, res) => new
                {
                    GroupID = res.GroupID
                }
            )
            .Join
            (
                db.Context.ResourceGroups.Retrieve(),
                res => res.GroupID,
                grp => grp.ID,
                (res, grp) => new
                {
                    AppVersionID = grp.AppVersionID
                }
            )
            .Join
            (
                db.Context.AppVersions.Retrieve(),
                grp => grp.AppVersionID,
                av => av.ID,
                (grp, v) => v.AppID
            )
            .Distinct();
        return db.Context.Apps.Retrieve()
            .Where(a => a.Type == AppType.Values.WebApp && appIDs.Any(id => id == a.ID))
            .Select(a => new EfApp(db, a))
            .ToArrayAsync(ct);
    }
}