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
        createHomeGroup(sp);
    }

    partial void createHomeGroup(IServiceProvider sp);
}