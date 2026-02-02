using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAppRole
{
    private readonly EfHubDB db;
    private readonly AppRoleEntity role;

    internal EfAppRole(EfHubDB db, AppRoleEntity role)
    {
        this.db = db;
        this.role = role;
    }

    internal int ID { get => role.ID; }

    public bool IsDeactivated() => role.TimeDeactivated < DateTimeOffset.MaxValue;

    internal Task Deactivate(DateTimeOffset timeDeactivated, CancellationToken ct) =>
        UpdateTimeDeactivated(timeDeactivated, ct);

    internal Task Activate(CancellationToken ct) => UpdateTimeDeactivated(DateTimeOffset.MaxValue, ct);

    private Task UpdateTimeDeactivated(DateTimeOffset timeDeactivated, CancellationToken ct) =>
        db.Context.Roles.Update
        (
            role,
            r => r.TimeDeactivated = timeDeactivated,
            ct
        );

    public Task<EfApp> App(CancellationToken ct) => db.Apps.App(role.AppID, ct);

    public bool IsDenyAccess() => NameEquals(AppRoleName.DenyAccess);

    public bool NameEquals(AppRoleName roleName) => GetRoleName().Equals(roleName);

    public AppRoleModel ToModel() => new AppRoleModel
    {
        ID = ID,
        Name = GetRoleName()
    };

    private AppRoleName GetRoleName() => new AppRoleName(role.DisplayText);

    public override string ToString() => $"{nameof(EfAppRole)} {ID}";
}