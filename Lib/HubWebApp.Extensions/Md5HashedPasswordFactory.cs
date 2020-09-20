using XTI_App;

namespace HubWebApp.Extensions
{
    public sealed class Md5HashedPasswordFactory : IHashedPasswordFactory
    {
        public IHashedPassword Create(string password) => new Md5HashedPassword(password);
    }
}
