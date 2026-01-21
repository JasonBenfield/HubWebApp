namespace XTI_HubWebAppApiActions.Auth;

public sealed class LoginReturnKeyAction : AppAction<LoginReturnModel, string>
{
    private readonly IAnonClient anonClient;
    private readonly ILoginReturnKey returnKey;

    public LoginReturnKeyAction(IAnonClient anonClient, ILoginReturnKey returnKey)
    {
        this.anonClient = anonClient;
        this.returnKey = returnKey;
    }

    public async Task<string> Execute(LoginReturnModel requestData, CancellationToken stoppingToken)
    {
        anonClient.Load();
        var requesterKey = string.IsNullOrWhiteSpace(anonClient.RequesterKey) ?
            Guid.NewGuid().ToString("N") :
            anonClient.RequesterKey;
        var returnKeyValue = await returnKey.Value
        (
            requesterKey: requesterKey,
            returnUrl: requestData.ReturnUrl,
            ct: stoppingToken
        );
        return returnKeyValue;
    }
}
