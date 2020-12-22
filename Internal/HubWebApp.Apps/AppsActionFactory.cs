using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.Apps
{
    public sealed class AppsActionFactory
    {
        private readonly IServiceProvider sp;

        public AppsActionFactory(IServiceProvider sp)
        {
            this.sp = sp;
        }

        public AppAction<EmptyRequest, AppModel[]> CreateAll()
        {
            var appFactory = sp.GetService<AppFactory>();
            var userContext = sp.GetService<IUserContext>();
            return new AllAction(appFactory, userContext);
        }
    }
}
