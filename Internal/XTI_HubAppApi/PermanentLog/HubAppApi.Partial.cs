using XTI_App.Api;
using XTI_HubAppApi.PermanentLog;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private PermanentLogGroup? permanentLog;

    public PermanentLogGroup PermanentLog { get => permanentLog ?? throw new ArgumentNullException(nameof(permanentLog)); }

    partial void createPermanentLog(IServiceProvider services)
    {
        permanentLog = new PermanentLogGroup
        (
            source.AddGroup(nameof(PermanentLog), ResourceAccess.AllowAuthenticated()),
            services
        );
    }
}