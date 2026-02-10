namespace XTI_HubWebAppApiActions.Command;

public sealed class GetCommandAction : AppAction<AppCommandIDRequest, AppCommandModel>
{
    private readonly AppFromPath appFromPath;

    public GetCommandAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppCommandModel> Execute(AppCommandIDRequest requestData, CancellationToken stoppingToken)
    {
        var efApp = await appFromPath.Value(stoppingToken);
        var efCommand = await efApp.Command(requestData.CommandID, stoppingToken);
        return efCommand.ToModel();
    }
}
