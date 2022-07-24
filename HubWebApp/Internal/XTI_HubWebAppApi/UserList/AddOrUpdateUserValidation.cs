using XTI_Core;

namespace XTI_HubWebAppApi.UserList;

public sealed class AddOrUpdateUserValidation : AppActionValidation<AddOrUpdateUserModel>
{
    public Task Validate(ErrorList errors, AddOrUpdateUserModel model, CancellationToken stoppingToken)
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