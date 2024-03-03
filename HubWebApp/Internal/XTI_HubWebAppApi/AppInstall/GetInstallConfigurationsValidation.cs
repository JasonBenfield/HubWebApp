using XTI_Core;

namespace XTI_HubWebAppApi.AppInstall;

internal sealed class GetInstallConfigurationsValidation : AppActionValidation<GetInstallConfigurationsRequest>
{
    public Task Validate(ErrorList errors, GetInstallConfigurationsRequest getRequest, CancellationToken stoppingToken)
    {
        if (string.IsNullOrWhiteSpace(getRequest.RepoOwner))
        {
            errors.Add("Repo Owner is required.");
        }
        if (string.IsNullOrWhiteSpace(getRequest.RepoName))
        {
            errors.Add("Repo Name is required.");
        }
        return Task.CompletedTask;
    }
}
