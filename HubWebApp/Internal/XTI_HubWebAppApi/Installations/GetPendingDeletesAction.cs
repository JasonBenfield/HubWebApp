namespace XTI_HubWebAppApi.Installations;

internal sealed class GetPendingDeletesAction : AppAction<GetPendingDeletesRequest, AppVersionInstallationModel[]>
{
    private readonly HubFactory hubFactory;

    public GetPendingDeletesAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AppVersionInstallationModel[]> Execute(GetPendingDeletesRequest getRequest, CancellationToken stoppingToken)
    {
        var appVersionInstallations = new List<AppVersionInstallationModel>();
        foreach(var machineName in getRequest.MachineNames)
        {
            var installations = await hubFactory.Installations.GetPendingDeletes(machineName);
            foreach (var installation in installations)
            {
                var appVersion = await installation.AppVersion();
                appVersionInstallations.Add
                (
                    new AppVersionInstallationModel
                    (
                        appVersion.App.ToModel(),
                        appVersion.Version.ToModel(),
                        installation.ToModel()
                    )
                );
            }
        }
        return appVersionInstallations.ToArray();
    }
}
