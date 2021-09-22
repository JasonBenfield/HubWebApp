using System;
using XTI_App.Api;
using XTI_HubAppApi.PermanentLog;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        public PermanentLogGroup PermanentLog { get; private set; }

        partial void permanentLog(IServiceProvider services)
        {
            PermanentLog = new PermanentLogGroup
            (
                source.AddGroup(nameof(PermanentLog), ResourceAccess.AllowAuthenticated()),
                new PermanentLogGroupActionFactory(services)
            );
        }
    }
}
