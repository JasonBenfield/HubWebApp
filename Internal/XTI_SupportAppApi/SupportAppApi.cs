using System;
using XTI_App.Api;

namespace XTI_SupportAppApi
{
    public sealed class SupportAppApi : AppApiWrapper
    {
        public SupportAppApi
        (
            IAppApiUser user,
            IServiceProvider services
        )
            : base
            (
                new AppApi
                (
                    SupportInfo.AppKey,
                    user,
                    ResourceAccess.AllowAuthenticated()
                )
            )
        {
            Log = new LogGroup
            (
                source.AddGroup(nameof(Log)),
                new LogActionFactory(services)
            );
        }

        public LogGroup Log { get; }
    }
}
