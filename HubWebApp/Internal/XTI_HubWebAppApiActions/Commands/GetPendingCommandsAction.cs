namespace XTI_HubWebAppApiActions.Commands;

public sealed class GetPendingCommandsAction : AppAction<GetPendingCommandsRequest, AppCommandSummaryModel[]>
{
    private readonly IHubService hubService;

    public GetPendingCommandsAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public Task<AppCommandSummaryModel[]> Execute(GetPendingCommandsRequest getRequest, CancellationToken stoppingToken) =>
        hubService.GetPendingCommands
        (
            getRequest.CommandNames.Select(cn => new AppCommandName(cn)).ToArray(),
            getRequest.MachineNames,
            stoppingToken
        );
}
