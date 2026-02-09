namespace XTI_HubWebAppApiActions.Installations;

public sealed class GetPendingCommandsAction : AppAction<GetPendingCommandsRequest, AppCommandModel[]>
{
    private readonly IHubService hubService;

    public GetPendingCommandsAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public Task<AppCommandModel[]> Execute(GetPendingCommandsRequest getRequest, CancellationToken stoppingToken) =>
        hubService.GetPendingCommands
        (
            getRequest.CommandNames.Select(cn => new AppCommandName(cn)).ToArray(),
            getRequest.MachineNames,
            stoppingToken
        );
}
