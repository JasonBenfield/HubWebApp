using XTI_Core;

namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class DeleteInstallConfigurationValidation : AppActionValidation<DeleteInstallConfigurationRequest>
{
    public Task Validate(ErrorList errors, DeleteInstallConfigurationRequest deleteRequest, CancellationToken stoppingToken)
    {
        if (string.IsNullOrWhiteSpace(deleteRequest.RepoOwner))
        {
            errors.Add("Repo Owner is required.");
        }
        if (string.IsNullOrWhiteSpace(deleteRequest.RepoName))
        {
            errors.Add("Repo Name is required.");
        }
        if (string.IsNullOrWhiteSpace(deleteRequest.ConfigurationName))
        {
            errors.Add("Repo Name is required.");
        }
        if (string.IsNullOrWhiteSpace(deleteRequest.AppKey.AppName))
        {
            errors.Add("App Name is required.");
        }
        if (deleteRequest.AppKey.AppType <= 0)
        {
            errors.Add("App Type is required.");
        }
        return Task.CompletedTask;
    }
}
