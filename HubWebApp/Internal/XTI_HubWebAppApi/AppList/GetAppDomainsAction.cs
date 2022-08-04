namespace XTI_HubWebAppApi.AppList;

internal sealed class GetAppDomainsAction : AppAction<EmptyRequest, AppDomainModel[]>
{
    private readonly HubFactory factory;

    public GetAppDomainsAction(HubFactory factory)
    {
        this.factory = factory;
    }

    public Task<AppDomainModel[]> Execute(EmptyRequest model, CancellationToken stoppingToken) => factory.Installations.AppDomains();
}
