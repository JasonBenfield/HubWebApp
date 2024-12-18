using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class HubRoles
{
    internal static HubRoles Instance = new();

    public HubRoles()
    {
        AppViewerRoles = [Admin, EditApp, ViewApp];
        AppEditorRoles = [Admin, EditApp];
        UserViewerRoles = [Admin, EditUser, ViewUser];
        UserEditorRoles = [Admin, EditUser];
    }

    public AppRoleName Admin { get; } = AppRoleName.Admin;
    public AppRoleName System { get; } = AppRoleName.System;
    public AppRoleName ViewApp { get; } = new(nameof(ViewApp));
    public AppRoleName EditApp { get; } = new(nameof(EditApp));
    public AppRoleName AddUserGroup { get; } = new(nameof(AddUserGroup));
    public AppRoleName AddUser { get; } = new(nameof(AddUser));
    public AppRoleName EditUser { get; } = new(nameof(EditUser));
    public AppRoleName ViewUser { get; } = new(nameof(ViewUser));
    public AppRoleName Authenticator { get; } = new(nameof(Authenticator));
    public AppRoleName PermanentLog { get; } = new(nameof(PermanentLog));
    public AppRoleName ViewLog { get; } = new(nameof(ViewLog));
    public AppRoleName InstallationManager { get; } = new(nameof(InstallationManager));
    public AppRoleName AddStoredObject { get; } = new(nameof(AddStoredObject));

    public AppRoleName[] AppViewerRoles { get; }
    public AppRoleName[] AppEditorRoles { get; }
    public AppRoleName[] UserViewerRoles { get; }
    public AppRoleName[] UserEditorRoles { get; }
}