namespace XTI_HubWebAppApi.System;

internal sealed class GetModifierAction : AppAction<GetModifierRequest, ModifierModel>
{
    private readonly HubFactory hubFactory;

    public GetModifierAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<ModifierModel> Execute(GetModifierRequest getRequest, CancellationToken stoppingToken)
    {
        var modCategory = await hubFactory.ModCategories.Category(getRequest.CategoryID);
        var modifier = await modCategory.ModifierByModKey(new ModifierKey(getRequest.ModKey));
        return modifier.ToModel();
    }
}
