﻿using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class XtiVersionRepository
{
    private readonly AppFactory factory;

    internal XtiVersionRepository(AppFactory factory)
    {
        this.factory = factory;
    }

    internal IQueryable<int> QueryAppVersionID(App app, XtiVersion version) =>
        factory.DB
            .AppVersions.Retrieve()
            .Where(av => av.AppID == app.ID.Value && av.VersionID == version.ID.Value)
            .Select(av => av.ID);

    internal async Task<XtiVersion> AddIfNotFound
    (
        AppVersionName versionName,
        AppVersionKey key,
        DateTimeOffset timeAdded,
        AppVersionStatus status,
        AppVersionType type,
        AppVersionNumber versionNumber
    )
    {
        var entity = await GetVersionByName(versionName, key);
        if (entity == null)
        {
            entity = await AddVersion
            (
                versionName,
                key,
                timeAdded,
                type,
                status,
                versionNumber
            );
        }
        return factory.CreateVersion(entity);
    }

    internal async Task AddVersionToAppIfNotFound(App app, XtiVersion version)
    {
        var appVersion = await GetAppVersion(app, version);
        if (appVersion == null)
        {
            await factory.DB.AppVersions.Create
            (
                new AppXtiVersionEntity
                {
                    VersionID = version.ID.Value,
                    AppID = app.ID.Value,
                    Domain = app.Domain
                }
            );
        }
    }

    private Task<AppXtiVersionEntity?> GetAppVersion(App app, XtiVersion version) =>
        factory.DB
            .AppVersions.Retrieve()
            .FirstOrDefaultAsync(av => av.VersionID == version.ID.Value && av.AppID == app.ID.Value);

    internal async Task RemoveVersionFromApp(App app, XtiVersion version)
    {
        var appVersion = await GetAppVersion(app, version);
        if (appVersion != null)
        {
            await factory.DB.AppVersions.Delete(appVersion);
        }
    }

    private async Task<AppVersionKey> NextKey(AppVersionName versionName)
    {
        var keys = await factory.DB
            .Versions.Retrieve()
            .Where(v => v.VersionName == versionName.Value && v.VersionKey.StartsWith("V"))
            .Select(v => v.VersionKey.Substring(1))
            .ToArrayAsync();
        var keyValues = keys.Select(k => int.Parse(k));
        var maxKey = keyValues.Any() ? keyValues.Max() : 0;
        return new AppVersionKey(maxKey + 1);
    }

    public async Task<XtiVersion> StartNewVersion(AppVersionName versionName, DateTimeOffset timeAdded, AppVersionType type, AppKey[] appKeys)
    {
        XtiVersion? version = null;
        await factory.DB.Transaction(async () =>
        {
            await AddCurrentVersionIfNotFound(versionName, timeAdded, appKeys);
            var validVersionTypes = new List<AppVersionType>(new[] { AppVersionType.Values.Major, AppVersionType.Values.Minor, AppVersionType.Values.Patch });
            if (!validVersionTypes.Contains(type))
            {
                throw new ArgumentException($"Version type {type} is not valid");
            }
            var versionNumber = new AppVersionNumber(0, 0, 0);
            var entity = await AddVersion(versionName, AppVersionKey.None, timeAdded, type, AppVersionStatus.Values.New, versionNumber);
            version = factory.CreateVersion(entity);
            foreach (var appKey in appKeys)
            {
                var app = await factory.Apps.App(appKey);
                await app.AddVersionIfNotFound(version);
            }
        });
        return version ?? throw new ArgumentNullException(nameof(version));
    }

    private async Task<XtiVersion> AddCurrentVersionIfNotFound(AppVersionName versionName, DateTimeOffset timeAdded, AppKey[] appKeys)
    {
        XtiVersion currentVersion;
        var entity = await GetVersionByName(versionName, AppVersionKey.Current);
        if (entity == null)
        {
            entity = await AddVersion
            (
                versionName,
                AppVersionKey.Current,
                timeAdded,
                AppVersionType.Values.Major,
                AppVersionStatus.Values.Current,
                new AppVersionNumber(1, 0, 0)
            );
            currentVersion = factory.CreateVersion(entity);
        }
        else
        {
            currentVersion = factory.CreateVersion(entity);
        }
        foreach (var appKey in appKeys)
        {
            var app = await factory.Apps.App(appKey);
            await app.AddVersionIfNotFound(currentVersion);
        }
        return currentVersion;
    }

    private async Task<XtiVersionEntity> AddVersion(AppVersionName versionName, AppVersionKey key, DateTimeOffset timeAdded, AppVersionType type, AppVersionStatus status, AppVersionNumber versionNumber)
    {
        XtiVersionEntity? record = null;
        if (key.Equals(AppVersionKey.None) || key.Equals(AppVersionKey.Current))
        {
            key = await NextKey(versionName);
        }
        await factory.DB.Transaction(async () =>
        {
            record = new XtiVersionEntity
            {
                VersionName = versionName.Value,
                VersionKey = key.Value,
                Major = versionNumber.Major,
                Minor = versionNumber.Minor,
                Patch = versionNumber.Patch,
                TimeAdded = timeAdded,
                Description = "",
                Status = status.Value,
                Type = type.Value
            };
            await factory.DB.Versions.Create(record);
        });
        return record ?? throw new ArgumentNullException(nameof(record));
    }

    public async Task<XtiVersion> Version(int id)
    {
        var record = await factory.DB
            .Versions
            .Retrieve()
            .FirstAsync(v => v.ID == id);
        return factory.CreateVersion(record);
    }

    public async Task<XtiVersion> VersionByName(AppVersionName versionName, AppVersionKey versionKey)
    {
        var record = await GetVersionByName(versionName, versionKey);
        return factory.CreateVersion(record ?? throw new Exception($"Version '{versionName.DisplayText} {versionKey.DisplayText}' was not found"));
    }

    public Task<XtiVersion[]> VersionsByName(AppVersionName versionName) =>
        factory.DB
            .Versions
            .Retrieve()
            .Where
            (
                v => v.VersionName == versionName
            )
            .Select(v => factory.CreateVersion(v))
            .ToArrayAsync();

    internal async Task<AppVersion> VersionByApp(App app, AppVersionKey versionKey)
    {
        var record = await GetVersionByApp(app, versionKey);
        var version = factory.CreateVersion(record ?? throw new Exception($"Version '{versionKey.DisplayText}' was not found"));
        return version.App(app);
    }

    internal async Task<AppVersion> VersionByAppOrUnknown(App app, AppVersionKey versionKey)
    {
        var record = await GetVersionByApp(app, versionKey);
        if (record == null)
        {
            var unknownApp = await factory.Apps.App(AppKey.Unknown);
            record = await GetVersionByApp(unknownApp, AppVersionKey.Current);
            if (record == null)
            {
                var unknownVersion = await AddCurrentVersionIfNotFound
                (
                    AppVersionName.Unknown, DateTimeOffset.Now, new[] { AppKey.Unknown }
                );
                return unknownVersion.App(unknownApp);
            }
        }
        var version = factory.CreateVersion
        (
            record ?? throw new Exception($"Version '{versionKey.DisplayText}' was not found")
        );
        return version.App(app);
    }

    private async Task<XtiVersionEntity?> GetVersionByApp(App app, AppVersionKey versionKey)
    {
        XtiVersionEntity? record;
        var versionIDs = QueryVersionIDs(app);
        if (versionKey.Equals(AppVersionKey.Current))
        {
            record = await factory.DB
                .Versions
                .Retrieve()
                .Where
                (
                    v =>
                        versionIDs.Contains(v.ID) &&
                        v.Status == AppVersionStatus.Values.Current.Value
                )
                .FirstOrDefaultAsync();
        }
        else
        {
            record = await factory.DB
                .Versions
                .Retrieve()
                .Where(v => versionIDs.Contains(v.ID) && v.VersionKey == versionKey.Value)
                .FirstOrDefaultAsync();
        }
        return record;
    }

    private async Task<XtiVersionEntity?> GetVersionByName(AppVersionName versionName, AppVersionKey versionKey)
    {
        XtiVersionEntity? record;
        if (versionKey.Equals(AppVersionKey.Current))
        {
            record = await factory.DB
                .Versions
                .Retrieve()
                .Where
                (
                    v =>
                        v.VersionName == versionName &&
                        v.Status == AppVersionStatus.Values.Current.Value
                )
                .FirstOrDefaultAsync();
        }
        else
        {
            record = await factory.DB
                .Versions
                .Retrieve()
                .Where(v => v.VersionName == versionName && v.VersionKey == versionKey.Value)
                .FirstOrDefaultAsync();
        }
        return record;
    }

    internal Task<XtiVersion[]> VersionsByApp(App app)
    {
        var versionIDs = QueryVersionIDs(app);
        return factory.DB
            .Versions
            .Retrieve()
            .Where(v => versionIDs.Contains(v.ID))
            .Select(v => factory.CreateVersion(v))
            .ToArrayAsync();
    }

    private IQueryable<int> QueryVersionIDs(App app)
    {
        var appModel = app.ToAppModel();
        var versionIDs = factory.DB.Versions.Retrieve()
            .Where(v => v.VersionName == appModel.VersionName.Value)
            .Select(v => v.ID);
        return factory.DB
            .AppVersions.Retrieve()
            .Where(av => av.AppID == app.ID.Value && versionIDs.Contains(av.VersionID))
            .Select(av => av.VersionID);
    }

    internal async Task Publishing(XtiVersionEntity version)
    {
        if (!AppVersionStatus.Values.New.Equals(version.Status) && !AppVersionStatus.Values.Publishing.Equals(version.Status))
        {
            throw PublishException.Publishing(AppVersionKey.Parse(version.VersionKey), AppVersionStatus.Values.Value(version.Status));
        }
        var lastVersion = await factory.DB
            .Versions.Retrieve()
            .Where(v => v.VersionName == version.VersionName)
            .OrderByDescending(v => v.Major)
            .ThenByDescending(v => v.Minor)
            .ThenByDescending(v => v.Patch)
            .FirstOrDefaultAsync();
        var nextVersion = new AppVersionNumber
        (
            lastVersion?.Major ?? 0,
            lastVersion?.Minor ?? 0,
            lastVersion?.Patch ?? 0
        )
        .Next(AppVersionType.Values.Value(version.Type));
        await factory.DB.Versions.Update(version, r =>
        {
            r.Status = AppVersionStatus.Values.Publishing.Value;
            r.Major = nextVersion.Major;
            r.Minor = nextVersion.Minor;
            r.Patch = nextVersion.Patch;
        });
    }

    internal async Task Published(XtiVersionEntity version)
    {
        if (!AppVersionStatus.Values.Publishing.Equals(version.Status))
        {
            throw PublishException.Published(AppVersionKey.Parse(version.VersionKey), AppVersionStatus.Values.Value(version.Status));
        }
        await factory.DB.Transaction
        (
            async () =>
            {
                var previousVersions = await factory.DB
                    .Versions.Retrieve()
                    .Where(v => v.ID != version.ID && v.VersionName == version.VersionName && v.Status != AppVersionStatus.Values.Old)
                    .ToArrayAsync();
                foreach (var previousVersion in previousVersions)
                {
                    await factory.DB.Versions.Update(previousVersion, v => v.Status = AppVersionStatus.Values.Old);
                }
                await factory.DB.Versions.Update
                (
                    version,
                    r => r.Status = AppVersionStatus.Values.Current.Value
                );
            }
        );
    }

}