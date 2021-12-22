using XTI_App.Api;
using XTI_Hub;
using XTI_WebApp.Api;

namespace XTI_HubAppApi;

public sealed partial class HubAppApi : WebAppApiWrapper
{
    public HubAppApi
    (
        IAppApiUser user,
        IServiceProvider services
    )
        : base
        (
            new AppApi
            (
                HubInfo.AppKey,
                user,
                ResourceAccess.AllowAuthenticated()
                    .WithAllowed(HubInfo.Roles.Admin)
            ),
            services
        )
    {
        createAuth(services);
        createPermanentLog(services);
        createAppList(services);
        createAppInquiry(services);
        createInstall(services);
        createPublish(services);
        createAppPublish(services);
        createVersion(services);
        createResourceGroup(services);
        createResource(services);
        createModCategory(services);
        createUserList(services);
        createUserInquiry(services);
        createAppUser(services);
        createAppUserMaintenance(services);
        createUserMaintenance(services);
    }

    partial void createAuth(IServiceProvider services);

    partial void createUserList(IServiceProvider services);

    partial void createPermanentLog(IServiceProvider services);

    partial void createAppList(IServiceProvider services);

    partial void createAppInquiry(IServiceProvider services);

    partial void createInstall(IServiceProvider services);

    partial void createPublish(IServiceProvider services);

    partial void createAppPublish(IServiceProvider services);

    partial void createModCategory(IServiceProvider services);

    partial void createResourceGroup(IServiceProvider services);

    partial void createResource(IServiceProvider sp);

    partial void createAppUser(IServiceProvider services);

    partial void createAppUserMaintenance(IServiceProvider services);

    partial void createVersion(IServiceProvider services);

    partial void createUserInquiry(IServiceProvider services);

    partial void createUserMaintenance(IServiceProvider services);

}