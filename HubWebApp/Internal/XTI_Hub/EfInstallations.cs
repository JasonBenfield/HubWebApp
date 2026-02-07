using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfInstallations
{
    private readonly EfHubDB db;

    internal EfInstallations(EfHubDB db)
    {
        this.db = db;
    }

    public Task<EfInstallation[]> GetPendingDeletes(string machineName, CancellationToken ct)
    {
        var locationIDs = db.Context.InstallLocations.Retrieve()
            .Where(l => l.QualifiedMachineName == machineName.ToLower())
            .Select(l => l.ID);
        return db.Context.Installations.Retrieve()
            .Where(inst => locationIDs.Contains(inst.LocationID) && inst.Status == InstallStatus.Values.DeletePending)
            .Select(inst => new EfInstallation(db, inst))
            .ToArrayAsync(ct);
    }

    internal async Task<EfInstallation> NewInstallation(EfInstallLocation efLocation, EfAppVersion efAppVersion, string domain, string siteName, DateTimeOffset timeAdded, InstallStatus initialStatus, bool isCurrent, CancellationToken ct)
    {
        var appVersionID = await efAppVersion.AppVersionID(ct);
        var installation = new InstallationEntity
        {
            LocationID = efLocation.ID,
            AppVersionID = appVersionID,
            Status = initialStatus.Value,
            IsCurrent = isCurrent,
            TimeAdded = timeAdded,
            Domain = domain,
            SiteName = siteName
        };
        await db.Context.Installations.Create(installation, ct);
        return new EfInstallation(db, installation);
    }

    internal async Task<EfInstallation[]> Installations(IQueryable<int> installationIDs, CancellationToken ct)
    {
        var installations = await db.Context.Installations.Retrieve()
            .Where(inst => installationIDs.Contains(inst.ID))
            .ToArrayAsync(ct);
        return installations.Select(inst => new EfInstallation(db, inst)).ToArray();
    }

    internal async Task<EfInstallation[]> PreviousInstallations(InstallationEntity entity, CancellationToken ct)
    {
        var installationsQuery = db.Context.Installations.Retrieve();
        if (entity.IsCurrent)
        {
            var appID = await db.Context.AppVersions.Retrieve()
                .Where(av => av.ID == entity.AppVersionID)
                .Select(av => av.AppID)
                .FirstAsync(ct);
            var appVersionIDs = db.Context.AppVersions.Retrieve()
                .Where(av => av.AppID == appID)
                .Select(av => av.ID);
            installationsQuery = installationsQuery
                .Where
                (
                    inst =>
                        inst.ID != entity.ID &&
                        inst.LocationID == entity.LocationID &&
                        appVersionIDs.Contains(inst.AppVersionID) &&
                        inst.IsCurrent == entity.IsCurrent
                );
        }
        else
        {
            installationsQuery = installationsQuery
                .Where
                (
                    inst =>
                        inst.ID != entity.ID &&
                        inst.LocationID == entity.LocationID &&
                        inst.AppVersionID == entity.AppVersionID &&
                        inst.IsCurrent == entity.IsCurrent
                );
        }
        var previousInstallations = await installationsQuery.ToArrayAsync(ct);
        var efPreviousInstallations = previousInstallations
            .Select(inst => new EfInstallation(db, inst))
            .ToArray();
        return efPreviousInstallations;
    }

    public async Task<EfInstallation> InstallationOrDefault(int installationID, CancellationToken ct)
    {
        EfInstallation efInstallation;
        if (installationID > 0)
        {
            var installation = await db.Context.Installations.Retrieve()
                .Where(inst => inst.ID == installationID)
                .FirstOrDefaultAsync(ct);
            if (installation == null)
            {
                var efUnknownLoc = await db.InstallLocations.UnknownLocation(ct);
                var efUnknownApp = await db.Apps.AppOrUnknown(AppKey.Unknown, ct);
                var efCurrentVersion = await efUnknownApp.CurrentVersion(ct);
                efInstallation = await efUnknownLoc.CurrentInstallation(efCurrentVersion, ct);
            }
            else
            {
                efInstallation = new EfInstallation(db, installation);
            }
        }
        else
        {
            efInstallation = new EfInstallation(db, new());
        }
        return efInstallation;
    }

    internal Task<bool> HasCurrentInstallation(EfInstallLocation location, EfAppVersion appVersion, CancellationToken ct) =>
        GetCurrentInstallation(location, appVersion).AnyAsync(ct);

    internal Task<EfInstallation> CurrentInstallation(EfInstallLocation location, EfAppVersion appVersion, CancellationToken ct) =>
        GetCurrentInstallation(location, appVersion).FirstAsync(ct);

    private IQueryable<EfInstallation> GetCurrentInstallation(EfInstallLocation location, EfAppVersion appVersion)
    {
        var appVersionIDs = appVersion.QueryAppVersionID();
        return db.Context
            .Installations
            .Retrieve()
            .Where
            (
                inst =>
                    inst.IsCurrent
                    && inst.LocationID == location.ID
                    && appVersionIDs.Contains(inst.AppVersionID)
                    && inst.Status == InstallStatus.Values.Installed.Value
            )
            .Select(inst => new EfInstallation(db, inst));
    }

    public async Task<AppDomainModel[]> AppDomains(CancellationToken ct)
    {
        var appDomainEntities = await db.Context
            .Installations
            .Retrieve()
            .Where
            (
                inst =>
                    !string.IsNullOrWhiteSpace(inst.Domain) &&
                    inst.Status == InstallStatus.Values.Installed.Value
            )
            .Join
            (
                db.Context.AppVersions.Retrieve(),
                inst => inst.AppVersionID,
                av => av.ID,
                (inst, av) => new { Installation = inst, AppVersion = av }
            )
            .Join
            (
                db.Context.Versions.Retrieve(),
                grouped => grouped.AppVersion.VersionID,
                v => v.ID,
                (grouped, v) => new
                {
                    Installation = grouped.Installation,
                    AppVersion = grouped.AppVersion,
                    Version = v
                }
            )
            .Join
            (
                db.Context.Apps.Retrieve(),
                grouped => grouped.AppVersion.AppID,
                a => a.ID,
                (grouped, app) => new
                {
                    App = app,
                    Version = grouped.Version,
                    Domain = grouped.Installation.Domain
                }
            )
            .ToArrayAsync(ct);
        return appDomainEntities
            .Select
            (
                ad => new AppDomainModel
                (
                    new AppKey
                    (
                        new AppName(ad.App.DisplayText),
                        AppType.Values.Value(ad.App.Type)
                    ),
                    AppVersionKey.Parse(ad.Version.VersionKey),
                    ad.Domain
                )
            )
            .ToArray();
    }

}