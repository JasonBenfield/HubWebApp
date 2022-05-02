using XTI_Core;

namespace XTI_HubAppApi.AppInstall;

internal sealed class AddSystemUserValidation : AppActionValidation<AddSystemUserRequest>
{
    public Task Validate(ErrorList errors, AddSystemUserRequest model)
    {
        if (model.AppKey.Name.Equals(AppName.Unknown))
        {
            errors.Add("App Name is required.");
        }
        if (model.AppKey.Type.Equals(AppType.Values.NotFound))
        {
            errors.Add("App Type is required.");
        }
        if (string.IsNullOrWhiteSpace(model.Password))
        {
            errors.Add("Password is required");
        }
        return Task.CompletedTask;
    }
}