using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppRepository
{
    private readonly HubFactory factory;

    internal AppRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    internal async Task AddUnknownIfNotFound()
    {
        var app = await AddOrUpdate(AppVersionName.Unknown, AppKey.Unknown, DateTimeOffset.Now);
        var version = await factory.Versions.AddIfNotFound
        (
            AppVersionName.Unknown,
            AppVersionKey.Current,
            DateTimeOffset.Now,
            AppVersionStatus.Values.Current,
            AppVersionType.Values.Major,
            new AppVersionNumber(1, 0, 0)
        );
        await factory.Versions.AddVersionToAppIfNotFound(app, version);
        var currentVersion = await app.CurrentVersion();
        await factory.InstallLocations.AddUnknownIfNotFound(currentVersion);
        var defaultModCategory = await app.AddModCategoryIfNotFound(ModifierCategoryName.Default);
        await defaultModCategory.AddDefaultModifierIfNotFound();
        var group = await currentVersion.AddOrUpdateResourceGroup(ResourceGroupName.Unknown, defaultModCategory);
        await group.AddOrUpdateResource(ResourceName.Unknown, ResourceResultType.Values.None);
    }

    public async Task<App> AddOrUpdate(AppVersionName versionName, AppKey appKey, DateTimeOffset timeAdded)
    {
        App app;
        var title = appKey.Format();
        var record = await GetAppByKey(appKey);
        if (record == null)
        {
            app = await Add(versionName, appKey, title, timeAdded);
        }
        else
        {
            await factory.DB.Apps.Update
            (
                record, 
                r =>
                {
                    r.DisplayText = appKey.Name.DisplayText;
                    r.VersionName = versionName.Value;
                    r.Title = title.Trim();
                }
            );
            app = factory.CreateApp(record);
        }
        var version = await factory.Versions.AddCurrentVersionIfNotFound(versionName, timeAdded);
        await app.AddVersionIfNotFound(version);
        return app;
    }

    private async Task<App> Add(AppVersionName versionName, AppKey appKey, string title, DateTimeOffset timeAdded)
    {
        App? app = null;
        await factory.Transaction(async () =>
        {
            var entity = await AddEntity(versionName, appKey, title, timeAdded);
            app = factory.CreateApp(entity);
            if (!appKey.IsAnyAppType(AppType.Values.Package, AppType.Values.WebPackage))
            {
                var defaultModCategory = await app.AddModCategoryIfNotFound(ModifierCategoryName.Default);
                await defaultModCategory.AddDefaultModifierIfNotFound();
            }
        });
        return app ?? throw new ArgumentNullException(nameof(app));
    }

    private async Task<AppEntity> AddEntity(AppVersionName versionName, AppKey appKey, string title, DateTimeOffset timeAdded)
    {
        var record = new AppEntity
        {
            Name = appKey.Name.Value,
            DisplayText = appKey.Name.DisplayText,
            Type = appKey.Type.Value,
            Title = title.Trim(),
            VersionName = versionName.Value,
            TimeAdded = timeAdded
        };
        await factory.DB.Apps.Create(record);
        return record;
    }

    public async Task<IEnumerable<App>> All()
    {
        var records = await factory.DB.Apps.Retrieve().ToArrayAsync();
        return records.Select(r => factory.CreateApp(r));
    }

    public async Task<App> App(int id)
    {
        var record = await factory.DB.Apps.Retrieve().FirstOrDefaultAsync(a => a.ID == id);
        return factory.CreateApp(record ?? throw new Exception($"App {id} not found"));
    }

    public async Task<App> App(AppKey appKey)
    {
        var record = await GetAppByKey(appKey);
        return factory.CreateApp
        (
            record ?? throw new ArgumentNullException($"App '{appKey.Name.DisplayText} {appKey.Type.DisplayText}' not found")
        );
    }

    private Task<AppEntity?> GetAppByKey(AppKey appKey) =>
        factory.DB.Apps.Retrieve()
            .FirstOrDefaultAsync(a => a.Name == appKey.Name.Value && a.Type == appKey.Type.Value);

    public async Task<App> AppOrUnknown(AppKey appKey)
    {
        var record = await GetAppByKey(appKey);
        if (record == null && !appKey.Equals(AppKey.Unknown))
        {
            record = await GetAppByKey(AppKey.Unknown);
        }
        return factory.CreateApp
        (
            record ?? throw new ArgumentNullException($"App '{appKey.Name.DisplayText} {appKey.Type.DisplayText}' not found")
        );
    }

    public Task<App[]> WebAppsWithOpenSessions(AppUser user)
    {
        var sessionIDs = factory.DB
            .Sessions
            .Retrieve()
            .Where(s => s.UserID == user.ID && s.TimeEnded > DateTimeOffset.Now.AddDays(1))
            .Select(s => s.ID);
        var appIDs = factory.DB
            .Requests
            .Retrieve()
            .Where(r => sessionIDs.Any(id => id == r.SessionID))
            .Join
            (
                factory.DB.Resources.Retrieve(),
                req => req.ResourceID,
                res => res.ID,
                (req, res) => new
                {
                    GroupID = res.GroupID
                }
            )
            .Join
            (
                factory.DB.ResourceGroups.Retrieve(),
                res => res.GroupID,
                grp => grp.ID,
                (res, grp) => new
                {
                    AppVersionID = grp.AppVersionID
                }
            )
            .Join
            (
                factory.DB.AppVersions.Retrieve(),
                grp => grp.AppVersionID,
                av => av.ID,
                (grp, v) => v.AppID
            )
            .Distinct();
        return factory.DB
            .Apps
            .Retrieve()
            .Where(a => a.Type == AppType.Values.WebApp && appIDs.Any(id => id == a.ID))
            .Select(a => factory.CreateApp(a))
            .ToArrayAsync();
    }
}