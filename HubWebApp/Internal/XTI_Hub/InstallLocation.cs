using XTI_Hub.Abstractions;
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

    public Task<bool> HasCurrentInstallation(AppVersion appVersion, CancellationToken ct)
        => hubFactory.Installations.HasCurrentInstallation(this, appVersion, ct);

    public Task<Installation> CurrentInstallation(AppVersion appVersion, CancellationToken ct)
        => hubFactory.Installations.CurrentInstallation(this, appVersion, ct);

    public Task<Installation> NewCurrentInstallation(AppVersion appVersion, string domain, string siteName, DateTimeOffset timeAdded, CancellationToken ct)
        => hubFactory.Installations.NewCurrentInstallation(this, appVersion, domain, siteName, timeAdded, ct);

    public Task<Installation> NewVersionInstallation(AppVersion appVersion, string domain, string siteName, DateTimeOffset timeAdded, CancellationToken ct)
        => hubFactory.Installations.NewVersionInstallation(this, appVersion, domain, siteName, timeAdded, ct);

    public InstallLocationModel ToModel() => new InstallLocationModel(entity.ID, entity.QualifiedMachineName);

    public override string ToString() => $"{nameof(InstallLocation)} {entity.ID}";
}