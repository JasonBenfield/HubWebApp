﻿namespace XTI_HubWebAppApiActions.AppInquiry;

public sealed class GetDefaultAppOptionsAction : AppAction<EmptyRequest, string>
{
    private readonly AppFromPath appFromPath;

    public GetDefaultAppOptionsAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<string> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        return app.SerializedDefaultOptions;
    }
}
