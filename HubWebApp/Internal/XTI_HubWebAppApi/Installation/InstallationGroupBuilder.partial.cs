namespace XTI_HubWebAppApi.Installation;

partial class InstallationGroupBuilder
{
    partial void Configure()
    {
        source.WithModCategory(HubInfo.ModCategories.Apps);
        source.WithAllowed(HubInfo.Roles.InstallationManager);
        GetInstallationDetail
            .ThrottleRequestLogging().For(15).Minutes()
            .ThrottleExceptionLogging().For(5).Minutes();
    }
}
