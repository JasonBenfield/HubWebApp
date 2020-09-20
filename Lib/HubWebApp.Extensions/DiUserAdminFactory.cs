using HubWebApp.UserAdminApi;
using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App;

namespace HubWebApp.Extensions
{
    public sealed class DiUserAdminFactory : UserAdminGroupFactory
    {
        public DiUserAdminFactory(IServiceProvider sp)
        {
            this.sp = sp;
        }

        private readonly IServiceProvider sp;

        protected override AppFactory CreateAppFactory() => sp.GetService<AppFactory>();

        protected override IHashedPasswordFactory CreateHashedPasswordFactory() =>
            sp.GetService<IHashedPasswordFactory>();

        protected override Clock CreateClock() => sp.GetService<Clock>();
    }
}
