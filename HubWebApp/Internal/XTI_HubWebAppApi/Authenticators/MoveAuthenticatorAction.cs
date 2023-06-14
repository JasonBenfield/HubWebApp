namespace XTI_HubWebAppApi.Authenticators;

internal sealed class MoveAuthenticatorAction : AppAction<MoveAuthenticatorRequest, EmptyActionResult>
{
    private readonly HubFactory hubFactory;

    public MoveAuthenticatorAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<EmptyActionResult> Execute(MoveAuthenticatorRequest moveRequest, CancellationToken stoppingToken)
    {
        var authenticatorKey = new AuthenticatorKey(moveRequest.AuthenticatorKey);
        var sourceUser = await hubFactory.Users.UserOrAnonByExternalKey(authenticatorKey, moveRequest.ExternalUserKey);
        if (!sourceUser.IsUserName(AppUserName.Anon))
        {
            await sourceUser.DeleteAuthenticator(authenticatorKey, moveRequest.ExternalUserKey);
        }
        var targetUser = await hubFactory.Users.User(moveRequest.TargetUserID);
        await targetUser.AddAuthenticator(authenticatorKey, moveRequest.ExternalUserKey);
        return new EmptyActionResult();
    }
}
