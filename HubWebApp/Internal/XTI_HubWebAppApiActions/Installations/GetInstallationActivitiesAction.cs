namespace XTI_HubWebAppApiActions.Installations;

public sealed class GetInstallationActivitiesAction : AppAction<GetInstallationActivitiesRequest, InstallationActivitiesResult>
{
    private readonly EfHubDB hubFactory;

    public GetInstallationActivitiesAction(EfHubDB hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<InstallationActivitiesResult> Execute(GetInstallationActivitiesRequest getRequest, CancellationToken stoppingToken)
    {
        var installationRequests = new List<AppVersionInstallationModel>();
        var deletionRequests = new List<AppVersionInstallationModel>();
        foreach (var machineName in getRequest.MachineNames)
        {
            var efInstallations = await hubFactory.Installations.GetPendingDeletes(machineName, stoppingToken);
            foreach (var efInstallation in efInstallations)
            {
                var efAppVersion = await efInstallation.AppVersion(stoppingToken);
                deletionRequests.Add
                (
                    new AppVersionInstallationModel
                    (
                        efAppVersion.App.ToModel(),
                        efAppVersion.Version.ToModel(),
                        efInstallation.ToModel()
                    )
                );
            }
        }
        return new InstallationActivitiesResult
        (
            Deletions: deletionRequests.ToArray(),
            Installations: installationRequests.ToArray()
        );
    }
}
