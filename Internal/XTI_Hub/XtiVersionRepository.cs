using Microsoft.EntityFrameworkCore;
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
        string groupName,
        AppVersionKey key,
        DateTimeOffset timeAdded,
        AppVersionStatus status,
        AppVersionType type,
        AppVersionNumber versionNumber,
        App app
    )
    {
        var entity = await GetVersionByGroupName(groupName, key);
        if (entity == null)
        {
            entity = await AddVersion
            (
                groupName.ToLower(),
                key,
                timeAdded,
                type,
                status,
                versionNumber
            );
        }
        var xtiVersion = factory.CreateVersion(entity);
        await AddVersionToApp(app, xtiVersion);
        return xtiVersion;
    }

    internal async Task AddVersionToApp(App app, XtiVersion version)
    {
        var appVersionExists = await factory.DB
            .AppVersions.Retrieve()
            .Where(av => av.VersionID == version.ID.Value && av.AppID == app.ID.Value)
            .AnyAsync();
        if (!appVersionExists)
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

    public async Task<AppVersionKey> NextKey()
    {
        int maxID;
        var any = await factory.DB
            .Versions.Retrieve()
            .AnyAsync();
        if (any)
        {
            maxID = await factory.DB.Versions.Retrieve().MaxAsync(v => v.ID);
        }
        else
        {
            maxID = 0;
        }
        return new AppVersionKey(maxID + 1);
    }

    public async Task<XtiVersion> StartNewVersion(string groupName, AppVersionKey key, DateTimeOffset timeAdded, AppVersionType type)
    {
        var validVersionTypes = new List<AppVersionType>(new[] { AppVersionType.Values.Major, AppVersionType.Values.Minor, AppVersionType.Values.Patch });
        if (!validVersionTypes.Contains(type))
        {
            throw new ArgumentException($"Version type {type} is not valid");
        }
        var versionNumber = new AppVersionNumber(0, 0, 0);
        var entity = await AddVersion(groupName, key, timeAdded, type, AppVersionStatus.Values.New, versionNumber);
        return factory.CreateVersion(entity);
    }

    private async Task<XtiVersionEntity> AddVersion(string groupName, AppVersionKey key, DateTimeOffset timeAdded, AppVersionType type, AppVersionStatus status, AppVersionNumber versionNumber)
    {
        XtiVersionEntity? record = null;
        await factory.DB.Transaction(async () =>
        {
            record = new XtiVersionEntity
            {
                GroupName = groupName.ToLower(),
                VersionKey = new GeneratedKey().Value(),
                Major = versionNumber.Major,
                Minor = versionNumber.Minor,
                Patch = versionNumber.Patch,
                TimeAdded = timeAdded,
                Description = "",
                Status = status.Value,
                Type = type.Value
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

    public async Task<XtiVersion> VersionByGroupName(string groupName, AppVersionKey versionKey)
    {
        var record = await GetVersionByGroupName(groupName, versionKey);
        return factory.CreateVersion(record ?? throw new Exception($"Version '{versionKey.DisplayText}' was not found"));
    }

    internal async Task<AppVersion> VersionByApp(App app, AppVersionKey versionKey)
    {
        var record = await GetVersionByApp(app, versionKey);
        var version = factory.CreateVersion(record ?? throw new Exception($"Version '{versionKey.DisplayText}' was not found"));
        return version.App(app);
    }

    internal async Task<AppVersion> VersionByAppOrDefault(App app, AppVersionKey versionKey)
    {
        var record = await GetVersionByApp(app, versionKey);
        if (record == null)
        {
            var unknownApp = await factory.Apps.App(AppKey.Unknown);
            record = await GetVersionByApp(unknownApp, AppVersionKey.Current);
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

    private async Task<XtiVersionEntity?> GetVersionByGroupName(string groupName, AppVersionKey versionKey)
    {
        XtiVersionEntity? record;
        groupName = groupName.ToLower();
        if (versionKey.Equals(AppVersionKey.Current))
        {
            record = await factory.DB
                .Versions
                .Retrieve()
                .Where
                (
                    v =>
                        v.GroupName == groupName &&
                        v.Status == AppVersionStatus.Values.Current.Value
                )
                .FirstOrDefaultAsync();
        }
        else
        {
            record = await factory.DB
                .Versions
                .Retrieve()
                .Where(v => v.GroupName == groupName && v.VersionKey == versionKey.Value)
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

    private IQueryable<int> QueryVersionIDs(App app) =>
        factory.DB
            .AppVersions.Retrieve()
            .Where(av => av.AppID == app.ID.Value)
            .Select(av => av.VersionID);

    internal async Task Publishing(XtiVersionEntity version)
    {
        if (!AppVersionStatus.Values.New.Equals(version.Status) && !AppVersionStatus.Values.Publishing.Equals(version.Status))
        {
            throw PublishException.Publishing(AppVersionKey.Parse(version.VersionKey), AppVersionStatus.Values.Value(version.Status));
        }
        var lastVersion = await factory.DB
            .Versions.Retrieve()
            .Where(v => v.GroupName == version.GroupName)
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
                    .Where(v => v.ID != version.ID && v.Status != AppVersionStatus.Values.Old)
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