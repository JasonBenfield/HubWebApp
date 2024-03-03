using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class InstallConfigurationTemplateRepository
{
    private readonly HubFactory hubFactory;

    public InstallConfigurationTemplateRepository(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<InstallConfigurationTemplate> AddOrUpdateTemplate
    (
        string templateName,
        string destinationMachineName,
        string domain,
        string siteName
    )
    {
        InstallConfigurationTemplate installConfigurationTemplate;
        var templateEntity = await hubFactory.DB.InstallConfigurationTemplates.Retrieve()
            .Where(t => t.TemplateName == templateName)
            .FirstOrDefaultAsync();
        if (templateEntity == null)
        {
            templateEntity = new InstallConfigurationTemplateEntity
            {
                TemplateName = templateName,
                DestinationMachineName = destinationMachineName,
                Domain = domain,
                SiteName = siteName
            };
            await hubFactory.DB.InstallConfigurationTemplates.Create(templateEntity);
            installConfigurationTemplate = new InstallConfigurationTemplate(hubFactory, templateEntity);
        }
        else
        {
            installConfigurationTemplate = new InstallConfigurationTemplate(hubFactory, templateEntity);
            await installConfigurationTemplate.Update(destinationMachineName, domain, siteName);
        }
        return installConfigurationTemplate;
    }

    internal async Task<InstallConfigurationTemplate> Template(int id)
    {
        var template = await hubFactory.DB.InstallConfigurationTemplates.Retrieve()
            .Where(t => t.ID == id)
            .FirstOrDefaultAsync();
        return new InstallConfigurationTemplate
        (
            hubFactory, 
            template ?? throw new Exception($"Template {id} was not found.")
        );
    }

    public async Task<InstallConfigurationTemplate> Template(string templateName)
    {
        var template = await hubFactory.DB.InstallConfigurationTemplates.Retrieve()
            .Where(t => t.TemplateName == templateName)
            .FirstOrDefaultAsync();
        return new InstallConfigurationTemplate
        (
            hubFactory,
            template ?? throw new Exception($"Template '{templateName}' was not found.")
        );
    }
}
