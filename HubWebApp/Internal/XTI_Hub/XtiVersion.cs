using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class XtiVersion
{
    private readonly AppFactory factory;
    private readonly XtiVersionEntity record;

    internal XtiVersion(AppFactory factory, XtiVersionEntity record)
    {
        this.factory = factory;
        this.record = record ?? new XtiVersionEntity();
        ID = new EntityID(this.record.ID);
    }

    public EntityID ID { get; }

    public AppVersionKey Key() => AppVersionKey.Parse(record.VersionKey);

    public Task Publishing() => factory.Versions.Publishing(record);

    public Task Published() => factory.Versions.Published(record);

    internal AppVersion App(App app) => new AppVersion(factory, app, this);

    public XtiVersionModel ToModel() => new XtiVersionModel
    {
        ID = ID.Value,
        VersionName = new AppVersionName(record.GroupName),
        VersionKey = Key(),
        VersionNumber = new AppVersionNumber(record.Major, record.Minor, record.Patch),
        VersionType = AppVersionType.Values.Value(record.Type),
        Status = AppVersionStatus.Values.Value(record.Status),
        TimeAdded = record.TimeAdded
    };

    public override string ToString() => $"{nameof(XtiVersion)} {ID.Value}: {Key()}";
}