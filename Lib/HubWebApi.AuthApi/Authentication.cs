using System;
using System.Security.Claims;
using System.Threading.Tasks;
using XTI_App;

namespace HubWebApp.AuthApi
{
    public sealed class Authentication
    {
        public Authentication(UnverifiedUser unverifiedUser, AccessToken accessToken, IHashedPasswordFactory hashedPasswordFactory)
        {
            this.unverifiedUser = unverifiedUser;
            this.accessToken = accessToken;
            this.hashedPasswordFactory = hashedPasswordFactory;
        }

        private readonly UnverifiedUser unverifiedUser;
        private readonly AccessToken accessToken;
        private readonly IHashedPasswordFactory hashedPasswordFactory;

        public async Task<LoginResult> Authenticate(string userName, string password)
        {
            var hashedPassword = hashedPasswordFactory.Create(password);
            var user = await unverifiedUser.Verify(new AppUserName(userName), hashedPassword);
            var claims = new XtiClaimsCreator(null, user).Values();
            var token = await accessToken.Generate(claims);
            return new LoginResult(token);
        }
    }
}
