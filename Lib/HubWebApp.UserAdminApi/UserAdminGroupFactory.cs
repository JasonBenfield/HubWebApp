using XTI_WebApp.Api;
using XTI_App;

namespace HubWebApp.UserAdminApi
{
    public abstract class UserAdminGroupFactory : IUserAdminFactory
    {
        protected UserAdminGroupFactory()
        {
        }

        public AppAction<AddUserModel, int> CreateAddUserAction()
        {
            var appFactory = CreateAppFactory();
            var hashedPasswordFactory = CreateHashedPasswordFactory();
            var clock = CreateClock();
            return new AddUserAction(appFactory, hashedPasswordFactory, clock);
        }

        protected abstract AppFactory CreateAppFactory();

        protected abstract IHashedPasswordFactory CreateHashedPasswordFactory();

        protected abstract Clock CreateClock();
    }
}
