using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.ResourceGroupInquiry;

public sealed class GetMostRecentErrorEventsAction : AppAction<GetResourceGroupLogRequest, AppEventModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetMostRecentErrorEventsAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppEventModel[]> Execute(GetResourceGroupLogRequest model)
    {
        var app = await appFromPath.Value();
        var versionKey = AppVersionKey.Parse(model.VersionKey);
        var version = await app.Version(versionKey);
        var group = await version.ResourceGroup(model.GroupID);
        var events = await group.MostRecentErrorEvents(model.HowMany);
        return events.Select(evt => evt.ToModel()).ToArray();
    }
}