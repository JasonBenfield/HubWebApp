using XTI_Core;

namespace XTI_HubWebAppApi.UserList;

public sealed class AddUserValidation : AppActionValidation<AddUserModel>
{
    public Task Validate(ErrorList errors, AddUserModel model, CancellationToken stoppingToken)
    {
        if (string.IsNullOrWhiteSpace(model.UserName))
        {
            errors.Add(UserErrors.UserNameIsRequired, "User Name", nameof(model.UserName));
        }
        if (string.IsNullOrWhiteSpace(model.Password))
        {
            errors.Add(UserErrors.PasswordIsRequired, "Password", nameof(model.Password));
        }
        return Task.CompletedTask;
    }
}