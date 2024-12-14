using XTI_Core;

namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class AddSystemUserValidation : AppActionValidation<AddSystemUserRequest>
{
    public Task Validate(ErrorList errors, AddSystemUserRequest addRequest, CancellationToken stoppingToken)
    {
        var appKey = addRequest.AppKey.ToAppKey();
        if (appKey.Name.Equals(AppName.Unknown))
        {
            errors.Add("App Name is required.");
        }
        if (appKey.Type.Equals(AppType.Values.NotFound))
        {
            errors.Add("App Type is required.");
        }
        if (string.IsNullOrWhiteSpace(addRequest.Password))
        {
            errors.Add("Password is required");
        }
        return Task.CompletedTask;
    }
}