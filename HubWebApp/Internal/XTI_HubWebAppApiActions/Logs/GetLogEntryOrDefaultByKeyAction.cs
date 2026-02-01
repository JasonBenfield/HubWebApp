namespace XTI_HubWebAppApiActions.Logs;

public sealed class GetLogEntryOrDefaultByKeyAction : AppAction<string, AppLogEntryModel>
{
    private readonly EfHubDB hubFactory;

    public GetLogEntryOrDefaultByKeyAction(EfHubDB hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AppLogEntryModel> Execute(string logEntryKey, CancellationToken stoppingToken)
    {
        var logEntry = await hubFactory.LogEntries.LogEntryOrDefaultByKey(logEntryKey, stoppingToken);
        return logEntry.ToModel();
    }
}
