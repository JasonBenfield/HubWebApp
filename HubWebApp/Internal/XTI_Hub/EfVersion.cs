using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfVersion
{
    private readonly EfHubDB db;
    private readonly XtiVersionEntity version;

    internal EfVersion(EfHubDB db, XtiVersionEntity version)
    {
        this.db = db;
        this.version = version ?? new XtiVersionEntity();
    }

    public int ID { get => version.ID; }

    public AppVersionKey Key() => AppVersionKey.Parse(version.VersionKey);

    public Task Publishing(CancellationToken ct) => db.Versions.Publishing(version, ct);

    public Task Published(CancellationToken ct) => db.Versions.Published(version, ct);

    internal EfAppVersion App(EfApp app) => new EfAppVersion(db, app, this);

    public XtiVersionModel ToModel() => new XtiVersionModel
    {
        ID = ID,
        VersionName = new AppVersionName(version.VersionName),
        VersionKey = Key(),
        VersionNumber = new AppVersionNumber(version.Major, version.Minor, version.Patch),
        VersionType = AppVersionType.Values.Value(version.Type),
        Status = AppVersionStatus.Values.Value(version.Status),
        TimeAdded = version.TimeAdded
    };

    public override string ToString() => $"{nameof(EfVersion)} {ID}: {Key()}";
}