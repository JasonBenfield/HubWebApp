namespace XTI_HubWebAppApi.PermanentLog;

partial class PermanentLogGroupBuilder
{
    partial void Configure()
    {
        source.WithAllowed(HubInfo.Roles.PermanentLog);
        LogSessionDetails
            .ThrottleRequestLogging().ForOneHour()
            .ThrottleExceptionLogging().For(5).Minutes();
    }
}
