using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfVersions
{
    private readonly EfHubDB db;

    internal EfVersions(EfHubDB db)
    {
        this.db = db;
    }

    internal IQueryable<int> QueryAppVersionID(EfApp app, EfVersion version) =>
        db.Context.AppVersions.Retrieve()
            .Where(av => av.AppID == app.ID && av.VersionID == version.ID)
            .Select(av => av.ID);

    public async Task<EfVersion> AddIfNotFound
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
        var version = await GetVersionByName(versionName, key, ct);
        if (version == null)
        {
            if (status.Equals(AppVersionStatus.Values.Current))
            {
                var previousVersions = await db.Context.Versions.Retrieve()
                    .Where(v => v.VersionName == versionName.Value && v.Status == AppVersionStatus.Values.Current.Value)
                    .ToArrayAsync(ct);
                foreach (var previousVersion in previousVersions)
                {
                    await db.Context.Versions.Update
                    (
                        previousVersion,
                        v => v.Status = AppVersionStatus.Values.Old,
                        ct
                    );
                }
            }
            version = await AddVersion
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
        return new EfVersion(db, version);
    }

    internal async Task AddVersionToAppIfNotFound(EfApp app, EfVersion version, CancellationToken ct)
    {
        var appVersion = await GetAppVersion(app, version, ct);
        if (appVersion == null)
        {
            await db.Context.AppVersions.Create
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

    private Task<AppXtiVersionEntity?> GetAppVersion(EfApp app, EfVersion version, CancellationToken ct) =>
        db.Context.AppVersions.Retrieve()
            .Where(av => av.VersionID == version.ID && av.AppID == app.ID)
            .FirstOrDefaultAsync(ct);

    private async Task<AppVersionKey> NextKey(AppVersionName versionName, CancellationToken ct)
    {
        var keys = await db.Context
            .Versions.Retrieve()
            .Where(v => v.VersionName == versionName.Value && v.VersionKey.StartsWith("V"))
            .Select(v => v.VersionKey.Substring(1))
            .ToArrayAsync(ct);
        var keyValues = keys.Select(k => int.Parse(k));
        var maxKey = keyValues.Any() ? keyValues.Max() : 0;
        return new AppVersionKey(maxKey + 1);
    }

    public async Task<EfVersion> StartNewVersion(AppVersionName versionName, DateTimeOffset timeAdded, AppVersionType type, CancellationToken ct)
    {
        var version = await db.Context.Transaction
        (
            () => _StartNewVersion(versionName, timeAdded, type, ct)
        );
        return version ?? throw new ArgumentNullException(nameof(version));
    }

    private async Task<EfVersion> _StartNewVersion(AppVersionName versionName, DateTimeOffset timeAdded, AppVersionType type, CancellationToken ct)
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
        var version = await AddVersion(versionName, key, timeAdded, type, AppVersionStatus.Values.New, versionNumber, ct);
        return new EfVersion(db, version);
    }

    private async Task<EfVersion> AddCurrentVersionToAppsIfNotFound(AppVersionName versionName, DateTimeOffset timeAdded, AppKey[] appKeys, CancellationToken ct)
    {
        var currentVersion = await AddCurrentVersionIfNotFound(versionName, timeAdded, ct);
        foreach (var appKey in appKeys)
        {
            var app = await db.Apps.App(appKey, ct);
            await app.AddVersionIfNotFound(currentVersion, ct);
        }
        return currentVersion;
    }

    internal async Task<EfVersion> AddCurrentVersionIfNotFound(AppVersionName versionName, DateTimeOffset timeAdded, CancellationToken ct)
    {
        EfVersion efCurrentVersion;
        var version = await GetVersionByName(versionName, AppVersionKey.Current, ct);
        if (version == null)
        {
            var key = await NextKey(versionName, ct);
            version = await AddVersion
            (
                versionName,
                key,
                timeAdded,
                AppVersionType.Values.Major,
                AppVersionStatus.Values.Current,
                new AppVersionNumber(1, 0, 0),
                ct
            );
            efCurrentVersion = new EfVersion(db, version);
        }
        else
        {
            efCurrentVersion = new EfVersion(db, version);
        }
        return efCurrentVersion;
    }

    private async Task<XtiVersionEntity> AddVersion(AppVersionName versionName, AppVersionKey key, DateTimeOffset timeAdded, AppVersionType type, AppVersionStatus status, AppVersionNumber versionNumber, CancellationToken ct)
    {
        if (key.IsNone() || key.IsCurrent())
        {
            throw new ArgumentException($"Unable to add version with key '{key.DisplayText}'");
        }
        var record = await db.Context.Transaction
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
                await db.Context.Versions.Create(versionEntity, ct);
                return versionEntity;
            }
        );
        return record;
    }

    public async Task<EfVersion> Version(int id, CancellationToken ct)
    {
        var version = await db.Context.Versions.Retrieve()
            .Where(v => v.ID == id)
            .FirstOrDefaultAsync(ct);
        return new EfVersion(db, version ?? throw new Exception($"Version {id} not found"));
    }

    public async Task<EfVersion> VersionByName(AppVersionName versionName, AppVersionKey versionKey, CancellationToken ct)
    {
        var version = await GetVersionByName(versionName, versionKey, ct);
        return new EfVersion(db, version ?? throw new Exception($"Version '{versionName.DisplayText} {versionKey.DisplayText}' was not found"));
    }

    public Task<EfVersion[]> VersionsByName(AppVersionName versionName, CancellationToken ct) =>
        db.Context
            .Versions
            .Retrieve()
            .Where
            (
                v => v.VersionName == versionName.Value
            )
            .Select(v => new EfVersion(db, v))
            .ToArrayAsync(ct);

    internal async Task<EfAppVersion> VersionByApp(EfApp app, AppVersionKey versionKey, CancellationToken ct)
    {
        var version = await GetVersionByApp(app, versionKey, ct);
        var efVersion = new EfVersion(db, version ?? throw new Exception($"Version '{app.ToModel().AppKey.Format()} {versionKey.DisplayText}' was not found"));
        return efVersion.App(app);
    }

    internal async Task<EfAppVersion> VersionByAppOrUnknown(EfApp app, AppVersionKey versionKey, CancellationToken ct)
    {
        var version = await GetVersionByApp(app, versionKey, ct);
        if (version == null)
        {
            var efUnknownApp = await db.Apps.App(AppKey.Unknown, ct);
            version = await GetVersionByApp(efUnknownApp, AppVersionKey.Current, ct);
            if (version == null)
            {
                var efUnknownVersion = await AddCurrentVersionToAppsIfNotFound
                (
                    AppVersionName.Unknown,
                    DateTimeOffset.Now,
                    [AppKey.Unknown],
                    ct
                );
                return efUnknownVersion.App(efUnknownApp);
            }
        }
        var efVersion = new EfVersion
        (
            db,
            version ?? throw new Exception($"Version '{versionKey.DisplayText}' was not found")
        );
        return efVersion.App(app);
    }

    private async Task<XtiVersionEntity?> GetVersionByApp(EfApp app, AppVersionKey versionKey, CancellationToken ct)
    {
        XtiVersionEntity? version;
        var versionName = app.ToModel().VersionName.Value;
        var versionIDs = db.Context.AppVersions.Retrieve()
            .Where(av => av.AppID == app.ID)
            .Select(av => av.VersionID);
        if (versionKey.IsCurrent())
        {
            version = await db.Context.Versions.Retrieve()
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
            version = null;
        }
        else
        {
            version = await db.Context.Versions.Retrieve()
                .Where
                (
                    v =>
                        versionIDs.Contains(v.ID) &&
                        v.VersionName == versionName &&
                        v.VersionKey == versionKey.Value
                )
                .FirstOrDefaultAsync(ct);
        }
        return version;
    }

    private async Task<XtiVersionEntity?> GetVersionByName(AppVersionName versionName, AppVersionKey versionKey, CancellationToken ct)
    {
        XtiVersionEntity? version;
        if (versionKey.IsCurrent())
        {
            version = await db.Context.Versions.Retrieve()
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
            version = null;
        }
        else
        {
            version = await db.Context.Versions.Retrieve()
                .Where(v => v.VersionName == versionName.Value && v.VersionKey == versionKey.Value)
                .FirstOrDefaultAsync(ct);
        }
        return version;
    }

    internal Task<EfVersion[]> VersionsByApp(EfApp app, CancellationToken ct)
    {
        var versionIDs = db.Context.AppVersions.Retrieve()
            .Where(av => av.AppID == app.ID)
            .Select(av => av.VersionID);
        return db.Context.Versions.Retrieve()
            .Where(v => versionIDs.Contains(v.ID))
            .Select(v => new EfVersion(db, v))
            .ToArrayAsync(ct);
    }

    internal async Task Publishing(XtiVersionEntity version, CancellationToken ct)
    {
        if (!AppVersionStatus.Values.New.Equals(version.Status) && !AppVersionStatus.Values.Publishing.Equals(version.Status))
        {
            throw PublishException.Publishing(AppVersionKey.Parse(version.VersionKey), AppVersionStatus.Values.Value(version.Status));
        }
        var lastVersionNumber = await db.Context.Versions.Retrieve()
            .Where(v => v.VersionName == version.VersionName && v.ID != version.ID)
            .OrderByDescending(v => v.Major)
            .ThenByDescending(v => v.Minor)
            .ThenByDescending(v => v.Patch)
            .Select(v => new AppVersionNumber(v.Major, v.Minor, v.Patch))
            .FirstOrDefaultAsync(ct) ??
            new AppVersionNumber(0, 0, 0);
        var nextVersionNumber = lastVersionNumber.Next(AppVersionType.Values.Value(version.Type));
        await db.Context.Versions.Update
        (
            version,
            r =>
            {
                r.Status = AppVersionStatus.Values.Publishing.Value;
                r.Major = nextVersionNumber.Major;
                r.Minor = nextVersionNumber.Minor;
                r.Patch = nextVersionNumber.Patch;
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
        await ArchivePreviousVersions(version, ct);
        await db.Context.Versions.Update
        (
            version,
            r => r.Status = AppVersionStatus.Values.Current.Value,
            ct
        );
    }

    private async Task ArchivePreviousVersions(XtiVersionEntity version, CancellationToken ct)
    {
        var versionsToAchive = await db.Context.Versions.Retrieve()
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
            await db.Context.Versions.Update
            (
                versionToArchive,
                v => v.Status = AppVersionStatus.Values.Old.Value,
                ct
            );
        }
    }
}