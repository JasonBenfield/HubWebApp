namespace XTI_HubWebAppApiActions.Command;

public sealed class CommandEndedAction : AppAction<AppCommandIDRequest, EmptyActionResult>
{
    private readonly IHubService hubService;
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    public CommandEndedAction(IHubService hubService, IModifierKeyAccessor modifierKeyAccessor)
    {
        this.hubService = hubService;
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    public async Task<EmptyActionResult> Execute(AppCommandIDRequest requestData, CancellationToken stoppingToken)
    {
        await hubService.CommandEnded
        (
            new AppKeyFromPath(modifierKeyAccessor).Value(),
            requestData.CommandID, 
            stoppingToken
        );
        return new EmptyActionResult();
    }
}
