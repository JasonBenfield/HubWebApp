namespace XTI_HubWebAppApiActions.System;

public sealed class AddOrUpdateModifierByModKeyAction : AppAction<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel>
{
    private readonly AppFromSystemUser appFromSystemUser;

    public AddOrUpdateModifierByModKeyAction(AppFromSystemUser appFromSystemUser)
    {
        this.appFromSystemUser = appFromSystemUser;
    }

    public async Task<ModifierModel> Execute(SystemAddOrUpdateModifierByModKeyRequest requestData, CancellationToken stoppingToken)
    {
        var efApp = await appFromSystemUser.App(stoppingToken);
        var efModCategory = await efApp.ModCategory(new ModifierCategoryName(requestData.ModCategoryName), stoppingToken);
        var modKey = new ModifierKey(requestData.ModKey);
        var efModifier = await efModCategory.AddOrUpdateModifier(modKey, requestData.TargetKey, requestData.TargetDisplayText, stoppingToken);
        return efModifier.ToModel();
    }
}
