using XTI_Core;

namespace XTI_HubWebAppApi.UserList;

public sealed class AddOrUpdateUserAction : AppAction<AddUserModel, int>
{
    private readonly HubFactory appFactory;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly IClock clock;

    public AddOrUpdateUserAction(HubFactory appFactory, IHashedPasswordFactory hashedPasswordFactory, IClock clock)
    {
        this.appFactory = appFactory;
        this.hashedPasswordFactory = hashedPasswordFactory;
        this.clock = clock;
    }

    public async Task<int> Execute(AddUserModel model, CancellationToken stoppingToken)
    {
        var userName = new AppUserName(model.UserName);
        var hashedPassword = hashedPasswordFactory.Create(model.Password);
        var timeAdded = clock.Now();
        var user = await appFactory.Users.AddOrUpdate
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