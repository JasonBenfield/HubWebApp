using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;

namespace XTI_HubAppApi.Auth
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
            if (!user.Exists())
            {
                throw new UserNotFoundException(userName.DisplayText);
            }
            if (!user.IsPasswordCorrect(hashedPassword))
            {
                throw new PasswordIncorrectException(userName.DisplayText);
            }
            return user;
        }
    }
}
