namespace XTI_HubWebAppApi.Command;

partial class CommandGroupBuilder
{
    partial void Configure()
    {
        source.WithModCategory(HubInfo.ModCategories.Apps);
        source.WithAllowed(HubInfo.Roles.InstallationManager);
        GetInstallationCommandDetail
            .ThrottleRequestLogging().For(15).Minutes()
            .ThrottleExceptionLogging().For(5).Minutes();
        GetDeleteCommandDetail
            .ThrottleRequestLogging().For(15).Minutes()
            .ThrottleExceptionLogging().For(5).Minutes();
    }
}
