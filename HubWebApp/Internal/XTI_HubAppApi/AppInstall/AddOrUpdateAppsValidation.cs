using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;

namespace XTI_HubAppApi.AppInstall;

internal sealed class AddOrUpdateAppsValidation : AppActionValidation<AddOrUpdateAppsRequest>
{
    public Task Validate(ErrorList errors, AddOrUpdateAppsRequest model)
    {
        if (model.Apps.Any(ad => ad.AppKey.Type.Equals(AppType.Values.WebApp) && string.IsNullOrWhiteSpace(ad.Domain)))
        {
            errors.Add("Domain is required when app is a web app");
        }
        return Task.CompletedTask;
    }
}
