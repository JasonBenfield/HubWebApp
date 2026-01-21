using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class Installation
{
    private readonly HubFactory hubFactory;
    private readonly InstallationEntity entity;

    internal Installation(HubFactory hubFactory, InstallationEntity entity)
    {
        this.hubFactory = hubFactory;
        this.entity = entity;
    }

    internal int ID { get => entity.ID; }

    public Task<InstallLocation> Location(CancellationToken ct) => hubFactory.InstallLocations.Location(entity.LocationID, ct);

    public Task BeginInstallation(CancellationToken ct) => hubFactory.Installations.BeginInstallation(entity, ct);

    public Task Installed(CancellationToken ct) => hubFactory.Installations.Installed(entity, ct);

    public Task RequestDelete(CancellationToken ct) => hubFactory.Installations.RequestDelete(entity, ct);

    public Task BeginDelete(CancellationToken ct) => hubFactory.Installations.BeginDelete(entity, ct);

    public Task Deleted(CancellationToken ct) => hubFactory.Installations.Deleted(entity, ct);

    public async Task<AppVersion> AppVersion(CancellationToken ct)
    {
        var appVersionEntity = await hubFactory.DB.AppVersions.Retrieve()
            .Where(av => av.ID == entity.AppVersionID)
            .FirstAsync(ct);
        var app = await hubFactory.Apps.App(appVersionEntity.AppID, ct);
        var version = await hubFactory.Versions.Version(appVersionEntity.VersionID, ct);
        return new AppVersion(hubFactory, app, version);
    }

    public Task<ResourceGroup> ResourceGroupOrDefault(ResourceGroupName groupName, CancellationToken ct) =>
        hubFactory.Groups.GroupOrDefault(entity.AppVersionID, groupName, ct);

    public Task<AppRequest[]> MostRecentRequests(int howMany, CancellationToken ct) =>
        hubFactory.Requests.MostRecentForInstallation(this, howMany, ct);

    public InstallationModel ToModel() => 
        new InstallationModel(ID, Status(), entity.IsCurrent, entity.Domain, entity.SiteName);

    private InstallStatus Status() => InstallStatus.Values.Value(entity.Status);

    public override string ToString() => $"{nameof(Installation)} {entity.ID}";

}