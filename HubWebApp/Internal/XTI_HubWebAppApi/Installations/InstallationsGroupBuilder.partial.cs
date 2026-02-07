namespace XTI_HubWebAppApi.Installations;

partial class InstallationsGroupBuilder
{
    partial void Configure()
    {
        source.WithAllowed(HubInfo.Roles.InstallationManager);
        GetInstallationActivities
            .ThrottleRequestLogging().ForOneHour()
            .ThrottleExceptionLogging().For(15).Minutes()
            .WithAllowed(HubInfo.Roles.InstallationManager);
        GetRequestedInstallationDetail
            .ThrottleRequestLogging().For(15).Minutes()
            .ThrottleExceptionLogging().For(5).Minutes();
        GetInstallationDetail
            .ThrottleRequestLogging().For(15).Minutes()
            .ThrottleExceptionLogging().For(5).Minutes();
    }
}
