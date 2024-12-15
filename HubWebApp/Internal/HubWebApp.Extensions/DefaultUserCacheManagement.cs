using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;
using XTI_HubWebAppApiActions;
using XTI_WebAppClient;

namespace HubWebApp.Extensions;

internal sealed class DefaultUserCacheManagement : IUserCacheManagement
{
    private readonly ICachedUserContext userContext;
    private readonly HubFactory hubFactory;
    private readonly GenericAppClientFactory appClientFactory;
    private readonly InstallationIDAccessor installationIDAccessor;

    public DefaultUserCacheManagement(ICachedUserContext userContext, HubFactory hubFactory, GenericAppClientFactory appClientFactory, InstallationIDAccessor installationIDAccessor)
    {
        this.userContext = userContext;
        this.hubFactory = hubFactory;
        this.appClientFactory = appClientFactory;
        this.installationIDAccessor = installationIDAccessor;
    }

    public async Task ClearCache(AppUserName userName, CancellationToken ct)
    {
        userContext.ClearCache(userName);
        var user = await hubFactory.Users.UserOrAnon(userName);
        var installationID = await installationIDAccessor.Value();
        var thisInstallation = await hubFactory.Installations.InstallationOrDefault(installationID);
        var appVersion = await thisInstallation.AppVersion();
        var thisInstallationModel = thisInstallation.ToModel();
        var thisAppName = appVersion.App.ToModel().AppKey.Name;
        var thisVersionKey = thisInstallationModel.IsCurrent 
            ? AppVersionKey.Current 
            : appVersion.Version.ToModel().VersionKey;
        var loggedInApps = await user.GetLoggedInApps();
        foreach(var loggedInApp in loggedInApps)
        {
            if 
            (
                !thisAppName.Equals(loggedInApp.AppName) || 
                !thisVersionKey.Equals(loggedInApp.VersionKey) || 
                !thisInstallationModel.Domain.Equals(loggedInApp.Domain, StringComparison.OrdinalIgnoreCase)
            )
            {
                var appClient = appClientFactory.Create
                (
                    loggedInApp.AppName.DisplayText.Replace(" ", ""),
                    loggedInApp.VersionKey.DisplayText,
                    loggedInApp.Domain
                );
                try
                {
                    await appClient.UserCache.ClearCache(userName.Value, ct);
                }
                catch { }
            }
        }
    }
}
