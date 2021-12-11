using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using XTI_HubDB.Entities;

namespace XTI_Hub
{
    public sealed class InstallationRepository
    {
        private readonly AppFactory appFactory;

        public InstallationRepository(AppFactory appFactory)
        {
            this.appFactory = appFactory;
        }

        public Task<Installation> Installation(int installationID)
            => appFactory.DB
                .Installations
                .Retrieve()
                .Where(inst => inst.ID == installationID)
                .Select(inst => appFactory.Installation(inst))
                .SingleAsync();

        internal Task<bool> HasCurrentInstallation(InstallLocation location, App app)
            => currentInstallation(location, app).AnyAsync();

        internal Task<CurrentInstallation> CurrentInstallation(InstallLocation location, App app)
            => currentInstallation(location, app).FirstAsync();

        private IQueryable<CurrentInstallation> currentInstallation(InstallLocation location, App app)
        {
            var versionIDs = appFactory.DB
                .Versions
                .Retrieve()
                .Where(v => v.AppID == app.ID.Value)
                .Select(v => v.ID);
            return appFactory.DB
                .Installations
                .Retrieve()
                .Where
                (
                    inst =>
                        inst.IsCurrent
                        && inst.LocationID == location.ID.Value
                        && versionIDs.Any(id => id == inst.VersionID)
                )
                .Select(inst => appFactory.CurrentInstallation(inst));
        }

        internal Task<bool> HasVersionInstallation(InstallLocation location, AppVersion appVersion)
            => versionInstallation(location, appVersion).AnyAsync();

        internal Task<VersionInstallation> VersionInstallation(InstallLocation location, AppVersion appVersion)
            => versionInstallation(location, appVersion).FirstAsync();

        private IQueryable<VersionInstallation> versionInstallation(InstallLocation location, AppVersion appVersion)
            => appFactory.DB
                .Installations
                .Retrieve()
                .Where
                (
                    inst =>
                        !inst.IsCurrent
                        && inst.LocationID == location.ID.Value
                        && inst.VersionID == appVersion.ID.Value
                )
                .Select(inst => appFactory.VersionInstallation(inst));

        internal async Task<CurrentInstallation> NewCurrentInstallation(InstallLocation location, AppVersion version, DateTimeOffset timeAdded)
        {
            var entity = await NewInstallation(location, version, timeAdded, true);
            return appFactory.CurrentInstallation(entity);
        }

        internal async Task<VersionInstallation> NewVersionInstallation(InstallLocation location, AppVersion version, DateTimeOffset timeAdded)
        {
            var entity = await NewInstallation(location, version, timeAdded, false);
            return appFactory.VersionInstallation(entity);
        }

        private async Task<InstallationEntity> NewInstallation(InstallLocation location, AppVersion version, DateTimeOffset timeAdded, bool isCurrent)
        {
            var entity = new InstallationEntity
            {
                LocationID = location.ID.Value,
                VersionID = version.ID.Value,
                Status = InstallStatus.Values.InstallPending.Value,
                IsCurrent = isCurrent,
                TimeAdded = timeAdded
            };
            await appFactory.DB.Installations.Create(entity);
            return entity;
        }
    }
}
