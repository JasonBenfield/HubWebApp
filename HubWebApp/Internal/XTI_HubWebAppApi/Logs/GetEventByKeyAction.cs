namespace XTI_HubWebAppApi.Logs;

internal sealed class GetEventByKeyAction : AppAction<string, AppLogEntryModel>
{
    private readonly HubFactory hubFactory;

    public GetEventByKeyAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AppLogEntryModel> Execute(string logEntryKey, CancellationToken stoppingToken)
    {
        var logEntry = await hubFactory.LogEntries.LogEntryByKey(logEntryKey);
        return logEntry.ToModel();
    }
}
