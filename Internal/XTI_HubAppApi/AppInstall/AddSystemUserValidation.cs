using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;

namespace XTI_HubAppApi.AppInstall;

internal sealed class AddSystemUserValidation : AppActionValidation<AddSystemUserRequest>
{
    public Task Validate(ErrorList errors, AddSystemUserRequest model)
    {
        if(model.AppKey.Type.Equals(AppType.Values.WebApp) && string.IsNullOrWhiteSpace(model.Domain))
        {
            errors.Add("Domain is required when app is a web app");
        }
        return Task.CompletedTask;
    }
}