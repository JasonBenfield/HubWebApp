namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class AddOrUpdateVersionsAction : AppAction<AddOrUpdateVersionsRequest, EmptyActionResult>
{
    private readonly IHubService hubAdmin;

    public AddOrUpdateVersionsAction(IHubService hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public async Task<EmptyActionResult> Execute(AddOrUpdateVersionsRequest addRequest, CancellationToken stoppingToken)
    {
        await hubAdmin.AddOrUpdateVersions
        (
            addRequest.ToAppKeys(), 
            addRequest.Versions,
            stoppingToken
        );
        return new EmptyActionResult();
    }
}
