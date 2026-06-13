namespace XTI_HubWebAppApi.App;

partial class AppGroupBuilder
{
    partial void Configure()
    {
        source
            .WithModCategory(HubInfo.ModCategories.Apps)
            .ResetAccessWithAllowed(HubInfo.Roles.AppViewerRoles);
        ConfigureInstall
            .ResetAccessWithAllowed(HubInfo.Roles.InstallRoles);
        DeleteInstallConfiguration
            .ResetAccessWithAllowed(HubInfo.Roles.InstallRoles);
        GetInstallConfigurations
            .ResetAccessWithAllowed(HubInfo.Roles.InstallRoles);
        UpdateVersionsFromPublished
            .ResetAccessWithAllowed(HubInfo.Roles.InstallRoles);
    }
}
