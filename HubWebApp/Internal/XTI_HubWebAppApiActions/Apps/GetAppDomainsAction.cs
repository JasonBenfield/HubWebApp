namespace XTI_HubWebAppApiActions.AppList;

public sealed class GetAppDomainsAction : AppAction<EmptyRequest, AppDomainModel[]>
{
    private readonly EfHubDB factory;

    public GetAppDomainsAction(EfHubDB factory)
    {
        this.factory = factory;
    }

    public Task<AppDomainModel[]> Execute(EmptyRequest model, CancellationToken stoppingToken) => factory.Installations.AppDomains(stoppingToken);
}
