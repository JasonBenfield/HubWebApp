namespace XTI_HubWebAppApi.Installations;

public sealed class DeletedAction : AppAction<GetInstallationRequest, EmptyActionResult>
{
    private readonly HubFactory hubFactory;

    public DeletedAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<EmptyActionResult> Execute(GetInstallationRequest model, CancellationToken stoppingToken)
    {
        var installation = await hubFactory.Installations.InstallationOrDefault(model.InstallationID);
        await installation.Deleted();
        return new EmptyActionResult();
    }
}