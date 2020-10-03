using System.Threading.Tasks;
using XTI_App;

namespace HubWebApp.AuthApi
{
    public sealed class UnverifiedUser
    {
        public UnverifiedUser(AppFactory factory)
        {
            this.factory = factory;
        }

        private readonly AppFactory factory;

        public async Task<AppUser> Verify(AppUserName userName, IHashedPassword hashedPassword)
        {
            var user = await factory.Users().User(userName);
            if (user.IsUnknown())
            {
                throw new UserNotFoundException(userName.Value);
            }
            if (!user.IsPasswordCorrect(hashedPassword))
            {
                throw new PasswordIncorrectException(userName.Value);
            }
            return user;
        }
    }
}
