namespace XTI_HubWebAppApiActions;

public sealed class UnverifiedUser
{
    private readonly EfHubDB factory;

    public UnverifiedUser(EfHubDB factory)
    {
        this.factory = factory;
    }

    public async Task<EfAppUser> Verify(AppUserName userName, IHashedPassword hashedPassword, CancellationToken ct)
    {
        EfAppUser user;
        var userExists = await factory.Users.UserNameExists(userName, ct);
        if (userExists && !userName.IsAnon())
        {
            user = await factory.Users.UserByUserName(userName, ct);
            if (!user.IsPasswordCorrect(hashedPassword))
            {
                throw new PasswordIncorrectException(userName.DisplayText);
            }
        }
        else
        {
            throw new UserNotFoundException(userName.DisplayText);
        }
        return user;
    }
}