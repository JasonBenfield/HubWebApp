using XTI_Core;

namespace XTI_HubWebAppApi.AppInstall;

internal sealed class ConfigureInstallTemplateValidation : AppActionValidation<ConfigureInstallTemplateRequest>
{
    public Task Validate(ErrorList errors, ConfigureInstallTemplateRequest model, CancellationToken stoppingToken)
    {
        if (string.IsNullOrWhiteSpace(model.TemplateName))
        {
            errors.Add("Template Name is required.");
        }
        return Task.CompletedTask;
    }
}
