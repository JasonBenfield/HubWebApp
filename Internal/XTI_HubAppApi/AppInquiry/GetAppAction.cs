using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppInquiry;

public sealed class GetAppAction : AppAction<EmptyRequest, AppModel>
{
    private readonly AppFromPath appFromPath;

    public GetAppAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppModel> Execute(EmptyRequest model)
    {
        var app = await appFromPath.Value();
        return app.ToAppModel();
    }
}