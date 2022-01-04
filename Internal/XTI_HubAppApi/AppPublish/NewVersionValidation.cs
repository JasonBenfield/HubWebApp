using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;

namespace XTI_HubAppApi.AppPublish;

internal sealed class NewVersionValidation : AppActionValidation<NewVersionRequest>
{
    public Task Validate(ErrorList errors, NewVersionRequest model)
    {
        if (model.VersionType.Equals(AppVersionType.Values.NotSet))
        {
            errors.Add("Version type is required");
        }
        if(model.AppKey.Type.Equals(AppType.Values.WebApp) && string.IsNullOrWhiteSpace(model.Domain))
        {
            errors.Add("Domain is required when app is a web app");
        }
        return Task.CompletedTask;
    }
}