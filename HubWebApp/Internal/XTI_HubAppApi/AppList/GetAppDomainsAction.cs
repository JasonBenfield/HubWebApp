using XTI_App.Api;
using XTI_Hub;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppList;

internal sealed class GetAppDomainsAction : AppAction<EmptyRequest, AppDomainModel[]>
{
    private readonly AppFactory factory;

    public GetAppDomainsAction(AppFactory factory)
    {
        this.factory = factory;
    }

    public Task<AppDomainModel[]> Execute(EmptyRequest model) => factory.Installations.AppDomains();
}
