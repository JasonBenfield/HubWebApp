namespace XTI_HubAppApi.UserMaintenance;

public sealed class GetUserForEditAction : AppAction<int, IDictionary<string, object?>>
{
    private readonly HubFactory factory;

    public GetUserForEditAction(HubFactory factory)
    {
        this.factory = factory;
    }

    public async Task<IDictionary<string, object?>> Execute(int userID)
    {
        var user = await factory.Users.User(userID);
        var userModel = user.ToModel();
        var form = new EditUserForm();
        form.UserID.SetValue(userModel.ID);
        form.PersonName.SetValue(userModel.Name);
        form.Email.SetValue(userModel.Email);
        return form.Export();
    }
}