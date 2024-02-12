using XTI_Hub.Abstractions;
using XTI_WebApp.Abstractions;

namespace XTI_HubAppClient.WebApp.Extensions;

public sealed class HcBasicAuthValidator : IBasicAuthValidator
{
    private readonly HubAppClient hubClient;

    public HcBasicAuthValidator(HubAppClient hubClient)
    {
        this.hubClient = hubClient;
    }

    public async Task<bool> IsValid(string username, string password)
    {
        var form = new VerifyLoginForm();
        form.UserName.SetValue(username);
        form.Password.SetValue(password);
        var loginResult = await hubClient.Auth.VerifyLogin(form);
        return !string.IsNullOrWhiteSpace(loginResult.AuthKey);
    }
}
