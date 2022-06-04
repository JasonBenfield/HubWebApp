namespace XTI_HubAppApi.ResourceGroupInquiry;

public sealed class GetMostRecentErrorEventsAction : AppAction<GetResourceGroupLogRequest, AppLogEntryModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetMostRecentErrorEventsAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppLogEntryModel[]> Execute(GetResourceGroupLogRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var versionKey = AppVersionKey.Parse(model.VersionKey);
        var version = await app.Version(versionKey);
        var group = await version.ResourceGroup(model.GroupID);
        var events = await group.MostRecentErrorEvents(model.HowMany);
        return events.Select(evt => evt.ToModel()).ToArray();
    }
}