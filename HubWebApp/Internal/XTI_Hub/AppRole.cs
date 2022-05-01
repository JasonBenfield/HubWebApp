using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppRole : IAppRole
{
    private readonly HubFactory factory;
    private readonly AppRoleEntity record;

    internal AppRole(HubFactory factory, AppRoleEntity record)
    {
        this.factory = factory;
        this.record = record ?? new AppRoleEntity();
        ID = this.record.ID;
    }

    public int ID { get; }
    public AppRoleName Name() => new AppRoleName(record.Name);

    public bool IsDeactivated() => record.TimeDeactivated < DateTimeOffset.MaxValue;

    internal Task Deactivate(DateTimeOffset timeDeactivated)
        => updateTimeDeactivated(timeDeactivated);

    internal Task Activate() => updateTimeDeactivated(DateTimeOffset.MaxValue);

    private Task updateTimeDeactivated(DateTimeOffset timeDeactivated)
        => factory.DB.Roles.Update(record, r => r.TimeDeactivated = timeDeactivated);

    internal Task<App> App() => factory.Apps.App(record.AppID);

    public AppRoleModel ToModel() => new AppRoleModel
    {
        ID = ID,
        Name = Name().DisplayText
    };

    public override string ToString() => $"{nameof(AppRole)} {ID}";
}