﻿namespace XTI_HubWebAppApi.AppInstall;

internal sealed class AddOrUpdateVersionsAction : AppAction<AddOrUpdateVersionsRequest, EmptyActionResult>
{
    private readonly IHubAdministration hubAdmin;

    public AddOrUpdateVersionsAction(IHubAdministration hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public async Task<EmptyActionResult> Execute(AddOrUpdateVersionsRequest addRequest, CancellationToken stoppingToken)
    {
        await hubAdmin.AddOrUpdateVersions
        (
            addRequest.ToAppKeys(), 
            addRequest.Versions
        );
        return new EmptyActionResult();
    }
}
