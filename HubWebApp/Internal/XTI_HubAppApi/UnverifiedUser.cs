using XTI_App.Abstractions;
using XTI_Hub;

namespace XTI_HubAppApi;

public sealed class UnverifiedUser
{
    private readonly AppFactory factory;

    public UnverifiedUser(AppFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppUser> Verify(AppUserName userName, IHashedPassword hashedPassword)
    {
        AppUser user;
        var userExists = await factory.Users.UserNameExists(userName);
        if (userExists && !userName.Equals(AppUserName.Anon))
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