namespace XTI_HubWebAppApiActions.CurrentUser;

public sealed class ChangePasswordAction : AppAction<ChangeCurrentUserPasswordForm, EmptyActionResult>
{
    private readonly IUserContext userContext;
    private readonly HubFactory hubFactory;
    private readonly IHashedPasswordFactory hashedPasswordFactory;

    public ChangePasswordAction(IUserContext userContext, HubFactory hubFactory, IHashedPasswordFactory hashedPasswordFactory)
    {
        this.userContext = userContext;
        this.hubFactory = hubFactory;
        this.hashedPasswordFactory = hashedPasswordFactory;
    }

    public async Task<EmptyActionResult> Execute(ChangeCurrentUserPasswordForm model, CancellationToken stoppingToken)
    {
        var userModel = await userContext.User(stoppingToken);
        var user = await hubFactory.Users.User(userModel.ID, stoppingToken);
        var hashedPassword = hashedPasswordFactory.Create(model.Password.Value() ?? "");
        await user.ChangePassword(hashedPassword, stoppingToken);
        return new EmptyActionResult();
    }
}