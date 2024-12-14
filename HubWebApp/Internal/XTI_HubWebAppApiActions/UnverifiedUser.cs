namespace XTI_HubWebAppApiActions;

public sealed class UnverifiedUser
{
    private readonly HubFactory factory;

    public UnverifiedUser(HubFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppUser> Verify(AppUserName userName, IHashedPassword hashedPassword)
    {
        AppUser user;
        var userExists = await factory.Users.UserNameExists(userName);
        if (userExists && !userName.IsAnon())
        {
            user = await factory.Users.UserByUserName(userName);
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