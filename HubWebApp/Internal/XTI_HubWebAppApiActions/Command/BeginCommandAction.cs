namespace XTI_HubWebAppApiActions.Command;

public sealed class BeginCommandAction : AppAction<AppCommandIDRequest, AppCommandModel>
{
    private readonly IHubService hubService;
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    public BeginCommandAction(IHubService hubService, IModifierKeyAccessor modifierKeyAccessor)
    {
        this.hubService = hubService;
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    public Task<AppCommandModel> Execute(AppCommandIDRequest requestData, CancellationToken stoppingToken) =>
        hubService.BeginCommand
        (
            new AppKeyFromPath(modifierKeyAccessor).Value(), 
            requestData.CommandID, 
            stoppingToken
        );
}
