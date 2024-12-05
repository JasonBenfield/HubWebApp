using XTI_Core;

namespace XTI_SupportServiceAppApi;

public sealed partial class SupportAppApi : AppApiWrapper
{
    public SupportAppApi(IAppApiUser user, IServiceProvider sp)
        : base
        (
            new AppApi
            (
                sp,
                SupportInfo.AppKey,
                user,
                ResourceAccess.AllowAuthenticated()
                    .WithAllowed(AppRoleName.Admin),
                XtiSerializer.Serialize(new SupportServiceAppOptions())
            )
        )
    {
        createPermanentLogGroup(sp);
        createInstallationsGroup(sp);
    }

    partial void createPermanentLogGroup(IServiceProvider sp);

    partial void createInstallationsGroup(IServiceProvider sp);
}