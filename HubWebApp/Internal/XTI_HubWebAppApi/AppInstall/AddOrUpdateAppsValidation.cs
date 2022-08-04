using XTI_Core;

namespace XTI_HubWebAppApi.AppInstall;

internal sealed class AddOrUpdateAppsValidation : AppActionValidation<AddOrUpdateAppsRequest>
{
    public Task Validate(ErrorList errors, AddOrUpdateAppsRequest model, CancellationToken stoppingToken)
    {
        if (model.Apps.Any(ad => ad.AppKey.Equals(AppKey.Unknown)))
        {
            errors.Add("App key is required");
        }
        return Task.CompletedTask;
    }
}
