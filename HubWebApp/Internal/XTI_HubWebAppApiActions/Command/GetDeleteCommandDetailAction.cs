namespace XTI_HubWebAppApiActions.Command;

public sealed class GetDeleteCommandDetailAction : AppAction<AppCommandIDRequest, AppDeleteCommandDetailModel>
{
    private readonly IHubService hubService;
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    public GetDeleteCommandDetailAction(IHubService hubService, IModifierKeyAccessor modifierKeyAccessor)
    {
        this.hubService = hubService;
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    public Task<AppDeleteCommandDetailModel> Execute(AppCommandIDRequest requestData, CancellationToken stoppingToken)=>
        hubService.GetDeleteCommandDetail
        (
            new AppKeyFromPath(modifierKeyAccessor).Value(),
            requestData.CommandID, 
            stoppingToken
        );
}
