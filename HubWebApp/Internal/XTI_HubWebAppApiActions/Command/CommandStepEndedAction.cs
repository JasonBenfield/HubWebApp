namespace XTI_HubWebAppApiActions.Command;

public sealed class CommandStepEndedAction : AppAction<AppCommandStepEndedRequest, EmptyRequest>
{
    private readonly IHubService hubService;
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    public CommandStepEndedAction(IHubService hubService, IModifierKeyAccessor modifierKeyAccessor)
    {
        this.hubService = hubService;
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    public async Task<EmptyRequest> Execute(AppCommandStepEndedRequest requestData, CancellationToken stoppingToken)
    {
        await hubService.CommandStepEnded
        (
            new AppKeyFromPath(modifierKeyAccessor).Value(),
            requestData.StepID, 
            requestData.ErrorMessage, 
            stoppingToken
        );
        return new EmptyRequest();
    }
}
