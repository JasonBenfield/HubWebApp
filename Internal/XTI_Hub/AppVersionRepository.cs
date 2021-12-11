using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub
{
    public sealed class AppVersionRepository
    {
        private readonly AppFactory factory;

        internal AppVersionRepository(AppFactory factory)
        {
            this.factory = factory;
        }

        internal async Task<AppVersion> AddIfNotFound(AppVersionKey key, App app, DateTimeOffset timeAdded, AppVersionStatus status, AppVersionType type, Version version)
        {
            var record = await factory.DB
                .Versions
                .Retrieve()
                .FirstOrDefaultAsync(v => v.AppID == app.ID.Value && v.VersionKey == key.Value);
            if(record == null)
            {
                record = new AppVersionEntity
                {
                    VersionKey = key.Value,
                    AppID = app.ID.Value,
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
            return factory.Version(record);
        }

        internal async Task<AppVersion> StartNewVersion(AppVersionKey key, App app, DateTimeOffset timeAdded, AppVersionType type)
        {
            var validVersionTypes = new List<AppVersionType>(new[] { AppVersionType.Values.Major, AppVersionType.Values.Minor, AppVersionType.Values.Patch });
            if (!validVersionTypes.Contains(type))
            {
                throw new ArgumentException($"Version type {type} is not valid");
            }
            var versionNumber = new Version(0, 0, 0);
            return await AddVersion(key, app, timeAdded, type, AppVersionStatus.Values.New, versionNumber);
        }

        private async Task<AppVersion> AddVersion(AppVersionKey key, App app, DateTimeOffset timeAdded, AppVersionType type, AppVersionStatus status, Version versionNumber)
        {
            AppVersionEntity record = null;
            await factory.DB.Transaction(async () =>
            {
                record = new AppVersionEntity
                {
                    VersionKey = new GeneratedKey().Value(),
                    AppID = app.ID.Value,
                    Major = versionNumber.Major,
                    Minor = versionNumber.Minor,
                    Patch = versionNumber.Build,
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
            return factory.Version(record);
        }

        public async Task<AppVersion> Version(int id)
        {
            var record = await factory.DB
                .Versions
                .Retrieve()
                .FirstAsync(v => v.ID == id);
            return factory.Version(record);
        }

        internal async Task<AppVersion> VersionByApp(App app, AppVersionKey versionKey)
        {
            AppVersionEntity record;
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
                    .FirstAsync();
            }
            else
            {
                record = await factory.DB
                    .Versions
                    .Retrieve()
                    .Where(v => v.AppID == app.ID.Value && v.VersionKey == versionKey.Value)
                    .FirstAsync();
            }
            return factory.Version(record);
        }

        internal Task<AppVersion[]> VersionsByApp(App app)
            => factory.DB
                .Versions
                .Retrieve()
                .Where(v => v.AppID == app.ID.Value)
                .Select(v => factory.Version(v))
                .ToArrayAsync();

        internal Task<AppVersion> AddCurrentVersion(App app, DateTimeOffset timeAdded) =>
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
}
