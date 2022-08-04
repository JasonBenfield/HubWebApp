namespace XTI_HubWebAppApi.UserMaintenance;

public sealed class GetUserForEditAction : AppAction<int, IDictionary<string, object?>>
{
    private readonly UserGroupFromPath userGroupFromPath;

    public GetUserForEditAction(UserGroupFromPath userGroupFromPath)
    {
        this.userGroupFromPath = userGroupFromPath;
    }

    public async Task<IDictionary<string, object?>> Execute(int userID, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value();
        var user = await userGroup.User(userID);
        var userModel = user.ToModel();
        var form = new EditUserForm();
        form.UserID.SetValue(userModel.ID);
        form.PersonName.SetValue(userModel.Name);
        form.Email.SetValue(userModel.Email);
        return form.Export();
    }
}