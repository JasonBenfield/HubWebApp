using XTI_HubAppApi.Auth;

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
        createHomeGroup(services);
        createAuth(services);
        createExternalAuth(services);
        createAuthenticators(services);
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
        createStorageGroup(services);
    }

    partial void createHomeGroup(IServiceProvider sp);

    partial void createAuth(IServiceProvider sp);

    partial void createExternalAuth(IServiceProvider sp);

    partial void createAuthenticators(IServiceProvider sp);

    partial void createUserList(IServiceProvider sp);

    partial void createPermanentLog(IServiceProvider sp);

    partial void createAppList(IServiceProvider sp);

    partial void createAppInquiry(IServiceProvider sp);

    partial void createInstall(IServiceProvider sp);

    partial void createPublish(IServiceProvider sp);

    partial void createAppPublish(IServiceProvider sp);

    partial void createModCategory(IServiceProvider sp);

    partial void createResourceGroup(IServiceProvider sp);

    partial void createResource(IServiceProvider sp);

    partial void createAppUser(IServiceProvider sp);

    partial void createAppUserMaintenance(IServiceProvider sp);

    partial void createVersion(IServiceProvider sp);

    partial void createUserInquiry(IServiceProvider sp);

    partial void createUserMaintenance(IServiceProvider sp);

    partial void createStorageGroup(IServiceProvider sp);

    protected override void ConfigureTemplate(AppApiTemplate template)
    {
        base.ConfigureTemplate(template);
        template.ExcludeValueTemplates(IsValueTemplateExcluded);
    }

    private static bool IsValueTemplateExcluded(ValueTemplate templ, ApiCodeGenerators codeGenerator)
    {
        if(codeGenerator == ApiCodeGenerators.Dotnet)
        {
            return templ.DataType == typeof(LoginCredentials)
                || templ.DataType.Namespace == "XTI_App.Abstractions"
                || templ.DataType.Namespace == "XTI_TempLog.Abstractions"
                || templ.DataType.Namespace == "XTI_Hub.Abstractions";
        }
        return false;
    }
}