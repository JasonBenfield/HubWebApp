namespace XTI_HubWebAppApiActions.System;

public sealed class AddOrUpdateModifierByTargetKeyAction : AppAction<SystemAddOrUpdateModifierByTargetKeyRequest, ModifierModel>
{
    private readonly AppFromSystemUser appFromSystemUser;

    public AddOrUpdateModifierByTargetKeyAction(AppFromSystemUser appFromSystemUser)
    {
        this.appFromSystemUser = appFromSystemUser;
    }

    public async Task<ModifierModel> Execute(SystemAddOrUpdateModifierByTargetKeyRequest requestData, CancellationToken stoppingToken)
    {
        var efApp = await appFromSystemUser.App(stoppingToken);
        var efModCategory = await efApp.ModCategory(new ModifierCategoryName(requestData.ModCategoryName), stoppingToken);
        var generatedModKey = new GeneratedKeyFactory().Create(requestData.GenerateModKey);
        var efModifier = await efModCategory.AddOrUpdateModifier(generatedModKey, requestData.TargetKey, requestData.TargetDisplayText, stoppingToken);
        return efModifier.ToModel();
    }
}
