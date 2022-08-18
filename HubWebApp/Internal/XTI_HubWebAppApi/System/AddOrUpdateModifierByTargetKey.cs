namespace XTI_HubWebAppApi.System;

internal sealed class AddOrUpdateModifierByTargetKeyAction : AppAction<AddOrUpdateModifierByTargetKeyRequest, ModifierModel>
{
    private readonly AppFromSystemUser appFromSystemUser;
    private readonly HubFactory hubFactory;

    public AddOrUpdateModifierByTargetKeyAction(AppFromSystemUser appFromSystemUser, HubFactory hubFactory)
    {
        this.appFromSystemUser = appFromSystemUser;
        this.hubFactory = hubFactory;
    }

    public async Task<ModifierModel> Execute(AddOrUpdateModifierByTargetKeyRequest model, CancellationToken stoppingToken)
    {
        var appContextModel = await appFromSystemUser.App(AppVersionKey.Current);
        var app = await hubFactory.Apps.App(appContextModel.App.ID);
        var modCategory = await app.ModCategory(new ModifierCategoryName(model.ModCategoryName));
        var generatedModKey = new GeneratedKeyFactory().Create(model.GenerateModKey);
        var modifier = await modCategory.AddOrUpdateModifier(generatedModKey, model.TargetKey, model.TargetDisplayText);
        return modifier.ToModel();
    }
}
