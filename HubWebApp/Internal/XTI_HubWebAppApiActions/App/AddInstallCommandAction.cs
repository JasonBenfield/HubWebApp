namespace XTI_HubWebAppApiActions.App;

public sealed class AddInstallCommandAction : AppAction<AddInstallCommandRequest, AppInstallCommandDetailModel>
{
    private readonly IHubService hubService;
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    public AddInstallCommandAction(IHubService hubService, IModifierKeyAccessor modifierKeyAccessor)
    {
        this.hubService = hubService;
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    public Task<AppInstallCommandDetailModel> Execute(AddInstallCommandRequest requestData, CancellationToken stoppingToken) =>
        hubService.AddInstallCommand
        (
            new AppKeyFromPath(modifierKeyAccessor).Value(),
            requestData,
            stoppingToken
        );
}