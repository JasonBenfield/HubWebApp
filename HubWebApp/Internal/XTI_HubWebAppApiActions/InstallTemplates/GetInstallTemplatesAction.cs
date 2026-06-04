namespace XTI_HubWebAppApiActions.InstallTemplates;

public sealed class GetInstallTemplatesAction : AppAction<EmptyRequest, InstallConfigurationTemplateModel[]>
{
    private readonly EfHubDB db;

    public GetInstallTemplatesAction(EfHubDB db)
    {
        this.db = db;
    }

    public async Task<InstallConfigurationTemplateModel[]> Execute(EmptyRequest requestData, CancellationToken stoppingToken)
    {
        var efTemplates = await db.InstallConfigurationTemplates.Templates(stoppingToken);
        return efTemplates.Select(t => t.ToModel()).ToArray();
    }
}
