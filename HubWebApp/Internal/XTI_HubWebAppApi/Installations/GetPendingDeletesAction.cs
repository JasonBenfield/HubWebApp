namespace XTI_HubWebAppApi.Installations;

internal sealed class GetPendingDeletesAction : AppAction<GetPendingDeletesRequest, InstallationModel[]>
{
    private readonly HubFactory hubFactory;

    public GetPendingDeletesAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<InstallationModel[]> Execute(GetPendingDeletesRequest model, CancellationToken stoppingToken)
    {
        var installations = await hubFactory.Installations.GetPendingDeletes(model.MachineName);
        return installations.Select(inst => inst.ToModel()).ToArray();
    }
}
