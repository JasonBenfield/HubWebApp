using XTI_Core;

namespace XTI_HubWebAppApi.AppInstall;

internal sealed class ConfigureInstallValidation : AppActionValidation<ConfigureInstallRequest>
{
    public Task Validate(ErrorList errors, ConfigureInstallRequest configRequest, CancellationToken stoppingToken)
    {
        if (string.IsNullOrWhiteSpace(configRequest.RepoOwner))
        {
            errors.Add("Repo Owner is required.");
        }
        if (string.IsNullOrWhiteSpace(configRequest.RepoName))
        {
            errors.Add("Repo Name is required.");
        }
        if (string.IsNullOrWhiteSpace(configRequest.ConfigurationName))
        {
            errors.Add("Configuration Name is required.");
        }
        if (string.IsNullOrWhiteSpace(configRequest.AppKey.AppName))
        {
            errors.Add("App Name is required.");
        }
        if (configRequest.AppKey.AppType <= 0)
        {
            errors.Add("App Type is required.");
        }
        if (string.IsNullOrWhiteSpace(configRequest.TemplateName))
        {
            errors.Add("Template Name is required.");
        }
        return Task.CompletedTask;
    }
}
