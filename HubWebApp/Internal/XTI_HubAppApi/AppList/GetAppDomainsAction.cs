﻿namespace XTI_HubAppApi.AppList;

internal sealed class GetAppDomainsAction : AppAction<EmptyRequest, AppDomainModel[]>
{
    private readonly HubFactory factory;

    public GetAppDomainsAction(HubFactory factory)
    {
        this.factory = factory;
    }

    public Task<AppDomainModel[]> Execute(EmptyRequest model) => factory.Installations.AppDomains();
}
