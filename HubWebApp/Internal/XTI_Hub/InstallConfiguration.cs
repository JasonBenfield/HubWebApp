using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class InstallConfiguration
{
    private readonly HubFactory hubFactory;
    private readonly InstallConfigurationEntity config;

    internal InstallConfiguration(HubFactory hubFactory, InstallConfigurationEntity config)
    {
        this.hubFactory = hubFactory;
        this.config = config;
    }

    public bool IsFound() => config.ID > 0;

    internal Task Update(InstallConfigurationTemplate template, int installSequence, CancellationToken ct) =>
        hubFactory.DB.InstallConfigurations.Update
        (
            config,
            c =>
            {
                c.TemplateID = template.ID;
                c.InstallSequence = installSequence;
            },
            ct
        );

    internal Task Delete(CancellationToken ct) =>
        hubFactory.DB.InstallConfigurations.Delete(config, ct);

    public async Task<InstallConfigurationModel> ToModel(CancellationToken ct)
    {
        var template = await hubFactory.InstallConfigurationTemplates.Template(config.TemplateID, ct);
        return new
        (
            ID: config.ID,
            ConfigurationName: config.ConfigurationName,
            AppKey: new AppKey(new AppName(config.AppName), AppType.Values.Value(config.AppType)),
            Template: template.ToModel(),
            InstallSequence: config.InstallSequence
        );
    }

}
