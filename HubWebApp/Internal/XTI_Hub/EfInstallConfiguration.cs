using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfInstallConfiguration
{
    private readonly EfHubDB db;
    private readonly InstallConfigurationEntity config;

    internal EfInstallConfiguration(EfHubDB db, InstallConfigurationEntity config)
    {
        this.db = db;
        this.config = config;
    }

    public bool IsFound() => config.ID > 0;

    internal int ID { get => config.ID; }

    internal Task Update(EfInstallConfigurationTemplate template, int installSequence, CancellationToken ct) =>
        db.Context.InstallConfigurations.Update
        (
            config,
            c =>
            {
                c.TemplateID = template.ID;
                c.InstallSequence = installSequence;
            },
            ct
        );

    public Task Delete(CancellationToken ct) =>
        db.Context.InstallConfigurations.Delete(config, ct);

    public async Task<InstallConfigurationModel> ToModel(CancellationToken ct)
    {
        var template = await db.InstallConfigurationTemplates.Template(config.TemplateID, ct);
        return new
        (
            ID: config.ID,
            RepoOwner: config.RepoOwner,
            RepoName: config.RepoName,
            ConfigurationName: config.ConfigurationName,
            AppKey: new AppKey(new AppName(config.AppName), AppType.Values.Value(config.AppType)),
            Template: template.ToModel(),
            InstallSequence: config.InstallSequence
        );
    }

}
