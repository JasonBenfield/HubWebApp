﻿namespace XTI_HubWebAppApi.AppPublish;

public sealed class GetVersionsAction : AppAction<AppKey, XtiVersionModel[]>
{
    private readonly HubFactory appFactory;

    public GetVersionsAction(HubFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<XtiVersionModel[]> Execute(AppKey appKey, CancellationToken stoppingToken)
    {
        var app = await appFactory.Apps.App(appKey);
        var versions = await app.Versions();
        var versionModels = versions.Select(v => v.ToModel()).ToArray();
        return versionModels;
    }
}