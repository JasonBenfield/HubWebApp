using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class InstallLocation
{
    private readonly AppFactory appFactory;
    private readonly InstallLocationEntity entity;

    internal InstallLocation(AppFactory appFactory, InstallLocationEntity entity)
    {
        this.appFactory = appFactory;
        this.entity = entity;
        ID = new EntityID(entity.ID);
    }

    public EntityID ID { get; }

    public string QualifiedName() => entity.QualifiedMachineName;

    public string MachineName()
    {
        var dotIndex = entity.QualifiedMachineName.IndexOf(".");
        if (dotIndex > -1)
        {
            return entity.QualifiedMachineName.Substring(0, dotIndex);
        }
        return entity.QualifiedMachineName;
    }

    public Task<bool> HasCurrentInstallation(App app)
        => appFactory.Installations.HasCurrentInstallation(this, app);

    public Task<CurrentInstallation> CurrentInstallation(App app)
        => appFactory.Installations.CurrentInstallation(this, app);

    public Task<CurrentInstallation> NewCurrentInstallation(AppVersion appVersion, DateTimeOffset timeAdded)
        => appFactory.Installations.NewCurrentInstallation(this, appVersion, timeAdded);

    public Task<bool> HasVersionInstallation(AppVersion appVersion)
        => appFactory.Installations.HasVersionInstallation(this, appVersion);

    public Task<VersionInstallation> VersionInstallation(AppVersion appVersion)
        => appFactory.Installations.VersionInstallation(this, appVersion);

    public Task<VersionInstallation> NewVersionInstallation(AppVersion appVersion, DateTimeOffset timeAdded)
        => appFactory.Installations.NewVersionInstallation(this, appVersion, timeAdded);

    public override string ToString() => $"{nameof(InstallLocation)} {entity.ID}";
}