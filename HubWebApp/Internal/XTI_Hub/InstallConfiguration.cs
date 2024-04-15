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

    internal Task Update(InstallConfigurationTemplate template, int installSequence) =>
        hubFactory.DB.InstallConfigurations.Update
        (
            config,
            c =>
            {
                c.TemplateID = template.ID;
                c.InstallSequence = installSequence;
            }
        );

    internal Task Delete() =>
        hubFactory.DB.InstallConfigurations.Delete(config);

    public async Task<InstallConfigurationModel> ToModel()
    {
        var template = await hubFactory.InstallConfigurationTemplates.Template(config.TemplateID);
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
