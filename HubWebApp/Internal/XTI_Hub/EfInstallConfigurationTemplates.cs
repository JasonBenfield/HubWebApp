using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfInstallConfigurationTemplates
{
    private readonly EfHubDB db;

    internal EfInstallConfigurationTemplates(EfHubDB db)
    {
        this.db = db;
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
        var templateEntity = await db.Context.InstallConfigurationTemplates.Retrieve()
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
            await db.Context.InstallConfigurationTemplates.Create(templateEntity, ct);
            installConfigurationTemplate = new EfInstallConfigurationTemplate(db, templateEntity);
        }
        else
        {
            installConfigurationTemplate = new EfInstallConfigurationTemplate(db, templateEntity);
            await installConfigurationTemplate.Update(destinationMachineName, domain, siteName, ct);
        }
        return installConfigurationTemplate;
    }

    internal async Task<EfInstallConfigurationTemplate> Template(int id, CancellationToken ct)
    {
        var template = await db.Context.InstallConfigurationTemplates.Retrieve()
            .Where(t => t.ID == id)
            .FirstOrDefaultAsync(ct);
        return new EfInstallConfigurationTemplate
        (
            db,
            template ?? throw new Exception($"Template {id} was not found.")
        );
    }

    public async Task<EfInstallConfigurationTemplate> Template(string templateName, CancellationToken ct)
    {
        var template = await db.Context.InstallConfigurationTemplates.Retrieve()
            .Where(t => t.TemplateName == templateName)
            .FirstOrDefaultAsync(ct);
        return new EfInstallConfigurationTemplate
        (
            db,
            template ?? throw new Exception($"Template '{templateName}' was not found.")
        );
    }

    public async Task<EfInstallConfigurationTemplate[]> Templates(CancellationToken ct)
    {
        var templates = await db.Context.InstallConfigurationTemplates.Retrieve().ToArrayAsync(ct);
        return templates.Select(t => new EfInstallConfigurationTemplate(db, t)).ToArray();
    }
}
