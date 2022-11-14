namespace XTI_HubWebAppApi.CurrentUser;

internal sealed class EditUserAction : AppAction<EditCurrentUserForm, AppUserModel>
{
    private readonly IUserContext userContext;
    private readonly HubFactory hubFactory;

    public EditUserAction(IUserContext userContext, HubFactory hubFactory)
    {
        this.userContext = userContext;
        this.hubFactory = hubFactory;
    }

    public async Task<AppUserModel> Execute(EditCurrentUserForm model, CancellationToken stoppingToken)
    {
        var userContextModel = await userContext.User();
        var user = await hubFactory.Users.User(userContextModel.User.ID);
        var name = new PersonName(model.PersonName.Value() ?? "");
        var email = new EmailAddress(model.Email.Value() ?? "");
        await user.Edit(name, email);
        return user.ToModel();
    }
}
