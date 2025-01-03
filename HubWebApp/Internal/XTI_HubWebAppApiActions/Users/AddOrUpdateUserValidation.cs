﻿using XTI_Core;

namespace XTI_HubWebAppApiActions.UserList;

public sealed class AddOrUpdateUserValidation : AppActionValidation<AddOrUpdateUserRequest>
{
    public Task Validate(ErrorList errors, AddOrUpdateUserRequest model, CancellationToken stoppingToken)
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