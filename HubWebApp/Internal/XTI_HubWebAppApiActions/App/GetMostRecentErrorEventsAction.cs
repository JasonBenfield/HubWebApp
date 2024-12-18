namespace XTI_HubWebAppApiActions.AppInquiry;

public sealed class GetMostRecentErrorEventsAction : AppAction<int, AppLogEntryModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetMostRecentErrorEventsAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppLogEntryModel[]> Execute(int howMany, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var events = await app.MostRecentErrorLogEntries(howMany);
        return events.Select(evt => evt.ToModel()).ToArray();
    }
}