using XTI_Core;

namespace XTI_HubWebAppApi.Auth;

public sealed class LoginValidation : AppActionValidation<AuthenticatedLoginRequest>
{
    public Task Validate(ErrorList errors, AuthenticatedLoginRequest model, CancellationToken stoppingToken)
    {
        if (string.IsNullOrWhiteSpace(model.AuthKey))
        {
            errors.Add("Auth Key is Required");
        }
        if (string.IsNullOrWhiteSpace(model.AuthID))
        {
            errors.Add("Auth ID is Required");
        }
        return Task.CompletedTask;
    }
}