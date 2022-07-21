namespace XTI_HubWebAppApi.UserMaintenance;

public sealed class ChangePasswordAction : AppAction<ChangePasswordForm, EmptyActionResult>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly IHashedPasswordFactory hashedPasswordFactory;

    public ChangePasswordAction(UserGroupFromPath userGroupFromPath, IHashedPasswordFactory hashedPasswordFactory)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.hashedPasswordFactory = hashedPasswordFactory;
    }

    public async Task<EmptyActionResult> Execute(ChangePasswordForm model, CancellationToken stoppingToken)
    {
        var userID = model.UserID.Value() ?? 0;
        var userGroup = await userGroupFromPath.Value();
        var user = await userGroup.User(userID);
        var hashedPassword = hashedPasswordFactory.Create(model.Password.Value() ?? "");
        await user.ChangePassword(hashedPassword);
        return new EmptyActionResult();
    }
}