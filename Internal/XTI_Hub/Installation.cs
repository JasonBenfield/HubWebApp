using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public class Installation
{
    private readonly AppFactory appFactory;
    private readonly InstallationEntity entity;

    protected Installation(AppFactory appFactory, InstallationEntity entity)
    {
        this.appFactory = appFactory;
        this.entity = entity;
        ID = new EntityID(entity.ID);
    }

    public EntityID ID { get; }

    public InstallStatus Status() => InstallStatus.Values.Value(entity.Status);

    public Task InstallPending() => SetStatus(InstallStatus.Values.InstallPending);

    public Task Installed() => SetStatus(InstallStatus.Values.Installed);

    private Task SetStatus(InstallStatus status)
        => appFactory.DB
            .Installations
            .Update(entity, inst => inst.Status = status.Value);

    protected Task StartVersion() => SetStatus(InstallStatus.Values.InstallStarted);

    protected Task StartCurrent(AppVersion appVersion)
        => appFactory.DB
            .Installations
            .Update
            (
                entity,
                inst =>
                {
                    inst.Status = InstallStatus.Values.InstallStarted;
                    inst.VersionID = appVersion.ID.Value;
                }
            );

    public InstallationModel ToModel() => new InstallationModel
    {
        ID = ID.Value,
        Status = Status()
    };

    public override string ToString() => $"{nameof(Installation)} {entity.ID}";
}