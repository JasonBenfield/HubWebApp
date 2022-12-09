namespace XTI_HubWebAppApi.System;

internal sealed class AddOrUpdateModifierByModKeyAction : AppAction<AddOrUpdateModifierByModKeyRequest, ModifierModel>
{
    private readonly AppFromSystemUser appFromSystemUser;
    private readonly HubFactory hubFactory;

    public AddOrUpdateModifierByModKeyAction(AppFromSystemUser appFromSystemUser, HubFactory hubFactory)
    {
        this.appFromSystemUser = appFromSystemUser;
        this.hubFactory = hubFactory;
    }

    public async Task<ModifierModel> Execute(AddOrUpdateModifierByModKeyRequest model, CancellationToken stoppingToken)
    {
        var appContextModel = await appFromSystemUser.App(AppVersionKey.Current);
        var app = await hubFactory.Apps.App(appContextModel.App.ID);
        var modCategory = await app.ModCategory(new ModifierCategoryName(model.ModCategoryName));
        var modKey = new ModifierKey(model.ModKey);
        var modifier = await modCategory.AddOrUpdateModifier(modKey, model.TargetKey, model.TargetDisplayText);
        return modifier.ToModel();
    }
}
