namespace XTI_HubWebAppApiActions.Installation;

public sealed class InstalledAction : AppAction<InstallationIDRequest, EmptyActionResult>
{
    private readonly IHubService hubAdministration;
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    public InstalledAction(IHubService hubAdministration, IModifierKeyAccessor modifierKeyAccessor)
    {
        this.hubAdministration = hubAdministration;
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    public async Task<EmptyActionResult> Execute(InstallationIDRequest requestData, CancellationToken stoppingToken)
    {
        await hubAdministration.Installed
        (
            new AppKeyFromPath(modifierKeyAccessor).Value(),
            requestData.InstallationID, 
            stoppingToken
        );
        return new EmptyActionResult();
    }
}