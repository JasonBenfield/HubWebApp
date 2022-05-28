namespace XTI_HubAppApi.ResourceInquiry;

public sealed class GetMostRecentErrorEventsAction : AppAction<GetResourceLogRequest, AppLogEntryModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetMostRecentErrorEventsAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppLogEntryModel[]> Execute(GetResourceLogRequest model)
    {
        var app = await appFromPath.Value();
        var versionKey = AppVersionKey.Parse(model.VersionKey);
        var version = await app.Version(versionKey);
        var resource = await version.Resource(model.ResourceID);
        var events = await resource.MostRecentErrorEvents(model.HowMany);
        return events.Select(evt => evt.ToModel()).ToArray();
    }
}