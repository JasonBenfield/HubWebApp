using XTI_Core;

namespace XTI_HubWebAppApi.UserList;

public sealed class AddOrUpdateUserAction : AppAction<AddUserModel, int>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly IClock clock;

    public AddOrUpdateUserAction(UserGroupFromPath userGroupFromPath, IHashedPasswordFactory hashedPasswordFactory, IClock clock)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.hashedPasswordFactory = hashedPasswordFactory;
        this.clock = clock;
    }

    public async Task<int> Execute(AddUserModel model, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value();
        var userName = new AppUserName(model.UserName);
        var hashedPassword = hashedPasswordFactory.Create(model.Password);
        var timeAdded = clock.Now();
        var user = await userGroup.AddOrUpdate
        (
            userName,
            hashedPassword,
            new PersonName(model.PersonName),
            new EmailAddress(model.Email),
            timeAdded
        );
        return user.ToModel().ID;
    }
}