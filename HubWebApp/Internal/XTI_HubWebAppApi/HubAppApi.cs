using XTI_HubWebAppApi.Auth;

namespace XTI_HubWebAppApi;

public sealed partial class HubAppApi : WebAppApiWrapper
{
    public HubAppApi
    (
        IAppApiUser user,
        IServiceProvider sp
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
            sp
        )
    {
        createCurrentUserGroup(sp);
        createHomeGroup(sp);
        createInstallationsGroup(sp);
        createAuth(sp);
        createExternalAuth(sp);
        createAuthenticators(sp);
        createPermanentLog(sp);
        createAppList(sp);
        createAppInquiry(sp);
        createInstall(sp);
        createPublish(sp);
        createAppPublish(sp);
        createVersion(sp);
        createResourceGroup(sp);
        createResource(sp);
        createModCategory(sp);
        createUserList(sp);
        createUserInquiry(sp);
        createAppUser(sp);
        createAppUserMaintenance(sp);
        createUserMaintenance(sp);
        createStorageGroup(sp);
        createSystemGroup(sp);
        createUserGroupsGroup(sp);
        createPeriodicGroup(sp);
        createLogsGroup(sp);
        createUserRolesGroup(sp);
    }

    partial void createCurrentUserGroup(IServiceProvider sp);

    partial void createHomeGroup(IServiceProvider sp);

    partial void createInstallationsGroup(IServiceProvider sp);

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

    partial void createSystemGroup(IServiceProvider sp);

    partial void createUserGroupsGroup(IServiceProvider sp);

    partial void createUserRolesGroup(IServiceProvider sp);

    partial void createPeriodicGroup(IServiceProvider sp);

    partial void createLogsGroup(IServiceProvider sp);

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