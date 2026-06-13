namespace XTI_HubWebAppApiActions.Installation;

public sealed class BeginDeleteAction : AppAction<InstallationIDRequest, EmptyActionResult>
{
    private readonly IHubService hubService;
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    public BeginDeleteAction(IHubService hubService, IModifierKeyAccessor modifierKeyAccessor)
    {
        this.hubService = hubService;
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    public async Task<EmptyActionResult> Execute(InstallationIDRequest requestData, CancellationToken stoppingToken)
    {
        await hubService.BeginDelete
        (
            new AppKeyFromPath(modifierKeyAccessor).Value(),
            requestData.InstallationID, 
            stoppingToken
        );
        return new EmptyActionResult();
    }
}