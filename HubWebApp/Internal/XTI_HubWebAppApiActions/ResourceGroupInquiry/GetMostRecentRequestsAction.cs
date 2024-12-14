namespace XTI_HubWebAppApiActions.ResourceGroupInquiry;

public sealed class GetMostRecentRequestsAction : AppAction<GetResourceGroupLogRequest, AppRequestExpandedModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetMostRecentRequestsAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppRequestExpandedModel[]> Execute(GetResourceGroupLogRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var versionKey = AppVersionKey.Parse(model.VersionKey);
        var version = await app.Version(versionKey);
        var group = await version.ResourceGroup(model.GroupID);
        var requests = await group.MostRecentRequests(model.HowMany);
        return requests.ToArray();
    }
}