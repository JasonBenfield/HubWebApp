namespace XTI_HubWebAppApiActions.ResourceInquiry;

public sealed class GetMostRecentRequestsAction : AppAction<GetResourceLogRequest, AppRequestExpandedModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetMostRecentRequestsAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppRequestExpandedModel[]> Execute(GetResourceLogRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var versionKey = AppVersionKey.Parse(model.VersionKey);
        var version = await app.Version(versionKey);
        var resource = await version.Resource(model.ResourceID);
        var requests = await resource.MostRecentRequests(model.HowMany);
        return requests.ToArray();
    }
}