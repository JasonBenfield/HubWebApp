using HubWebApp.UserAdminApi;
using System;
using XTI_App;
using XTI_App.EF;
using Microsoft.Extensions.DependencyInjection;

namespace HubWebApp.Fakes
{
    public sealed class FakeUserAdminGroupFactory : UserAdminGroupFactory
    {
        public FakeUserAdminGroupFactory(IServiceProvider sp)
        {
            this.sp = sp;
        }

        private readonly IServiceProvider sp;

        protected override AppFactory CreateAppFactory() => sp.GetService<AppFactory>();

        protected override IHashedPasswordFactory CreateHashedPasswordFactory() => sp.GetService<IHashedPasswordFactory>();

        protected override Clock CreateClock() => sp.GetService<Clock>();
    }
}
