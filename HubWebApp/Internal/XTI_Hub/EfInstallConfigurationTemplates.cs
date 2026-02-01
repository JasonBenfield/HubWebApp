using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfInstallConfigurationTemplates
{
    private readonly EfHubDB hubFactory;

    public EfInstallConfigurationTemplates(EfHubDB hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<EfInstallConfigurationTemplate> AddOrUpdateTemplate
    (
        string templateName,
        string destinationMachineName,
        string domain,
        string siteName,
        CancellationToken ct
    )
    {
        EfInstallConfigurationTemplate installConfigurationTemplate;
        var templateEntity = await hubFactory.Context.InstallConfigurationTemplates.Retrieve()
            .Where(t => t.TemplateName == templateName)
            .FirstOrDefaultAsync(ct);
        if (templateEntity == null)
        {
            templateEntity = new InstallConfigurationTemplateEntity
            {
                TemplateName = templateName,
                DestinationMachineName = destinationMachineName,
                Domain = domain,
                SiteName = siteName
            };
            await hubFactory.Context.InstallConfigurationTemplates.Create(templateEntity, ct);
            installConfigurationTemplate = new EfInstallConfigurationTemplate(hubFactory, templateEntity);
        }
        else
        {
            installConfigurationTemplate = new EfInstallConfigurationTemplate(hubFactory, templateEntity);
            await installConfigurationTemplate.Update(destinationMachineName, domain, siteName, ct);
        }
        return installConfigurationTemplate;
    }

    internal async Task<EfInstallConfigurationTemplate> Template(int id, CancellationToken ct)
    {
        var template = await hubFactory.Context.InstallConfigurationTemplates.Retrieve()
            .Where(t => t.ID == id)
            .FirstOrDefaultAsync(ct);
        return new EfInstallConfigurationTemplate
        (
            hubFactory, 
            template ?? throw new Exception($"Template {id} was not found.")
        );
    }

    public async Task<EfInstallConfigurationTemplate> Template(string templateName, CancellationToken ct)
    {
        var template = await hubFactory.Context.InstallConfigurationTemplates.Retrieve()
            .Where(t => t.TemplateName == templateName)
            .FirstOrDefaultAsync(ct);
        return new EfInstallConfigurationTemplate
        (
            hubFactory,
            template ?? throw new Exception($"Template '{templateName}' was not found.")
        );
    }
}
