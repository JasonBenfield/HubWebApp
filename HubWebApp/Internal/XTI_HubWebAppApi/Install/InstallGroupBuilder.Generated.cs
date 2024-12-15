using XTI_HubWebAppApiActions.AppInstall;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Install;
public sealed partial class InstallGroupBuilder
{
    private readonly AppApiGroup source;
    internal InstallGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        AddAdminUser = source.AddAction<AddAdminUserRequest, AppUserModel>("AddAdminUser").WithExecution<AddAdminUserAction>();
        AddInstallationUser = source.AddAction<AddInstallationUserRequest, AppUserModel>("AddInstallationUser").WithExecution<AddInstallationUserAction>();
        AddOrUpdateApps = source.AddAction<AddOrUpdateAppsRequest, AppModel[]>("AddOrUpdateApps").WithExecution<AddOrUpdateAppsAction>().WithValidation<AddOrUpdateAppsValidation>();
        AddOrUpdateVersions = source.AddAction<AddOrUpdateVersionsRequest, EmptyActionResult>("AddOrUpdateVersions").WithExecution<AddOrUpdateVersionsAction>();
        AddSystemUser = source.AddAction<AddSystemUserRequest, AppUserModel>("AddSystemUser").WithExecution<AddSystemUserAction>().WithValidation<AddSystemUserValidation>();
        BeginInstallation = source.AddAction<GetInstallationRequest, EmptyActionResult>("BeginInstallation").WithExecution<BeginInstallationAction>();
        ConfigureInstall = source.AddAction<ConfigureInstallRequest, InstallConfigurationModel>("ConfigureInstall").WithExecution<ConfigureInstallAction>().WithValidation<ConfigureInstallValidation>();
        ConfigureInstallTemplate = source.AddAction<ConfigureInstallTemplateRequest, InstallConfigurationTemplateModel>("ConfigureInstallTemplate").WithExecution<ConfigureInstallTemplateAction>().WithValidation<ConfigureInstallTemplateValidation>();
        DeleteInstallConfiguration = source.AddAction<DeleteInstallConfigurationRequest, EmptyActionResult>("DeleteInstallConfiguration").WithExecution<DeleteInstallConfigurationAction>().WithValidation<DeleteInstallConfigurationValidation>();
        GetInstallConfigurations = source.AddAction<GetInstallConfigurationsRequest, InstallConfigurationModel[]>("GetInstallConfigurations").WithExecution<GetInstallConfigurationsAction>().WithValidation<GetInstallConfigurationsValidation>();
        GetVersion = source.AddAction<GetVersionRequest, XtiVersionModel>("GetVersion").WithExecution<GetVersionAction>();
        GetVersions = source.AddAction<GetVersionsRequest, XtiVersionModel[]>("GetVersions").WithExecution<GetVersionsAction>();
        Installed = source.AddAction<GetInstallationRequest, EmptyActionResult>("Installed").WithExecution<InstalledAction>();
        NewInstallation = source.AddAction<NewInstallationRequest, NewInstallationResult>("NewInstallation").WithExecution<NewInstallationAction>().WithValidation<NewInstallationValidation>();
        RegisterApp = source.AddAction<RegisterAppRequest, AppModel>("RegisterApp").WithExecution<RegisterAppAction>();
        SetUserAccess = source.AddAction<SetUserAccessRequest, EmptyActionResult>("SetUserAccess").WithExecution<SetUserAccessAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<AddAdminUserRequest, AppUserModel> AddAdminUser { get; }
    public AppApiActionBuilder<AddInstallationUserRequest, AppUserModel> AddInstallationUser { get; }
    public AppApiActionBuilder<AddOrUpdateAppsRequest, AppModel[]> AddOrUpdateApps { get; }
    public AppApiActionBuilder<AddOrUpdateVersionsRequest, EmptyActionResult> AddOrUpdateVersions { get; }
    public AppApiActionBuilder<AddSystemUserRequest, AppUserModel> AddSystemUser { get; }
    public AppApiActionBuilder<GetInstallationRequest, EmptyActionResult> BeginInstallation { get; }
    public AppApiActionBuilder<ConfigureInstallRequest, InstallConfigurationModel> ConfigureInstall { get; }
    public AppApiActionBuilder<ConfigureInstallTemplateRequest, InstallConfigurationTemplateModel> ConfigureInstallTemplate { get; }
    public AppApiActionBuilder<DeleteInstallConfigurationRequest, EmptyActionResult> DeleteInstallConfiguration { get; }
    public AppApiActionBuilder<GetInstallConfigurationsRequest, InstallConfigurationModel[]> GetInstallConfigurations { get; }
    public AppApiActionBuilder<GetVersionRequest, XtiVersionModel> GetVersion { get; }
    public AppApiActionBuilder<GetVersionsRequest, XtiVersionModel[]> GetVersions { get; }
    public AppApiActionBuilder<GetInstallationRequest, EmptyActionResult> Installed { get; }
    public AppApiActionBuilder<NewInstallationRequest, NewInstallationResult> NewInstallation { get; }
    public AppApiActionBuilder<RegisterAppRequest, AppModel> RegisterApp { get; }
    public AppApiActionBuilder<SetUserAccessRequest, EmptyActionResult> SetUserAccess { get; }

    public InstallGroup Build() => new InstallGroup(source, this);
}