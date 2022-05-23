using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public class Installation
{
    private readonly HubFactory hubFactory;
    private readonly InstallationEntity entity;

    protected Installation(HubFactory hubFactory, InstallationEntity entity)
    {
        this.hubFactory = hubFactory;
        this.entity = entity;
        ID = entity.ID;
    }

    public int ID { get; }

    public InstallStatus Status() => InstallStatus.Values.Value(entity.Status);

    public Task InstallPending() => SetStatus(InstallStatus.Values.InstallPending);

    public Task Installed() => SetStatus(InstallStatus.Values.Installed);

    private Task SetStatus(InstallStatus status) =>
        hubFactory.DB
            .Installations
            .Update(entity, inst => inst.Status = status.Value);

    protected Task StartVersion(string domain) =>
        hubFactory.DB
            .Installations
            .Update
            (
                entity,
                inst =>
                {
                    inst.Status = InstallStatus.Values.InstallStarted.Value;
                    inst.Domain = domain;
                }
            );

    protected async Task StartCurrent(AppVersion appVersion, string domain)
    {
        var appVersionID = await appVersion.AppVersionID();
        await hubFactory.DB
            .Installations
            .Update
            (
                entity,
                inst =>
                {
                    inst.Status = InstallStatus.Values.InstallStarted.Value;
                    inst.AppVersionID = appVersionID;
                    inst.Domain = domain;
                }
            );
    }

    public Task<ResourceGroup> ResourceGroupOrDefault(ResourceGroupName groupName) =>
        hubFactory.Groups.GroupOrDefault(entity.AppVersionID, groupName);

    public InstallationModel ToModel() => new InstallationModel
    {
        ID = ID,
        Status = Status()
    };

    public override string ToString() => $"{nameof(Installation)} {entity.ID}";
}