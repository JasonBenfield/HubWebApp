using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class InstallationRepository
{
    private readonly HubFactory hubFactory;

    internal InstallationRepository(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    internal async Task<Installation> NewCurrentInstallation(InstallLocation location, AppVersion appVersion, string domain, DateTimeOffset timeAdded)
    {
        var entity = await NewInstallation(location, appVersion, domain, timeAdded, true);
        return hubFactory.CreateInstallation(entity);
    }

    internal async Task<Installation> NewVersionInstallation(InstallLocation location, AppVersion appVersion, string domain, DateTimeOffset timeAdded)
    {
        var entity = await NewInstallation(location, appVersion, domain, timeAdded, false);
        return hubFactory.CreateInstallation(entity);
    }

    private async Task<InstallationEntity> NewInstallation(InstallLocation location, AppVersion appVersion, string domain, DateTimeOffset timeAdded, bool isCurrent)
    {
        var appVersionID = await appVersion.AppVersionID();
        var entity = new InstallationEntity
        {
            LocationID = location.ID,
            AppVersionID = appVersionID,
            Status = InstallStatus.Values.InstallPending.Value,
            IsCurrent = isCurrent,
            TimeAdded = timeAdded,
            Domain = domain
        };
        await hubFactory.DB.Installations.Create(entity);
        return entity;
    }

    internal Task StartInstallation(InstallationEntity entity) =>
        hubFactory.DB
            .Installations
            .Update
            (
                entity,
                inst =>
                {
                    inst.Status = InstallStatus.Values.InstallStarted.Value;
                }
            );

    internal Task Installed(InstallationEntity entity)=>
        hubFactory.DB.Transaction
        (
            async () =>
            {
                var previousInstallations = await hubFactory.DB
                    .Installations.Retrieve()
                    .Where
                    (
                        inst =>
                            inst.ID != entity.ID &&
                            inst.LocationID == entity.LocationID &&
                            inst.AppVersionID == entity.AppVersionID &&
                            inst.IsCurrent == entity.IsCurrent
                    )
                    .ToArrayAsync();
                foreach (var previousInst in previousInstallations)
                {
                    await SetInstallationStatus(previousInst, InstallStatus.Values.Deleted);
                }
                await SetInstallationStatus(entity, InstallStatus.Values.Installed);
            }
        );

    private Task SetInstallationStatus(InstallationEntity entity, InstallStatus status) =>
        hubFactory.DB
            .Installations
            .Update(entity, inst => inst.Status = status.Value);

    public async Task<Installation> InstallationOrDefault(int installationID)
    {
        var installation = await hubFactory.DB
            .Installations
            .Retrieve()
            .Where(inst => inst.ID == installationID)
            .Select(inst => hubFactory.CreateInstallation(inst))
            .FirstOrDefaultAsync();
        if(installation == null)
        {
            var unknownLoc = await hubFactory.InstallLocations.UnknownLocation();
            var unknownApp = await hubFactory.Apps.AppOrUnknown(AppKey.Unknown);
            var currentVersion = await unknownApp.CurrentVersion();
            installation = await unknownLoc.CurrentInstallation(currentVersion);
        }
        return installation;
    }

    internal Task<bool> HasCurrentInstallation(InstallLocation location, AppVersion appVersion)
        => currentInstallation(location, appVersion).AnyAsync();

    internal Task<Installation> CurrentInstallation(InstallLocation location, AppVersion appVersion)
        => currentInstallation(location, appVersion).FirstAsync();

    private IQueryable<Installation> currentInstallation(InstallLocation location, AppVersion appVersion)
    {
        var appVersionIDs = appVersion.QueryAppVersionID();
        return hubFactory.DB
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
            .Select(inst => hubFactory.CreateInstallation(inst));
    }

    public Task<AppDomainModel[]> AppDomains() =>
        hubFactory.DB
            .Installations
            .Retrieve()
            .Where
            (
                inst => inst.IsCurrent && !string.IsNullOrWhiteSpace(inst.Domain)
            )
        .Join
        (
            hubFactory.DB.AppVersions.Retrieve(),
            inst => inst.AppVersionID,
            av => av.ID,
            (inst, av) => new { Installation = inst, AppVersion = av }
        )
        .Join
        (
            hubFactory.DB.Apps.Retrieve(),
            grouped => grouped.AppVersion.AppID,
            a => a.ID,
            (grouped, app) => new AppDomainModel
            (
                new AppKey(new AppName(app.Name), AppType.Values.Value(app.Type)),
                grouped.Installation.Domain
            )
        )
        .ToArrayAsync();

}