namespace XTI_HubWebAppApiActions.System;

public sealed class AddOrUpdateModifierByModKeyAction : AppAction<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel>
{
    private readonly AppFromSystemUser appFromSystemUser;
    private readonly EfHubDB hubFactory;

    public AddOrUpdateModifierByModKeyAction(AppFromSystemUser appFromSystemUser, EfHubDB hubFactory)
    {
        this.appFromSystemUser = appFromSystemUser;
        this.hubFactory = hubFactory;
    }

    public async Task<ModifierModel> Execute(SystemAddOrUpdateModifierByModKeyRequest model, CancellationToken stoppingToken)
    {
        var appContextModel = await appFromSystemUser.App(model.InstallationID, stoppingToken);
        var app = await hubFactory.Apps.App(appContextModel.App.ID, stoppingToken);
        var modCategory = await app.ModCategory(new ModifierCategoryName(model.ModCategoryName), stoppingToken);
        var modKey = new ModifierKey(model.ModKey);
        var modifier = await modCategory.AddOrUpdateModifier(modKey, model.TargetKey, model.TargetDisplayText, stoppingToken);
        return modifier.ToModel();
    }
}
