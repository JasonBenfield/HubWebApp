using XTI_Core;
using XTI_HubWebAppApiActions.Auth;

namespace XTI_HubWebAppApiActions.AuthApi;

public sealed class AuthenticateValidation : AppActionValidation<AuthenticateRequest>
{
    public Task Validate(ErrorList errors, AuthenticateRequest model, CancellationToken stoppingToken)
    {
        if (string.IsNullOrWhiteSpace(model.UserName))
        {
            errors.Add(AuthErrors.UserNameIsRequired, "User Name", nameof(model.UserName));
        }
        if (string.IsNullOrWhiteSpace(model.Password))
        {
            errors.Add(AuthErrors.PasswordIsRequired, "Password", nameof(model.Password));
        }
        return Task.CompletedTask;
    }
}