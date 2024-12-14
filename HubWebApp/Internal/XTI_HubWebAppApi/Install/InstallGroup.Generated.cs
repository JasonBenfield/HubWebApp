using XTI_HubWebAppApiActions.AppInstall;

// Generated Code
namespace XTI_HubWebAppApi.Install;
public sealed partial class InstallGroup : AppApiGroupWrapper
{
    internal InstallGroup(AppApiGroup source, InstallGroupBuilder builder) : base(source)
    {
        AddAdminUser = builder.AddAdminUser.Build();
        AddInstallationUser = builder.AddInstallationUser.Build();
        AddOrUpdateApps = builder.AddOrUpdateApps.Build();
        AddOrUpdateVersions = builder.AddOrUpdateVersions.Build();
        AddSystemUser = builder.AddSystemUser.Build();
        BeginInstallation = builder.BeginInstallation.Build();
        ConfigureInstall = builder.ConfigureInstall.Build();
        ConfigureInstallTemplate = builder.ConfigureInstallTemplate.Build();
        DeleteInstallConfiguration = builder.DeleteInstallConfiguration.Build();
        GetInstallConfigurations = builder.GetInstallConfigurations.Build();
        GetVersion = builder.GetVersion.Build();
        GetVersions = builder.GetVersions.Build();
        Installed = builder.Installed.Build();
        NewInstallation = builder.NewInstallation.Build();
        RegisterApp = builder.RegisterApp.Build();
        SetUserAccess = builder.SetUserAccess.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<AddAdminUserRequest, AppUserModel> AddAdminUser { get; }
    public AppApiAction<AddInstallationUserRequest, AppUserModel> AddInstallationUser { get; }
    public AppApiAction<AddOrUpdateAppsRequest, AppModel[]> AddOrUpdateApps { get; }
    public AppApiAction<AddOrUpdateVersionsRequest, EmptyActionResult> AddOrUpdateVersions { get; }
    public AppApiAction<AddSystemUserRequest, AppUserModel> AddSystemUser { get; }
    public AppApiAction<GetInstallationRequest, EmptyActionResult> BeginInstallation { get; }
    public AppApiAction<ConfigureInstallRequest, InstallConfigurationModel> ConfigureInstall { get; }
    public AppApiAction<ConfigureInstallTemplateRequest, InstallConfigurationTemplateModel> ConfigureInstallTemplate { get; }
    public AppApiAction<DeleteInstallConfigurationRequest, EmptyActionResult> DeleteInstallConfiguration { get; }
    public AppApiAction<GetInstallConfigurationsRequest, InstallConfigurationModel[]> GetInstallConfigurations { get; }
    public AppApiAction<GetVersionRequest, XtiVersionModel> GetVersion { get; }
    public AppApiAction<GetVersionsRequest, XtiVersionModel[]> GetVersions { get; }
    public AppApiAction<GetInstallationRequest, EmptyActionResult> Installed { get; }
    public AppApiAction<NewInstallationRequest, NewInstallationResult> NewInstallation { get; }
    public AppApiAction<RegisterAppRequest, AppModel> RegisterApp { get; }
    public AppApiAction<SetUserAccessRequest, EmptyActionResult> SetUserAccess { get; }
}