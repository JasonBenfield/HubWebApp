﻿using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.AuthApi
{
    public sealed class LogoutAction : AppAction<EmptyRequest, AppActionRedirectResult>
    {
        private readonly AccessForLogin access;
        private readonly ISessionContext sessionContext;
        private readonly Clock clock;

        public LogoutAction(AccessForLogin access, ISessionContext sessionContext, Clock clock)
        {
            this.access = access;
            this.sessionContext = sessionContext;
            this.clock = clock;
        }

        public async Task<AppActionRedirectResult> Execute(EmptyRequest model)
        {
            await access.Logout();
            await sessionContext.CurrentSession.End(clock.Now());
            return new AppActionRedirectResult("/Hub/Current/Auth");
        }
    }
}
