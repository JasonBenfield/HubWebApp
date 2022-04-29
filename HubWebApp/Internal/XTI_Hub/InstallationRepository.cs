using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

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
            .Select(inst => appFactory.CreateInstallation(inst))
            .SingleAsync();

    internal Task<bool> HasCurrentInstallation(InstallLocation location, App app)
        => currentInstallation(location, app).AnyAsync();

    internal Task<CurrentInstallation> CurrentInstallation(InstallLocation location, App app)
        => currentInstallation(location, app).FirstAsync();

    private IQueryable<CurrentInstallation> currentInstallation(InstallLocation location, App app)
    {
        var appVersionIDs = appFactory.DB
            .AppVersions
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
                    && appVersionIDs.Contains(inst.AppVersionID)
            )
            .Select(inst => appFactory.CurrentInstallation(inst));
    }

    internal Task<bool> HasVersionInstallation(InstallLocation location, AppVersion appVersion)
        => versionInstallation(location, appVersion).AnyAsync();

    internal Task<VersionInstallation> VersionInstallation(InstallLocation location, AppVersion appVersion)
        => versionInstallation(location, appVersion).FirstAsync();

    private IQueryable<VersionInstallation> versionInstallation(InstallLocation location, AppVersion appVersion)
    {
        var appVersionIDs = appVersion.QueryAppVersionID();
        return appFactory.DB
            .Installations
            .Retrieve()
            .Where
            (
                inst =>
                    !inst.IsCurrent
                    && inst.LocationID == location.ID.Value
                    && appVersionIDs.Contains(inst.AppVersionID)
            )
            .Select(inst => appFactory.VersionInstallation(inst));
    }

    internal async Task<CurrentInstallation> NewCurrentInstallation(InstallLocation location, AppVersion appVersion, DateTimeOffset timeAdded)
    {
        var entity = await NewInstallation(location, appVersion, timeAdded, true);
        return appFactory.CurrentInstallation(entity);
    }

    internal async Task<VersionInstallation> NewVersionInstallation(InstallLocation location, AppVersion appVersion, DateTimeOffset timeAdded)
    {
        var entity = await NewInstallation(location, appVersion, timeAdded, false);
        return appFactory.VersionInstallation(entity);
    }

    private async Task<InstallationEntity> NewInstallation(InstallLocation location, AppVersion appVersion, DateTimeOffset timeAdded, bool isCurrent)
    {
        var appVersionID = await appVersion.AppVersionID();
        var entity = new InstallationEntity
        {
            LocationID = location.ID.Value,
            AppVersionID = appVersionID,
            Status = InstallStatus.Values.InstallPending.Value,
            IsCurrent = isCurrent,
            TimeAdded = timeAdded,
            Domain = ""
        };
        await appFactory.DB.Installations.Create(entity);
        return entity;
    }

    public Task<AppDomainModel[]> AppDomains() =>
        appFactory.DB
            .Installations
            .Retrieve()
            .Where
            (
                inst => inst.IsCurrent && !string.IsNullOrWhiteSpace(inst.Domain)
            )
        .Join
        (
            appFactory.DB.AppVersions.Retrieve(),
            inst => inst.AppVersionID,
            av => av.ID,
            (inst, av) => new { Installation = inst, AppVersion = av }
        )
        .Join
        (
            appFactory.DB.Apps.Retrieve(),
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