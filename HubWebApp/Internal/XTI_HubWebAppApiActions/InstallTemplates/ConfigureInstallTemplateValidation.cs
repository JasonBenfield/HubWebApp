using XTI_Core;

namespace XTI_HubWebAppApiActions.InstallTemplates;

public sealed class ConfigureInstallTemplateValidation : AppActionValidation<ConfigureInstallTemplateRequest>
{
    public Task Validate(ErrorList errors, ConfigureInstallTemplateRequest requestData, CancellationToken stoppingToken)
    {
        if (string.IsNullOrWhiteSpace(requestData.TemplateName))
        {
            errors.Add(InstallErrors.TemplateNameIsRequired);
        }
        return Task.CompletedTask;
    }
}
