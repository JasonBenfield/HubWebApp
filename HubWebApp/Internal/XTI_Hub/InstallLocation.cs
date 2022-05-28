using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class InstallLocation
{
    private readonly HubFactory hubFactory;
    private readonly InstallLocationEntity entity;

    internal InstallLocation(HubFactory hubFactory, InstallLocationEntity entity)
    {
        this.hubFactory = hubFactory;
        this.entity = entity;
        ID = entity.ID;
    }

    public int ID { get; }

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

    public Task<bool> HasCurrentInstallation(AppVersion appVersion)
        => hubFactory.Installations.HasCurrentInstallation(this, appVersion);

    public Task<Installation> CurrentInstallation(AppVersion appVersion)
        => hubFactory.Installations.CurrentInstallation(this, appVersion);

    public Task<Installation> NewCurrentInstallation(AppVersion appVersion, string domain, DateTimeOffset timeAdded)
        => hubFactory.Installations.NewCurrentInstallation(this, appVersion, domain, timeAdded);

    public Task<Installation> NewVersionInstallation(AppVersion appVersion, string domain, DateTimeOffset timeAdded)
        => hubFactory.Installations.NewVersionInstallation(this, appVersion, domain, timeAdded);

    public override string ToString() => $"{nameof(InstallLocation)} {entity.ID}";
}