namespace XTI_SupportServiceAppApi;

public sealed partial class SupportAppApi : AppApiWrapper
{
    public SupportAppApi
    (
        IAppApiUser user,
        string serializedDefaultOptions,
        IServiceProvider sp
    )
        : base
        (
            new AppApi
            (
                SupportInfo.AppKey,
                user,
                ResourceAccess.AllowAuthenticated()
                    .WithAllowed(AppRoleName.Admin),
                serializedDefaultOptions
            )
        )
    {
        createPermanentLogGroup(sp);
        createInstallationsGroup(sp);
    }

    partial void createPermanentLogGroup(IServiceProvider sp);

    partial void createInstallationsGroup(IServiceProvider sp);
}