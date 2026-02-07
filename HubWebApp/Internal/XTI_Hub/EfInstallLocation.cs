using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfInstallLocation
{
    private readonly EfHubDB db;
    private readonly InstallLocationEntity location;

    internal EfInstallLocation(EfHubDB db, InstallLocationEntity location)
    {
        this.db = db;
        this.location = location;
        ID = location.ID;
    }

    public int ID { get; }

    public string QualifiedName() => location.QualifiedMachineName;

    public string MachineName()
    {
        var dotIndex = location.QualifiedMachineName.IndexOf(".");
        if (dotIndex > -1)
        {
            return location.QualifiedMachineName.Substring(0, dotIndex);
        }
        return location.QualifiedMachineName;
    }

    public Task<bool> HasCurrentInstallation(EfAppVersion appVersion, CancellationToken ct) =>
        db.Installations.HasCurrentInstallation(this, appVersion, ct);

    public Task<EfInstallation> CurrentInstallation(EfAppVersion appVersion, CancellationToken ct) =>
        db.Installations.CurrentInstallation(this, appVersion, ct);

    public Task<EfInstallation> NewCurrentInstallation(EfAppVersion appVersion, string domain, string siteName, DateTimeOffset timeAdded, CancellationToken ct) =>
        db.Installations.NewInstallation(this, appVersion, domain, siteName, timeAdded, InstallStatus.Values.InstallPending, true, ct);

    public InstallLocationModel ToModel() => new InstallLocationModel(location.ID, location.QualifiedMachineName);

    public override string ToString() => $"{nameof(EfInstallLocation)} {location.ID}";
}