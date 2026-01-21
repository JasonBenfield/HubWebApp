namespace XTI_HubWebAppApiActions.AppInquiry;

public sealed class GetResourceGroupsAction : AppAction<EmptyRequest, ResourceGroupModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetResourceGroupsAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ResourceGroupModel[]> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value(stoppingToken);
        var version = await app.CurrentVersion(stoppingToken);
        var resourceGroups = await version.ResourceGroups(stoppingToken);
        return resourceGroups.Select(rg => rg.ToModel()).ToArray();
    }
}