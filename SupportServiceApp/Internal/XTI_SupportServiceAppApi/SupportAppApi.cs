namespace XTI_SupportServiceAppApi;

public sealed partial class SupportAppApi : AppApiWrapper
{
    public SupportAppApi
    (
        IAppApiUser user,
        IServiceProvider sp
    )
        : base
        (
            new AppApi
            (
                SupportInfo.AppKey,
                user,
                ResourceAccess.AllowAuthenticated()
                    .WithAllowed(AppRoleName.Admin)
            )
        )
    {
        createPermanentLogGroup(sp);
    }

    partial void createPermanentLogGroup(IServiceProvider sp);
}