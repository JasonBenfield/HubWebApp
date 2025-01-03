﻿namespace XTI_HubWebAppApiActions.ModCategoryInquiry;

public sealed class GetModCategoryAction : AppAction<int, ModifierCategoryModel>
{
    private readonly AppFromPath appFromPath;

    public GetModCategoryAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ModifierCategoryModel> Execute(int categoryID, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var modCategory = await app.ModCategory(categoryID);
        return modCategory.ToModel();
    }
}