namespace XTI_HubWebAppApi.Installations;

partial class InstallationsGroupBuilder
{
    partial void Configure()
    {
        BeginDelete.WithAllowed(HubInfo.Roles.InstallationManager);
        Deleted.WithAllowed(HubInfo.Roles.InstallationManager);
        GetInstallationActivities
            .ThrottleRequestLogging().ForOneHour()
            .ThrottleExceptionLogging().For(15).Minutes()
            .WithAllowed(HubInfo.Roles.InstallationManager);
    }
}
