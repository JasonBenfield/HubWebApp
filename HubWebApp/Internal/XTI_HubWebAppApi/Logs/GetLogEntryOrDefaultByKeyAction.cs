namespace XTI_HubWebAppApi.Logs;

internal sealed class GetLogEntryOrDefaultByKeyAction : AppAction<string, AppLogEntryModel>
{
    private readonly HubFactory hubFactory;

    public GetLogEntryOrDefaultByKeyAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AppLogEntryModel> Execute(string logEntryKey, CancellationToken stoppingToken)
    {
        var logEntry = await hubFactory.LogEntries.LogEntryOrDefaultByKey(logEntryKey);
        return logEntry.ToModel();
    }
}
