using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class HubRoles
{
    internal static HubRoles Instance = new();

    public AppRoleName Admin { get; } = AppRoleName.Admin;
    public AppRoleName ViewApp { get; } = new(nameof(ViewApp));
    public AppRoleName AddUserGroup { get; } = new(nameof(AddUserGroup));
    public AppRoleName AddUser { get; } = new(nameof(AddUser));
    public AppRoleName EditUser { get; } = new(nameof(EditUser));
    public AppRoleName ViewUser { get; } = new(nameof(ViewUser));
    public AppRoleName Authenticator { get; } = new(nameof(Authenticator));
    public AppRoleName PermanentLog { get; } = new(nameof(PermanentLog));
    public AppRoleName ViewLog { get; } = new(nameof(ViewLog));
    public AppRoleName InstallationManager { get; } = new(nameof(InstallationManager));
    public AppRoleName AddStoredObject { get; } = new(nameof(AddStoredObject));
}