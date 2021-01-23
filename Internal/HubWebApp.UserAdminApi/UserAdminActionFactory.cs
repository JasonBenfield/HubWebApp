using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App;
using XTI_App.Api;
using XTI_Core;

namespace HubWebApp.UserAdminApi
{
    public sealed class UserAdminActionFactory
    {
        private readonly IServiceProvider sp;

        public UserAdminActionFactory(IServiceProvider sp)
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
