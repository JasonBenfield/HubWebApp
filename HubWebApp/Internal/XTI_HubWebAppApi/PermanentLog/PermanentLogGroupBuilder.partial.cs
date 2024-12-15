namespace XTI_HubWebAppApi.PermanentLog;

partial class PermanentLogGroupBuilder
{
    partial void Configure()
    {
        source.WithAllowed(HubInfo.Roles.PermanentLog);
        LogBatch
            .ThrottleRequestLogging().ForOneHour()
            .ThrottleExceptionLogging().For(5).Minutes();
        LogSessionDetails
            .ThrottleRequestLogging().ForOneHour()
            .ThrottleExceptionLogging().For(5).Minutes();
    }
}
