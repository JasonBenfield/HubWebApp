namespace XTI_HubWebAppApi.Logs;

internal sealed class GetLogEntryByKeyAction : AppAction<string, AppLogEntryModel>
{
    private readonly HubFactory hubFactory;

    public GetLogEntryByKeyAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AppLogEntryModel> Execute(string logEntryKey, CancellationToken stoppingToken)
    {
        var logEntry = await hubFactory.LogEntries.LogEntryByKey(logEntryKey);
        return logEntry.ToModel();
    }
}
