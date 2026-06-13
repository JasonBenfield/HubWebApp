namespace XTI_HubWebAppApiActions.Command;

public sealed class BeginCommandStepAction : AppAction<BeginAppCommandStepRequest, AppCommandStepModel>
{
    private readonly IHubService hubService;
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    public BeginCommandStepAction(IHubService hubService, IModifierKeyAccessor modifierKeyAccessor)
    {
        this.hubService = hubService;
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    public Task<AppCommandStepModel> Execute(BeginAppCommandStepRequest requestData, CancellationToken stoppingToken) =>
        hubService.BeginCommandStep
        (
            new AppKeyFromPath(modifierKeyAccessor).Value(),
            requestData.CommandID, 
            requestData.Activity, 
            stoppingToken
        );
}
