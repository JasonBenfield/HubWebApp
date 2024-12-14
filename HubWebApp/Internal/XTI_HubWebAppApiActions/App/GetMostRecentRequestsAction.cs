namespace XTI_HubWebAppApiActions.AppInquiry;

public sealed class GetMostRecentRequestsAction : AppAction<int, AppRequestExpandedModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetMostRecentRequestsAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppRequestExpandedModel[]> Execute(int howMany, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var requests = await app.MostRecentRequests(howMany);
        return requests.ToArray();
    }
}