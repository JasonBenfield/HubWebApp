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

    public Task<InstallLocation> Location() => hubFactory.InstallLocations.Location(entity.LocationID);

    public Task BeginInstallation() => hubFactory.Installations.BeginInstallation(entity);

    public Task Installed() => hubFactory.Installations.Installed(entity);

    public Task RequestDelete() => hubFactory.Installations.RequestDelete(entity);

    public Task BeginDelete() => hubFactory.Installations.BeginDelete(entity);

    public Task Deleted() => hubFactory.Installations.Deleted(entity);

    public async Task<AppVersion> AppVersion()
    {
        var appVersionEntity = await hubFactory.DB.AppVersions.Retrieve()
            .Where(av => av.ID == entity.AppVersionID)
            .FirstAsync();
        var app = await hubFactory.Apps.App(appVersionEntity.AppID);
        var version = await hubFactory.Versions.Version(appVersionEntity.VersionID);
        return new AppVersion(hubFactory, app, version);
    }

    public Task<ResourceGroup> ResourceGroupOrDefault(ResourceGroupName groupName) =>
        hubFactory.Groups.GroupOrDefault(entity.AppVersionID, groupName);

    public InstallationModel ToModel() => 
        new InstallationModel(ID, Status(), entity.IsCurrent, entity.Domain, entity.SiteName);

    private InstallStatus Status() => InstallStatus.Values.Value(entity.Status);

    public override string ToString() => $"{nameof(Installation)} {entity.ID}";

}