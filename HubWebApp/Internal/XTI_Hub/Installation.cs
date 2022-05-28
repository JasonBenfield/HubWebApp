using XTI_App.Abstractions;
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
        ID = entity.ID;
    }

    public int ID { get; }

    public InstallStatus Status() => InstallStatus.Values.Value(entity.Status);

    public Task Installed() => hubFactory.Installations.Installed(entity);

    public Task Start() => hubFactory.Installations.StartInstallation(entity);

    public Task<ResourceGroup> ResourceGroupOrDefault(ResourceGroupName groupName) =>
        hubFactory.Groups.GroupOrDefault(entity.AppVersionID, groupName);

    public InstallationModel ToModel() => new InstallationModel
    {
        ID = ID,
        Status = Status()
    };

    public override string ToString() => $"{nameof(Installation)} {entity.ID}";
}