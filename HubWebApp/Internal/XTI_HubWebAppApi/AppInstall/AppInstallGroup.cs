namespace XTI_HubWebAppApi.AppInstall;

public sealed class AppInstallGroup : AppApiGroupWrapper
{
    public AppInstallGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        RegisterApp = source.AddAction
        (
            nameof(RegisterApp), () => sp.GetRequiredService<RegisterAppAction>()
        );
        AddOrUpdateApps = source.AddAction
        (
            nameof(AddOrUpdateApps),
            () => sp.GetRequiredService<AddOrUpdateAppsAction>(),
                () => sp.GetRequiredService<AddOrUpdateAppsValidation>()
        );
        AddOrUpdateVersions = source.AddAction
        (
            nameof(AddOrUpdateVersions), () => sp.GetRequiredService<AddOrUpdateVersionsAction>()
        );
        GetVersion = source.AddAction
        (
            nameof(GetVersion), () => sp.GetRequiredService<GetVersionAction>()
        );
        GetVersions = source.AddAction
        (
            nameof(GetVersions), () => sp.GetRequiredService<GetVersionsAction>()
        );
        AddSystemUser = source.AddAction
        (
            nameof(AddSystemUser),
            () => sp.GetRequiredService<AddSystemUserAction>(),
            () => sp.GetRequiredService<AddSystemUserValidation>()
        );
        AddAdminUser = source.AddAction
        (
            nameof(AddAdminUser),
            () => sp.GetRequiredService<AddAdminUserAction>()
        );
        AddInstallationUser = source.AddAction
        (
            nameof(AddInstallationUser),
            () => sp.GetRequiredService<AddInstallationUserAction>()
        );
        NewInstallation = source.AddAction
        (
            nameof(NewInstallation),
            () => sp.GetRequiredService<NewInstallationAction>(),
            () => sp.GetRequiredService<NewInstallationValidation>()
        );
        BeginInstallation = source.AddAction
        (
            nameof(BeginInstallation), () => sp.GetRequiredService<BeginInstallationAction>()
        );
        Installed = source.AddAction
        (
            nameof(Installed), () => sp.GetRequiredService<InstalledAction>()
        );
        SetUserAccess = source.AddAction
        (
            nameof(SetUserAccess), () => sp.GetRequiredService<SetUserAccessAction>()
        );
    }

    public AppApiAction<RegisterAppRequest, AppModel> RegisterApp { get; }
    public AppApiAction<GetVersionRequest, XtiVersionModel> GetVersion { get; }
    public AppApiAction<GetVersionsRequest, XtiVersionModel[]> GetVersions { get; }
    public AppApiAction<AddOrUpdateAppsRequest, AppModel[]> AddOrUpdateApps { get; }
    public AppApiAction<AddOrUpdateVersionsRequest, EmptyActionResult> AddOrUpdateVersions { get; }
    public AppApiAction<AddSystemUserRequest, AppUserModel> AddSystemUser { get; }
    public AppApiAction<AddAdminUserRequest, AppUserModel> AddAdminUser { get; }
    public AppApiAction<AddInstallationUserRequest, AppUserModel> AddInstallationUser { get; }
    public AppApiAction<NewInstallationRequest, NewInstallationResult> NewInstallation { get; }
    public AppApiAction<InstallationRequest, EmptyActionResult> BeginInstallation { get; }
    public AppApiAction<InstallationRequest, EmptyActionResult> Installed { get; }
    public AppApiAction<SetUserAccessRequest, EmptyActionResult> SetUserAccess { get; }
}