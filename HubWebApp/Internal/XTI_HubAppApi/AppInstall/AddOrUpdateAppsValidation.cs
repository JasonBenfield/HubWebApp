using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;

namespace XTI_HubAppApi.AppInstall;

internal sealed class AddOrUpdateAppsValidation : AppActionValidation<AddOrUpdateAppsRequest>
{
    public Task Validate(ErrorList errors, AddOrUpdateAppsRequest model)
    {
        if (model.Apps.Any(ad => ad.AppKey.Equals(AppKey.Unknown)))
        {
            errors.Add("App key is required");
        }
        return Task.CompletedTask;
    }
}
