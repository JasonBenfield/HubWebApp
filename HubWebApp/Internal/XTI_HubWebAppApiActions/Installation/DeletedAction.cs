namespace XTI_HubWebAppApiActions.Installation;

public sealed class DeletedAction : AppAction<InstallationIDRequest, EmptyActionResult>
{
    private readonly IHubService hubService;
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    public DeletedAction(IHubService hubService, IModifierKeyAccessor modifierKeyAccessor)
    {
        this.hubService = hubService;
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    public async Task<EmptyActionResult> Execute(InstallationIDRequest requestData, CancellationToken stoppingToken)
    {
        await hubService.Deleted
        (
            new AppKeyFromPath(modifierKeyAccessor).Value(),
            requestData.InstallationID, 
            stoppingToken
        );
        return new EmptyActionResult();
    }
}