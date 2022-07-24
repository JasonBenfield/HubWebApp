using XTI_Core;

namespace XTI_HubWebAppApi.UserList;

public sealed class AddUserAction : AppAction<AddUserForm, AppUserModel>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly HubFactory hubFactory;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly IClock clock;

    public AddUserAction(UserGroupFromPath userGroupFromPath, HubFactory hubFactory, IHashedPasswordFactory hashedPasswordFactory, IClock clock)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.hubFactory = hubFactory;
        this.hashedPasswordFactory = hashedPasswordFactory;
        this.clock = clock;
    }

    public async Task<AppUserModel> Execute(AddUserForm form, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value();
        var userName = new AppUserName(form.UserName.Value() ?? "");
        var existingUser = await hubFactory.Users.UserOrAnon(userName);
        if (existingUser.IsUserName(userName))
        {
            throw new AppException(string.Format(AppErrors.UserAlreadyExists, userName.DisplayText));
        }
        var hashedPassword = hashedPasswordFactory.Create(form.Password.Value() ?? "");
        var user = await userGroup.AddOrUpdate
        (
            userName,
            hashedPassword,
            new PersonName(form.PersonName.Value() ?? ""),
            new EmailAddress(form.Email.Value() ?? ""),
            clock.Now()
        );
        return user.ToModel();
    }
}