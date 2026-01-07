namespace XTI_HubWebAppApiActions.System;

public sealed class GetModifierAction : AppAction<GetModifierRequest, ModifierModel>
{
    private readonly HubFactory hubFactory;

    public GetModifierAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<ModifierModel> Execute(GetModifierRequest getRequest, CancellationToken stoppingToken)
    {
        var modCategory = await hubFactory.ModCategories.Category(getRequest.CategoryID, stoppingToken);
        var modifier = await modCategory.ModifierByModKey(new ModifierKey(getRequest.ModKey), stoppingToken);
        return modifier.ToModel();
    }
}
