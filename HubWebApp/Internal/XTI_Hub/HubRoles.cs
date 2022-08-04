using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class HubRoles
{
    internal static HubRoles Instance = new HubRoles();

    public AppRoleName Admin { get; } = AppRoleName.Admin;
    public AppRoleName ViewApp { get; } = new AppRoleName(nameof(ViewApp));
    public AppRoleName AddUserGroup { get; } = new AppRoleName(nameof(AddUserGroup));
    public AppRoleName AddUser { get; } = new AppRoleName(nameof(AddUser));
    public AppRoleName EditUser { get; } = new AppRoleName(nameof(EditUser));
    public AppRoleName ViewUser { get; } = new AppRoleName(nameof(ViewUser));
    public AppRoleName Authenticator { get; } = new AppRoleName(nameof(Authenticator));
    public AppRoleName PermanentLog { get; } = new AppRoleName(nameof(PermanentLog));
    public AppRoleName AddStoredObject { get; } = new AppRoleName(nameof(AddStoredObject));
}