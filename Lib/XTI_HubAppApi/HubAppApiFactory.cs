﻿using System;
using XTI_App.Api;

namespace XTI_HubAppApi
{
    public sealed class HubAppApiFactory : AppApiFactory
    {
        private readonly IServiceProvider sp;

        public HubAppApiFactory(IServiceProvider sp)
        {
            this.sp = sp;
        }

        protected override IAppApi _Create(IAppApiUser user) => new HubAppApi(user, sp);
    }
}
