﻿namespace XTI_HubAppApi.VersionInquiry;

public sealed class GetVersionAction : AppAction<string, XtiVersionModel>
{
    private readonly AppFromPath appFromPath;

    public GetVersionAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<XtiVersionModel> Execute(string versionKey, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var version = await app.Version(AppVersionKey.Parse(versionKey));
        return version.ToVersionModel();
    }
}