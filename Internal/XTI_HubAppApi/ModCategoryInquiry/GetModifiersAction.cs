﻿using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.ModCategoryInquiry;

public sealed class GetModifiersAction : AppAction<int, ModifierModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetModifiersAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ModifierModel[]> Execute(int modCategoryID)
    {
        var app = await appFromPath.Value();
        var modCategory = await app.ModCategory(modCategoryID);
        var modifiers = await modCategory.Modifiers();
        return modifiers.Select(m => m.ToModel()).ToArray();
    }
}