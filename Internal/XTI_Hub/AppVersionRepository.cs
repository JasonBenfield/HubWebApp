using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppVersionRepository
{
    private readonly AppFactory factory;

    internal AppVersionRepository(AppFactory factory)
    {
        this.factory = factory;
    }

    internal async Task<AppVersion> AddIfNotFound(AppVersionKey key, AppEntity app, DateTimeOffset timeAdded, AppVersionStatus status, AppVersionType type, Version version)
    {
        var record = await factory.DB
            .Versions
            .Retrieve()
            .FirstOrDefaultAsync(v => v.AppID == app.ID && v.VersionKey == key.Value);
        if (record == null)
        {
            record = new AppVersionEntity
            {
                VersionKey = key.Value,
                AppID = app.ID,
                Major = version.Major,
                Minor = version.Minor,
                Patch = version.Build,
                TimeAdded = timeAdded,
                Description = "",
                Status = status.Value,
                Type = type.Value
            };
            await factory.DB.Versions.Create(record);
        }
        return factory.CreateVersion(record);
    }

    internal async Task<AppVersion> StartNewVersion(AppVersionKey key, AppEntity app, DateTimeOffset timeAdded, AppVersionType type)
    {
        var validVersionTypes = new List<AppVersionType>(new[] { AppVersionType.Values.Major, AppVersionType.Values.Minor, AppVersionType.Values.Patch });
        if (!validVersionTypes.Contains(type))
        {
            throw new ArgumentException($"Version type {type} is not valid");
        }
        var versionNumber = new Version(0, 0, 0);
        return await AddVersion(key, app, timeAdded, type, AppVersionStatus.Values.New, versionNumber);
    }

    private async Task<AppVersion> AddVersion(AppVersionKey key, AppEntity app, DateTimeOffset timeAdded, AppVersionType type, AppVersionStatus status, Version versionNumber)
    {
        AppVersionEntity? record = null;
        await factory.DB.Transaction(async () =>
        {
            record = new AppVersionEntity
            {
                VersionKey = new GeneratedKey().Value(),
                AppID = app.ID,
                Major = versionNumber.Major,
                Minor = versionNumber.Minor,
                Patch = versionNumber.Build,
                TimeAdded = timeAdded,
                Description = "",
                Status = status.Value,
                Type = type.Value,
                Domain = app.Domain
            };
            await factory.DB.Versions.Create(record);
            if (key.Equals(AppVersionKey.None))
            {
                await factory.DB.Versions.Update
                (
                    record,
                    r => r.VersionKey = new AppVersionKey(r.ID).Value
                );
            }
        });
        return factory.CreateVersion(record ?? throw new ArgumentNullException(nameof(record)));
    }

    public async Task<AppVersion> Version(int id)
    {
        var record = await factory.DB
            .Versions
            .Retrieve()
            .FirstAsync(v => v.ID == id);
        return factory.CreateVersion(record);
    }

    internal async Task<AppVersion> VersionByApp(App app, AppVersionKey versionKey)
    {
        var record = await GetVersionByApp(app, versionKey);
        return factory.CreateVersion(record ?? throw new Exception($"Version '{versionKey.DisplayText}' was not found"));
    }

    internal async Task<AppVersion> VersionByAppOrDefault(App app, AppVersionKey versionKey)
    {
        var record = await GetVersionByApp(app, versionKey);
        if(record == null)
        {
            var unknownApp = await factory.Apps.App(AppKey.Unknown);
            record = await GetVersionByApp(unknownApp, AppVersionKey.Current);
        }
        return factory.CreateVersion
        (
            record ?? throw new Exception($"Version '{versionKey.DisplayText}' was not found")
        );
    }

    private async Task<AppVersionEntity?> GetVersionByApp(App app, AppVersionKey versionKey)
    {
        AppVersionEntity? record;
        if (versionKey.Equals(AppVersionKey.Current))
        {
            record = await factory.DB
                .Versions
                .Retrieve()
                .Where
                (
                    v =>
                        v.AppID == app.ID.Value &&
                        v.Status == AppVersionStatus.Values.Current.Value
                )
                .FirstOrDefaultAsync();
        }
        else
        {
            record = await factory.DB
                .Versions
                .Retrieve()
                .Where(v => v.AppID == app.ID.Value && v.VersionKey == versionKey.Value)
                .FirstOrDefaultAsync();
        }

        return record;
    }

    internal Task<AppVersion[]> VersionsByApp(App app)
        => factory.DB
            .Versions
            .Retrieve()
            .Where(v => v.AppID == app.ID.Value)
            .Select(v => factory.CreateVersion(v))
            .ToArrayAsync();

    internal Task<AppVersion> AddCurrentVersion(AppEntity app, DateTimeOffset timeAdded) =>
        AddIfNotFound
        (
            AppVersionKey.None,
            app,
            timeAdded,
            AppVersionStatus.Values.Current,
            AppVersionType.Values.Major,
            new Version(1, 0, 0)
        );
}