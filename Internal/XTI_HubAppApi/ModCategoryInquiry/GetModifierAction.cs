﻿using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.ModCategoryInquiry;

public sealed class GetModifierAction : AppAction<GetModCategoryModifierRequest, ModifierModel>
{
    private readonly AppFromPath appFromPath;

    public GetModifierAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ModifierModel> Execute(GetModCategoryModifierRequest model)
    {
        var app = await appFromPath.Value();
        var modCategory = await app.ModCategory(model.CategoryID);
        var modKey = new ModifierKey(model.ModifierKey);
        var modifier = await modCategory.ModifierByModKey(modKey);
        var modifierModel = modifier.ToModel();
        return modifierModel;
    }
}