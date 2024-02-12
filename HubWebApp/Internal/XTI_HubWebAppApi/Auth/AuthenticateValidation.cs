using XTI_Core;

namespace XTI_HubWebAppApi.Auth;

public sealed class AuthenticateValidation : AppActionValidation<LoginCredentials>
{
    public Task Validate(ErrorList errors, LoginCredentials model, CancellationToken stoppingToken)
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