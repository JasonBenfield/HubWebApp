namespace XTI_HubWebAppApi.AppInstall;

public sealed class AppInstallGroup : AppApiGroupWrapper
{
    public AppInstallGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        RegisterApp = source.AddAction<RegisterAppRequest, AppModel>()
            .Named(nameof(RegisterApp))
            .WithExecution<RegisterAppAction>()
            .Build();
        AddOrUpdateApps = source.AddAction<AddOrUpdateAppsRequest, AppModel[]>()
            .Named(nameof(AddOrUpdateApps))
            .WithExecution<AddOrUpdateAppsAction>()
            .WithValidation<AddOrUpdateAppsValidation>()
            .Build();
        AddOrUpdateVersions = source.AddAction<AddOrUpdateVersionsRequest, EmptyActionResult>()
            .Named(nameof(AddOrUpdateVersions))
            .WithExecution<AddOrUpdateVersionsAction>()
            .Build();
        GetVersion = source.AddAction<GetVersionRequest, XtiVersionModel>()
            .Named(nameof(GetVersion))
            .WithExecution<GetVersionAction>()
            .Build();
        GetVersions = source.AddAction<GetVersionsRequest, XtiVersionModel[]>()
            .Named(nameof(GetVersions))
            .WithExecution<GetVersionsAction>()
            .Build();
        AddSystemUser = source.AddAction<AddSystemUserRequest, AppUserModel>()
            .Named(nameof(AddSystemUser))
            .WithExecution<AddSystemUserAction>()
            .WithValidation<AddSystemUserValidation>()
            .Build();
        AddAdminUser = source.AddAction<AddAdminUserRequest, AppUserModel>()
            .Named(nameof(AddAdminUser))
            .WithExecution<AddAdminUserAction>()
            .Build();
        AddInstallationUser = source.AddAction<AddInstallationUserRequest, AppUserModel>()
            .Named(nameof(AddInstallationUser))
            .WithExecution<AddInstallationUserAction>()
            .Build();
        BeginInstallation = source.AddAction<GetInstallationRequest, EmptyActionResult>()
            .Named(nameof(BeginInstallation))
            .WithExecution<BeginInstallationAction>()
            .Build();
        ConfigureInstallTemplate = source.AddAction<ConfigureInstallTemplateRequest, InstallConfigurationTemplateModel>()
            .Named(nameof(ConfigureInstallTemplate))
            .WithExecution<ConfigureInstallTemplateAction>()
            .WithValidation<ConfigureInstallTemplateValidation>()
            .Build();
        ConfigureInstall = source.AddAction<ConfigureInstallRequest, InstallConfigurationModel>()
            .Named(nameof(ConfigureInstall))
            .WithExecution<ConfigureInstallAction>()
            .WithValidation<ConfigureInstallValidation>()
            .Build();
        DeleteInstallConfiguration = source.AddAction<DeleteInstallConfigurationRequest, EmptyActionResult>()
            .Named(nameof(DeleteInstallConfiguration))
            .WithExecution<DeleteInstallConfigurationAction>()
            .WithValidation<DeleteInstallConfigurationValidation>()
            .Build();
        GetInstallConfigurations = source.AddAction<GetInstallConfigurationsRequest, InstallConfigurationModel[]>()
            .Named(nameof(GetInstallConfigurations))
            .WithExecution<GetInstallConfigurationsAction>()
            .WithValidation<GetInstallConfigurationsValidation>()
            .Build();
        Installed = source.AddAction<GetInstallationRequest, EmptyActionResult>()
            .Named(nameof(Installed))
            .WithExecution<InstalledAction>()
            .Build();
        NewInstallation = source.AddAction<NewInstallationRequest, NewInstallationResult>()
            .Named(nameof(NewInstallation))
            .WithExecution<NewInstallationAction>()
            .WithValidation<NewInstallationValidation>()
            .Build();
        SetUserAccess = source.AddAction<SetUserAccessRequest, EmptyActionResult>()
            .Named(nameof(SetUserAccess))
            .WithExecution<SetUserAccessAction>()
            .Build();
    }

    public AppApiAction<GetInstallationRequest, EmptyActionResult> BeginInstallation { get; }
    public AppApiAction<AddOrUpdateAppsRequest, AppModel[]> AddOrUpdateApps { get; }
    public AppApiAction<AddOrUpdateVersionsRequest, EmptyActionResult> AddOrUpdateVersions { get; }
    public AppApiAction<AddSystemUserRequest, AppUserModel> AddSystemUser { get; }
    public AppApiAction<AddAdminUserRequest, AppUserModel> AddAdminUser { get; }
    public AppApiAction<AddInstallationUserRequest, AppUserModel> AddInstallationUser { get; }
    public AppApiAction<ConfigureInstallTemplateRequest, InstallConfigurationTemplateModel> ConfigureInstallTemplate { get; }
    public AppApiAction<ConfigureInstallRequest, InstallConfigurationModel> ConfigureInstall { get; }
    public AppApiAction<DeleteInstallConfigurationRequest, EmptyActionResult> DeleteInstallConfiguration { get; }
    public AppApiAction<GetInstallConfigurationsRequest, InstallConfigurationModel[]> GetInstallConfigurations { get; }
    public AppApiAction<GetVersionRequest, XtiVersionModel> GetVersion { get; }
    public AppApiAction<GetVersionsRequest, XtiVersionModel[]> GetVersions { get; }
    public AppApiAction<NewInstallationRequest, NewInstallationResult> NewInstallation { get; }
    public AppApiAction<GetInstallationRequest, EmptyActionResult> Installed { get; }
    public AppApiAction<RegisterAppRequest, AppModel> RegisterApp { get; }
    public AppApiAction<SetUserAccessRequest, EmptyActionResult> SetUserAccess { get; }
}