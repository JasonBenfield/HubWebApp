﻿namespace XTI_HubAppApi.AppInstall;

public sealed class GetVersionAction : AppAction<GetVersionRequest, XtiVersionModel>
{
    private readonly HubFactory appFactory;

    public GetVersionAction(HubFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<XtiVersionModel> Execute(GetVersionRequest model, CancellationToken stoppingToken)
    {
        var version = await appFactory.Versions.VersionByName(model.VersionName, model.VersionKey);
        return version.ToModel();
    }
}