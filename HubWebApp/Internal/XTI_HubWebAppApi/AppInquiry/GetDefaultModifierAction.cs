﻿namespace XTI_HubWebAppApi.AppInquiry;

public sealed class GetDefaultModifierAction : AppAction<EmptyRequest, ModifierModel>
{
    private readonly AppFromPath appFromPath;

    public GetDefaultModifierAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ModifierModel> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var modifier = await app.DefaultModifier();
        return modifier.ToModel();
    }
}