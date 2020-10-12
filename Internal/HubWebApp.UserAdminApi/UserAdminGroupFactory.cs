using XTI_App.Api;
using XTI_App;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace HubWebApp.UserAdminApi
{
    public sealed class UserAdminGroupFactory
    {
        private readonly IServiceProvider sp;

        public UserAdminGroupFactory(IServiceProvider sp)
        {
            this.sp = sp;
        }

        public AppAction<AddUserModel, int> CreateAddUserAction()
        {
            var appFactory = sp.GetService<AppFactory>();
            var hashedPasswordFactory = sp.GetService<IHashedPasswordFactory>();
            var clock = sp.GetService<Clock>();
            return new AddUserAction(appFactory, hashedPasswordFactory, clock);
        }
    }
}
