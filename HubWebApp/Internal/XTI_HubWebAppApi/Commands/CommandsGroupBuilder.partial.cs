namespace XTI_HubWebAppApi.Commands;

partial class CommandsGroupBuilder
{
    partial void Configure()
    {
        GetPendingCommands
            .ThrottleRequestLogging().ForOneHour()
            .ThrottleExceptionLogging().For(15).Minutes()
            .WithAllowed(HubInfo.Roles.InstallationManager);
    }
}
