using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppRepository
{
    private readonly AppFactory factory;

    internal AppRepository(AppFactory factory)
    {
        this.factory = factory;
    }

    internal async Task AddUnknownIfNotFound()
    {
        var app = await AddOrUpdate(AppVersionName.Unknown, AppKey.Unknown, "", DateTimeOffset.Now);
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
        var defaultModCategory = await app.ModCategory(ModifierCategoryName.Default);
        var group = await currentVersion.AddOrUpdateResourceGroup(ResourceGroupName.Unknown, defaultModCategory);
        await group.AddOrUpdateResource(ResourceName.Unknown, ResourceResultType.Values.None);
    }

    public async Task<App> AddOrUpdate(AppVersionName versionName, AppKey appKey, string domain, DateTimeOffset timeAdded)
    {
        App app;
        var title = appKey.Name.DisplayText;
        var record = await GetAppByKey(appKey);
        if (record == null)
        {
            app = await Add(versionName, appKey, title, domain, timeAdded);
        }
        else
        {
            await factory.DB.Apps.Update(record, r =>
            {
                r.VersionName = versionName.Value;
                r.Title = title.Trim();
                r.Domain = domain;
            });
            app = factory.CreateApp(record);
        }
        return app;
    }

    private async Task<App> Add(AppVersionName versionName, AppKey appKey, string title, string domain, DateTimeOffset timeAdded)
    {
        App? app = null;
        await factory.Transaction(async () =>
        {
            var entity = await AddApp(versionName, appKey, title, domain, timeAdded);
            app = factory.CreateApp(entity);
            var defaultModCategory = await app.AddModCategoryIfNotFound(ModifierCategoryName.Default);
            await factory.Modifiers.AddOrUpdateByModKey(defaultModCategory, ModifierKey.Default, "", "");
        });
        return app ?? throw new ArgumentNullException(nameof(app));
    }

    private async Task<AppEntity> AddApp(AppVersionName versionName, AppKey appKey, string title, string domain, DateTimeOffset timeAdded)
    {
        var record = new AppEntity
        {   
            Name = appKey.Name.Value,
            Type = appKey.Type.Value,
            Title = title.Trim(),
            VersionName = versionName.Value,
            Domain = domain,
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
        if (record == null)
        {
            record = await GetAppByKey(AppKey.Unknown);
        }
        return factory.CreateApp
        (
            record ?? throw new ArgumentNullException($"App '{appKey.Name.DisplayText} {appKey.Type.DisplayText}' not found")
        );
    }

    public Task<App[]> WebAppsWithOpenSessions(IAppUser user)
    {
        var sessionIDs = factory.DB
            .Sessions
            .Retrieve()
            .Where(s => s.UserID == user.ID.Value && s.TimeEnded > DateTimeOffset.Now.AddDays(1))
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