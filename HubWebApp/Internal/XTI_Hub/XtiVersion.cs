using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class XtiVersion
{
    private readonly HubFactory factory;
    private readonly XtiVersionEntity record;

    internal XtiVersion(HubFactory factory, XtiVersionEntity record)
    {
        this.factory = factory;
        this.record = record ?? new XtiVersionEntity();
        ID = this.record.ID;
    }

    public int ID { get; }

    public AppVersionKey Key() => AppVersionKey.Parse(record.VersionKey);

    public Task Publishing() => factory.Versions.Publishing(record);

    public Task Published() => factory.Versions.Published(record);

    internal AppVersion App(App app) => new AppVersion(factory, app, this);

    public XtiVersionModel ToModel() => new XtiVersionModel
    {
        ID = ID,
        VersionName = new AppVersionName(record.VersionName),
        VersionKey = Key(),
        VersionNumber = new AppVersionNumber(record.Major, record.Minor, record.Patch),
        VersionType = AppVersionType.Values.Value(record.Type),
        Status = AppVersionStatus.Values.Value(record.Status),
        TimeAdded = record.TimeAdded
    };

    public override string ToString() => $"{nameof(XtiVersion)} {ID}: {Key()}";
}