using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfInstallConfigurationTemplate
{
    private readonly EfHubDB db;
    private readonly InstallConfigurationTemplateEntity template;

    internal EfInstallConfigurationTemplate(EfHubDB db, InstallConfigurationTemplateEntity template)
    {
        this.db = db;
        this.template = template;
    }

    internal int ID { get => template.ID; }

    internal Task Update
    (
        string destinationMachineName,
        string domain,
        string siteName,
        CancellationToken ct
    ) =>
        db.Context.InstallConfigurationTemplates.Update
        (
            template,
            t =>
            {
                t.DestinationMachineName = destinationMachineName;
                t.Domain = domain;
                t.SiteName = siteName;
            },
            ct
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