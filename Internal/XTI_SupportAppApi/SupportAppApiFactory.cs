using System;
using XTI_App.Api;

namespace XTI_SupportAppApi
{
    public sealed class SupportAppApiFactory : AppApiFactory
    {
        private readonly IServiceProvider services;

        public SupportAppApiFactory(IServiceProvider services)
        {
            this.services = services;
        }

        protected override IAppApi _Create(IAppApiUser user) => new SupportAppApi
        (
            user,
            services
        );
    }
}
