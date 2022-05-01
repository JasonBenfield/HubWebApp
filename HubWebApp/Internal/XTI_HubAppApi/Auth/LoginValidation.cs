using XTI_Core;

namespace XTI_HubAppApi.Auth;

public sealed class LoginValidation : AppActionValidation<LoginCredentials>
{
    public Task Validate(ErrorList errors, LoginCredentials model)
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