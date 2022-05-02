using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public class Installation
{
    private readonly HubFactory appFactory;
    private readonly InstallationEntity entity;

    protected Installation(HubFactory appFactory, InstallationEntity entity)
    {
        this.appFactory = appFactory;
        this.entity = entity;
        ID = entity.ID;
    }

    public int ID { get; }

    public InstallStatus Status() => InstallStatus.Values.Value(entity.Status);

    public Task InstallPending() => SetStatus(InstallStatus.Values.InstallPending);

    public Task Installed() => SetStatus(InstallStatus.Values.Installed);

    private Task SetStatus(InstallStatus status) =>
        appFactory.DB
            .Installations
            .Update(entity, inst => inst.Status = status.Value);

    protected Task StartVersion(string domain) =>
        appFactory.DB
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
        await appFactory.DB
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

    public InstallationModel ToModel() => new InstallationModel
    {
        ID = ID,
        Status = Status()
    };

    public override string ToString() => $"{nameof(Installation)} {entity.ID}";
}