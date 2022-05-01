namespace XTI_HubAppApi.UserMaintenance;

public sealed class EditUserAction : AppAction<EditUserForm, EmptyActionResult>
{
    private readonly HubFactory factory;

    public EditUserAction(HubFactory factory)
    {
        this.factory = factory;
    }

    public async Task<EmptyActionResult> Execute(EditUserForm model)
    {
        var userID = model.UserID.Value() ?? 0;
        var user = await factory.Users.User(userID);
        var name = new PersonName(model.PersonName.Value() ?? "");
        if (string.IsNullOrWhiteSpace(name))
        {
            name = new PersonName(user.UserName().Value);
        }
        var email = new EmailAddress(model.Email.Value() ?? "");
        await user.Edit(name, email);
        return new EmptyActionResult();
    }
}