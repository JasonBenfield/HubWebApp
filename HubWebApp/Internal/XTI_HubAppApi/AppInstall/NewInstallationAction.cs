namespace XTI_HubAppApi.AppInstall;

public sealed class NewInstallationAction : AppAction<NewInstallationRequest, NewInstallationResult>
{
    private readonly IHubAdministration hubAdministration;

    public NewInstallationAction(IHubAdministration hubAdministration)
    {
        this.hubAdministration = hubAdministration;
    }

    public async Task<NewInstallationResult> Execute(NewInstallationRequest model, CancellationToken stoppingToken)
    {
        var result = await hubAdministration.NewInstallation
        (
            model.VersionName,
            model.AppKey,
            model.QualifiedMachineName,
            model.Domain
        );
        return result;
    }
}