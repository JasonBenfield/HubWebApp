using XTI_App.Abstractions;

namespace XTI_Hub
{
    public sealed class Md5HashedPasswordFactory : IHashedPasswordFactory
    {
        public IHashedPassword Create(string password) => new Md5HashedPassword(password);
    }
}
