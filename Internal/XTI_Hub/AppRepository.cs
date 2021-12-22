using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppRepository
{
    private readonly AppFactory factory;

    internal AppRepository(AppFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppVersion> StartNewVersion(AppKey appKey, AppVersionType versionType, DateTimeOffset timeAdded)
    {
        var app = await AddOrUpdate
        (
            appKey,
            timeAdded
        );
        var version = await app.StartNewVersion(versionType, timeAdded);
        return version;
    }

    public async Task<App> AddOrUpdate(AppKey appKey, DateTimeOffset timeAdded)
    {
        App app;
        var title = appKey.Name.DisplayText;
        var record = await factory.DB.Apps.Retrieve()
            .FirstOrDefaultAsync(a => a.Name == appKey.Name.Value && a.Type == appKey.Type.Value);
        if (record == null)
        {
            app = await Add(appKey, title, timeAdded);
        }
        else
        {
            await factory.DB.Apps.Update(record, r =>
            {
                r.Title = title.Trim();
            });
            app = factory.App(record);
        }
        return app;
    }

    private async Task<App> Add(AppKey appKey, string title, DateTimeOffset timeAdded)
    {
        App? app = null;
        await factory.Transaction(async () =>
        {
            app = await AddApp(appKey, title, timeAdded);
            await factory.Versions.AddCurrentVersion(app, timeAdded);
            var defaultModCategory = await app.AddModCategoryIfNotFound(ModifierCategoryName.Default);
            await factory.Modifiers.AddOrUpdateByModKey(defaultModCategory, ModifierKey.Default, "", "");
        });
        return app ?? throw new ArgumentNullException(nameof(app));
    }

    private async Task<App> AddApp(AppKey appKey, string title, DateTimeOffset timeAdded)
    {
        var record = new AppEntity
        {
            Name = appKey.Name.Value,
            Type = appKey.Type.Value,
            Title = title?.Trim() ?? "",
            TimeAdded = timeAdded
        };
        await factory.DB.Apps.Create(record);
        return factory.App(record);
    }

    public async Task<IEnumerable<App>> All()
    {
        var records = await factory.DB.Apps.Retrieve().ToArrayAsync();
        return records.Select(r => factory.App(r));
    }

    public async Task<App> App(int id)
    {
        var record = await factory.DB.Apps.Retrieve().FirstOrDefaultAsync(a => a.ID == id);
        return factory.App(record ?? throw new Exception($"App {id} not found"));
    }

    public async Task<App> App(AppKey appKey)
    {
        var record = await GetAppByKey(appKey);
        return factory.App
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
        return factory.App
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
                    VersionID = grp.VersionID
                }
            )
            .Join
            (
                factory.DB.Versions.Retrieve(),
                grp => grp.VersionID,
                v => v.ID,
                (grp, v) => v.AppID
            )
            .Distinct();
        return factory.DB
            .Apps
            .Retrieve()
            .Where(a => a.Type == AppType.Values.WebApp && appIDs.Any(id => id == a.ID))
            .Select(a => factory.App(a))
            .ToArrayAsync();
    }
}