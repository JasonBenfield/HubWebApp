namespace XTI_HubWebAppApiActions.AppInquiry;

public sealed class GetAppAction : AppAction<EmptyRequest, AppModel>
{
    private readonly AppFromPath appFromPath;

    public GetAppAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppModel> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        return app.ToModel();
    }
}