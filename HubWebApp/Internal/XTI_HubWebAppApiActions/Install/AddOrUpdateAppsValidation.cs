using XTI_Core;

namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class AddOrUpdateAppsValidation : AppActionValidation<AddOrUpdateAppsRequest>
{
    public Task Validate(ErrorList errors, AddOrUpdateAppsRequest addRequest, CancellationToken stoppingToken)
    {
        if (addRequest.ToAppKeys().Any(ad => ad.Equals(AppKey.Unknown)))
        {
            errors.Add("App key is required");
        }
        return Task.CompletedTask;
    }
}
