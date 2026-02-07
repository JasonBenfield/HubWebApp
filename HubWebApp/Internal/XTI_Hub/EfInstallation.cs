using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfInstallation
{
    private readonly EfHubDB db;
    private readonly InstallationEntity installation;

    internal EfInstallation(EfHubDB db, InstallationEntity installation)
    {
        this.db = db;
        this.installation = installation;
    }

    internal int ID { get => installation.ID; }

    public Task<EfInstallLocation> Location(CancellationToken ct) => db.InstallLocations.Location(installation.LocationID, ct);

    public Task BeginInstallation(CancellationToken ct) =>
        SetInstallationStatus(InstallStatus.Values.InstallStarted, ct);

    public async Task Installed(CancellationToken ct)
    {
        var efPreviousInstallations = await db.Installations.PreviousInstallations(installation, ct);
        foreach (var efPreviousInstallation in efPreviousInstallations)
        {
            await efPreviousInstallation.Deleted(ct);
        }
        await SetInstallationStatus(InstallStatus.Values.Installed, ct);
    }

    public Task RequestDelete(CancellationToken ct) =>
        SetInstallationStatus(InstallStatus.Values.DeletePending, ct);

    public Task BeginDelete(CancellationToken ct) =>
        SetInstallationStatus(InstallStatus.Values.DeleteStarted, ct);

    public Task Deleted(CancellationToken ct) =>
        SetInstallationStatus(InstallStatus.Values.Deleted, ct);

    private Task SetInstallationStatus(InstallStatus status, CancellationToken ct) =>
        db.Context.Installations.Update
        (
            installation,
            inst => inst.Status = status.Value,
            ct
        );

    public async Task<EfAppVersion> AppVersion(CancellationToken ct)
    {
        var appVersionEntity = await db.Context.AppVersions.Retrieve()
            .Where(av => av.ID == installation.AppVersionID)
            .FirstAsync(ct);
        var efApp = await db.Apps.App(appVersionEntity.AppID, ct);
        var efVersion = await db.Versions.Version(appVersionEntity.VersionID, ct);
        return new EfAppVersion(db, efApp, efVersion);
    }

    public Task<EfResourceGroup> ResourceGroupOrDefault(ResourceGroupName groupName, CancellationToken ct) =>
        db.Groups.GroupOrDefault(installation.AppVersionID, groupName, ct);

    public Task<EfAppRequest[]> MostRecentRequests(int howMany, CancellationToken ct) =>
        db.Requests.MostRecentForInstallation(this, howMany, ct);

    internal Task<EfAppCommand> RequestedInstallationOrDefault(CancellationToken ct) =>
        db.AppCommands.InstallCommandOrDefault(installation, ct);

    public InstallationModel ToModel() =>
        new InstallationModel
        (
            ID,
            Status(),
            installation.IsCurrent,
            installation.Domain,
            installation.SiteName
        );

    private InstallStatus Status() => InstallStatus.Values.Value(installation.Status);

    public override string ToString() => $"{nameof(EfInstallation)} {installation.ID}";

}