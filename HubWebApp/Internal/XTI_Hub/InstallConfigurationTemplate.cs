using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class InstallConfigurationTemplate
{
    private readonly HubFactory hubFactory;
    private readonly InstallConfigurationTemplateEntity template;

    internal InstallConfigurationTemplate(HubFactory hubFactory, InstallConfigurationTemplateEntity template)
    {
        this.hubFactory = hubFactory;
        this.template = template;
    }

    internal int ID { get => template.ID; }

    internal Task Update
    (
        string destinationMachineName,
        string domain,
        string siteName
    ) =>
        hubFactory.DB.InstallConfigurationTemplates.Update
        (
            template,
            t =>
            {
                t.DestinationMachineName = destinationMachineName;
                t.Domain = domain;
                t.SiteName = siteName;
            }
        );

    public InstallConfigurationTemplateModel ToModel() =>
        new
        (
            ID: template.ID,
            TemplateName: template.TemplateName,
            DestinationMachineName: template.DestinationMachineName,
            Domain: template.Domain,
            SiteName: template.SiteName
        );
}