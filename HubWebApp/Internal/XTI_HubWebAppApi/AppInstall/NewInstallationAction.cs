namespace XTI_HubWebAppApi.AppInstall;

public sealed class NewInstallationAction : AppAction<NewInstallationRequest, NewInstallationResult>
{
    private readonly IHubAdministration hubAdministration;

    public NewInstallationAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public async Task<NewInstallationResult> Execute(NewInstallationRequest addRequest, CancellationToken stoppingToken)
    {
        var result = await hubAdministration.NewInstallation
        (
            addRequest.ToAppVersionName(),
            addRequest.AppKey.ToAppKey(),
            addRequest.QualifiedMachineName,
            addRequest.Domain,
            addRequest.SiteName,
            stoppingToken
        );
        return result;
    }
}