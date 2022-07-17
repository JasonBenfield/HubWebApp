namespace XTI_HubWebAppApi.UserMaintenance;

public sealed class EditUserAction : AppAction<EditUserForm, EmptyActionResult>
{
    private readonly UserGroupFromPath userGroupFromPath;

    public EditUserAction(UserGroupFromPath userGroupFromPath)
    {
        this.userGroupFromPath = userGroupFromPath;
    }

    public async Task<EmptyActionResult> Execute(EditUserForm model, CancellationToken stoppingToken)
    {
        var userID = model.UserID.Value() ?? 0;
        var userGroup = await userGroupFromPath.Value();
        var user = await userGroup.User(userID);
        var name = new PersonName(model.PersonName.Value() ?? "");
        if (string.IsNullOrWhiteSpace(name))
        {
            name = new PersonName(user.ToModel().UserName.Value);
        }
        var email = new EmailAddress(model.Email.Value() ?? "");
        await user.Edit(name, email);
        return new EmptyActionResult();
    }
}