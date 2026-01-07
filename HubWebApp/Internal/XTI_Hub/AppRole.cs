using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppRole
{
    private readonly HubFactory factory;
    private readonly AppRoleEntity record;

    internal AppRole(HubFactory factory, AppRoleEntity record)
    {
        this.factory = factory;
        this.record = record;
        ID = this.record.ID;
    }

    internal int ID { get; }

    public bool IsDeactivated() => record.TimeDeactivated < DateTimeOffset.MaxValue;

    internal Task Deactivate(DateTimeOffset timeDeactivated, CancellationToken ct)
        => UpdateTimeDeactivated(timeDeactivated, ct);

    internal Task Activate(CancellationToken ct) => UpdateTimeDeactivated(DateTimeOffset.MaxValue, ct);

    private Task UpdateTimeDeactivated(DateTimeOffset timeDeactivated, CancellationToken ct) => 
        factory.DB.Roles.Update
        (
            record, 
            r => r.TimeDeactivated = timeDeactivated,
            ct
        );

    public Task<App> App(CancellationToken ct) => factory.Apps.App(record.AppID, ct);

    public bool IsDenyAccess() => NameEquals(AppRoleName.DenyAccess);

    public bool NameEquals(AppRoleName roleName) => GetRoleName().Equals(roleName);

    public AppRoleModel ToModel() => new AppRoleModel
    {
        ID = ID,
        Name = GetRoleName()
    };

    private AppRoleName GetRoleName() => new AppRoleName(record.DisplayText);

    public override string ToString() => $"{nameof(AppRole)} {ID}";
}