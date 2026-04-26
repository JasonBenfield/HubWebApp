namespace XTI_HubWebAppApiActions.Commands;

public sealed class GetCommandsInProgressAction : AppAction<EmptyRequest, AppCommandSummaryModel[]>
{
    private readonly EfHubDB db;

    public GetCommandsInProgressAction(EfHubDB db)
    {
        this.db = db;
    }

    public async Task<AppCommandSummaryModel[]> Execute(EmptyRequest requestData, CancellationToken stoppingToken)
    {
        var efCommands = await db.AppCommands.GetPendingCommands(stoppingToken);
        var commandSummaries = new List<AppCommandSummaryModel>();
        foreach (var efCommand in efCommands)
        {
            var commandSummary = await efCommand.ToSummaryModel(stoppingToken);
            commandSummaries.Add(commandSummary);
        }
        return commandSummaries.ToArray();
    }
}
