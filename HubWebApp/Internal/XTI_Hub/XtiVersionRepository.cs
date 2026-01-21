using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class XtiVersionRepository
{
    private readonly HubFactory factory;

    internal XtiVersionRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    internal IQueryable<int> QueryAppVersionID(App app, XtiVersion version) =>
        factory.DB
            .AppVersions.Retrieve()
            .Where(av => av.AppID == app.ID && av.VersionID == version.ID)
            .Select(av => av.ID);

    internal async Task<XtiVersion> AddIfNotFound
    (
        AppVersionName versionName,
        AppVersionKey key,
        DateTimeOffset timeAdded,
        AppVersionStatus status,
        AppVersionType type,
        AppVersionNumber versionNumber,
        CancellationToken ct
    )
    {
        var entity = await GetVersionByName(versionName, key, ct);
        if (entity == null)
        {
            if (status.Equals(AppVersionStatus.Values.Current))
            {
                var previousVersions = await factory.DB
                    .Versions.Retrieve()
                    .Where(v => v.VersionName == versionName.Value && v.Status == AppVersionStatus.Values.Current.Value)
                    .ToArrayAsync(ct);
                foreach (var previousVersion in previousVersions)
                {
                    await factory.DB.Versions.Update
                    (
                        previousVersion,
                        v => v.Status = AppVersionStatus.Values.Old,
                        ct
                    );
                }
            }
            entity = await AddVersion
            (
                versionName,
                key,
                timeAdded,
                type,
                status,
                versionNumber,
                ct
            );
        }
        return factory.CreateVersion(entity);
    }

    internal async Task AddVersionToAppIfNotFound(App app, XtiVersion version, CancellationToken ct)
    {
        var appVersion = await GetAppVersion(app, version, ct);
        if (appVersion == null)
        {
            await factory.DB.AppVersions.Create
            (
                new AppXtiVersionEntity
                {
                    VersionID = version.ID,
                    AppID = app.ID
                },
                ct
            );
        }
    }

    private Task<AppXtiVersionEntity?> GetAppVersion(App app, XtiVersion version, CancellationToken ct) =>
        factory.DB
            .AppVersions.Retrieve()
            .FirstOrDefaultAsync(av => av.VersionID == version.ID && av.AppID == app.ID, ct);

    private async Task<AppVersionKey> NextKey(AppVersionName versionName, CancellationToken ct)
    {
        var keys = await factory.DB
            .Versions.Retrieve()
            .Where(v => v.VersionName == versionName.Value && v.VersionKey.StartsWith("V"))
            .Select(v => v.VersionKey.Substring(1))
            .ToArrayAsync(ct);
        var keyValues = keys.Select(k => int.Parse(k));
        var maxKey = keyValues.Any() ? keyValues.Max() : 0;
        return new AppVersionKey(maxKey + 1);
    }

    public async Task<XtiVersion> StartNewVersion(AppVersionName versionName, DateTimeOffset timeAdded, AppVersionType type, CancellationToken ct)
    {
        var version = await factory.DB.Transaction
        (
            () => _StartNewVersion(versionName, timeAdded, type, ct)
        );
        return version ?? throw new ArgumentNullException(nameof(version));
    }

    private async Task<XtiVersion> _StartNewVersion(AppVersionName versionName, DateTimeOffset timeAdded, AppVersionType type, CancellationToken ct)
    {
        var validVersionTypes = new List<AppVersionType>
        (
            [
                AppVersionType.Values.Major,
                AppVersionType.Values.Minor,
                AppVersionType.Values.Patch
            ]
        );
        if (!validVersionTypes.Contains(type))
        {
            throw new ArgumentException($"Version type {type} is not valid");
        }
        await AddCurrentVersionIfNotFound(versionName, timeAdded, ct);
        var versionNumber = new AppVersionNumber(0, 0, 0);
        var key = await NextKey(versionName, ct);
        var entity = await AddVersion(versionName, key, timeAdded, type, AppVersionStatus.Values.New, versionNumber, ct);
        return factory.CreateVersion(entity);
    }

    private async Task<XtiVersion> AddCurrentVersionToAppsIfNotFound(AppVersionName versionName, DateTimeOffset timeAdded, AppKey[] appKeys, CancellationToken ct)
    {
        var currentVersion = await AddCurrentVersionIfNotFound(versionName, timeAdded, ct);
        foreach (var appKey in appKeys)
        {
            var app = await factory.Apps.App(appKey, ct);
            await app.AddVersionIfNotFound(currentVersion, ct);
        }
        return currentVersion;
    }

    internal async Task<XtiVersion> AddCurrentVersionIfNotFound(AppVersionName versionName, DateTimeOffset timeAdded, CancellationToken ct)
    {
        XtiVersion currentVersion;
        var entity = await GetVersionByName(versionName, AppVersionKey.Current, ct);
        if (entity == null)
        {
            var key = await NextKey(versionName, ct);
            entity = await AddVersion
            (
                versionName,
                key,
                timeAdded,
                AppVersionType.Values.Major,
                AppVersionStatus.Values.Current,
                new AppVersionNumber(1, 0, 0),
                ct
            );
            currentVersion = factory.CreateVersion(entity);
        }
        else
        {
            currentVersion = factory.CreateVersion(entity);
        }
        return currentVersion;
    }

    private async Task<XtiVersionEntity> AddVersion(AppVersionName versionName, AppVersionKey key, DateTimeOffset timeAdded, AppVersionType type, AppVersionStatus status, AppVersionNumber versionNumber, CancellationToken ct)
    {
        if (key.IsNone() || key.IsCurrent())
        {
            throw new ArgumentException($"Unable to add version with key '{key.DisplayText}'");
        }
        var record = await factory.DB.Transaction
        (
            async () =>
            {
                var versionEntity = new XtiVersionEntity
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
                await factory.DB.Versions.Create(versionEntity, ct);
                return versionEntity;
            }
        );
        return record;
    }

    public async Task<XtiVersion> Version(int id, CancellationToken ct)
    {
        var record = await factory.DB
            .Versions
            .Retrieve()
            .FirstAsync(v => v.ID == id, ct);
        return factory.CreateVersion(record);
    }

    public async Task<XtiVersion> VersionByName(AppVersionName versionName, AppVersionKey versionKey, CancellationToken ct)
    {
        var record = await GetVersionByName(versionName, versionKey, ct);
        return factory.CreateVersion(record ?? throw new Exception($"Version '{versionName.DisplayText} {versionKey.DisplayText}' was not found"));
    }

    public Task<XtiVersion[]> VersionsByName(AppVersionName versionName, CancellationToken ct) =>
        factory.DB
            .Versions
            .Retrieve()
            .Where
            (
                v => v.VersionName == versionName.Value
            )
            .Select(v => factory.CreateVersion(v))
            .ToArrayAsync(ct);

    internal async Task<AppVersion> VersionByApp(App app, AppVersionKey versionKey, CancellationToken ct)
    {
        var record = await GetVersionByApp(app, versionKey, ct);
        var version = factory.CreateVersion(record ?? throw new Exception($"Version '{app.ToModel().AppKey.Format()} {versionKey.DisplayText}' was not found"));
        return version.App(app);
    }

    internal async Task<AppVersion> VersionByAppOrUnknown(App app, AppVersionKey versionKey, CancellationToken ct)
    {
        var record = await GetVersionByApp(app, versionKey, ct);
        if (record == null)
        {
            var unknownApp = await factory.Apps.App(AppKey.Unknown, ct);
            record = await GetVersionByApp(unknownApp, AppVersionKey.Current, ct);
            if (record == null)
            {
                var unknownVersion = await AddCurrentVersionToAppsIfNotFound
                (
                    AppVersionName.Unknown,
                    DateTimeOffset.Now,
                    [AppKey.Unknown],
                    ct
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

    private async Task<XtiVersionEntity?> GetVersionByApp(App app, AppVersionKey versionKey, CancellationToken ct)
    {
        XtiVersionEntity? record;
        var versionName = app.ToModel().VersionName.Value;
        var versionIDs = factory.DB
            .AppVersions.Retrieve()
            .Where(av => av.AppID == app.ID)
            .Select(av => av.VersionID);
        if (versionKey.IsCurrent())
        {
            record = await factory.DB
                .Versions
                .Retrieve()
                .Where
                (
                    v =>
                        versionIDs.Contains(v.ID) &&
                        v.VersionName == versionName &&
                        v.Status == AppVersionStatus.Values.Current.Value
                )
                .FirstOrDefaultAsync(ct);
        }
        else if (versionKey.IsBlank())
        {
            record = null;
        }
        else
        {
            record = await factory.DB
                .Versions
                .Retrieve()
                .Where
                (
                    v =>
                        versionIDs.Contains(v.ID) &&
                        v.VersionName == versionName &&
                        v.VersionKey == versionKey.Value
                )
                .FirstOrDefaultAsync(ct);
        }
        return record;
    }

    private async Task<XtiVersionEntity?> GetVersionByName(AppVersionName versionName, AppVersionKey versionKey, CancellationToken ct)
    {
        XtiVersionEntity? record;
        if (versionKey.IsCurrent())
        {
            record = await factory.DB
                .Versions
                .Retrieve()
                .Where
                (
                    v =>
                        v.VersionName == versionName.Value &&
                        v.Status == AppVersionStatus.Values.Current.Value
                )
                .FirstOrDefaultAsync(ct);
        }
        else if (versionKey.IsBlank())
        {
            record = null;
        }
        else
        {
            record = await factory.DB
                .Versions
                .Retrieve()
                .Where(v => v.VersionName == versionName.Value && v.VersionKey == versionKey.Value)
                .FirstOrDefaultAsync(ct);
        }
        return record;
    }

    internal Task<XtiVersion[]> VersionsByApp(App app, CancellationToken ct)
    {
        var versionIDs = factory.DB
            .AppVersions.Retrieve()
            .Where(av => av.AppID == app.ID)
            .Select(av => av.VersionID);
        return factory.DB
            .Versions
            .Retrieve()
            .Where(v => versionIDs.Contains(v.ID))
            .Select(v => factory.CreateVersion(v))
            .ToArrayAsync(ct);
    }

    internal async Task Publishing(XtiVersionEntity version, CancellationToken ct)
    {
        if (!AppVersionStatus.Values.New.Equals(version.Status) && !AppVersionStatus.Values.Publishing.Equals(version.Status))
        {
            throw PublishException.Publishing(AppVersionKey.Parse(version.VersionKey), AppVersionStatus.Values.Value(version.Status));
        }
        var lastVersion = await factory.DB
            .Versions.Retrieve()
            .Where(v => v.VersionName == version.VersionName && v.ID != version.ID)
            .OrderByDescending(v => v.Major)
            .ThenByDescending(v => v.Minor)
            .ThenByDescending(v => v.Patch)
            .Select(v => new AppVersionNumber(v.Major, v.Minor, v.Patch))
            .FirstOrDefaultAsync(ct)
            ?? new AppVersionNumber(0, 0, 0);
        var nextVersion = lastVersion.Next(AppVersionType.Values.Value(version.Type));
        await factory.DB.Versions.Update
        (
            version,
            r =>
            {
                r.Status = AppVersionStatus.Values.Publishing.Value;
                r.Major = nextVersion.Major;
                r.Minor = nextVersion.Minor;
                r.Patch = nextVersion.Patch;
            },
            ct
        );
    }

    internal async Task Published(XtiVersionEntity version, CancellationToken ct)
    {
        if (!AppVersionStatus.Values.Publishing.Equals(version.Status))
        {
            throw PublishException.Published(AppVersionKey.Parse(version.VersionKey), AppVersionStatus.Values.Value(version.Status));
        }
        await factory.DB.Transaction
        (
            async () =>
            {
                await ArchivePreviousVersions(version, ct);
                await factory.DB.Versions.Update
                (
                    version,
                    r => r.Status = AppVersionStatus.Values.Current.Value,
                    ct
                );
            }
        );
    }

    private async Task ArchivePreviousVersions(XtiVersionEntity version, CancellationToken ct)
    {
        var versionsToAchive = await factory.DB
            .Versions.Retrieve()
            .Where
            (
                v =>
                    v.ID != version.ID &&
                    v.VersionName == version.VersionName &&
                    v.Status == AppVersionStatus.Values.Current.Value
            )
            .ToArrayAsync(ct);
        foreach (var versionToArchive in versionsToAchive)
        {
            await factory.DB.Versions.Update
            (
                versionToArchive,
                v => v.Status = AppVersionStatus.Values.Old.Value,
                ct
            );
        }
    }
}