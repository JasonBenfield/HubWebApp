using XTI_Hub;
using XTI_HubAppApi.PermanentLog;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private PermanentLogGroup? permanentLog;

    public PermanentLogGroup PermanentLog
    {
        get => permanentLog ?? throw new ArgumentNullException(nameof(permanentLog));
    }

    partial void createPermanentLog(IServiceProvider sp)
    {
        permanentLog = new PermanentLogGroup
        (
            source.AddGroup(nameof(PermanentLog), Access.WithAllowed(HubInfo.Roles.PermanentLog)),
            sp
        );
    }
}