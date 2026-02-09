namespace XTI_HubWebAppApi.Installations;

partial class InstallationsGroupBuilder
{
    partial void Configure()
    {
        source.WithAllowed(HubInfo.Roles.InstallationManager);
        GetPendingCommands
            .ThrottleRequestLogging().ForOneHour()
            .ThrottleExceptionLogging().For(15).Minutes()
            .WithAllowed(HubInfo.Roles.InstallationManager);
        GetInstallationCommandDetail
            .ThrottleRequestLogging().For(15).Minutes()
            .ThrottleExceptionLogging().For(5).Minutes();
        GetDeleteCommandDetail
            .ThrottleRequestLogging().For(15).Minutes()
            .ThrottleExceptionLogging().For(5).Minutes();
        GetInstallationDetail
            .ThrottleRequestLogging().For(15).Minutes()
            .ThrottleExceptionLogging().For(5).Minutes();
    }
}
