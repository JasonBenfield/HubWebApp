﻿namespace XTI_HubWebAppApi.Auth;

internal sealed class LoginReturnKeyAction : AppAction<LoginReturnModel, string>
{
    private readonly ILoginReturnKey returnKey;

    public LoginReturnKeyAction(ILoginReturnKey returnKey)
    {
        this.returnKey = returnKey;
    }

    public Task<string> Execute(LoginReturnModel model, CancellationToken stoppingToken) => returnKey.Value(model.ReturnUrl);
}
