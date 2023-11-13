using XTI_Core;

namespace XTI_Admin;

internal sealed class LocalInstallServiceProcess
{
    private readonly Scopes scopes;

    public LocalInstallServiceProcess(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public Task Run(string machineName, string remoteInstallKey)
    {
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        var hubDbTypeAccessor = scopes.GetRequiredService<HubDbTypeAccessor>();
        var httpClientFactory = scopes.GetRequiredService<IHttpClientFactory>();
        var options = scopes.GetRequiredService<AdminOptions>();
        var remoteCommandService = new RemoteCommandService(xtiEnv, httpClientFactory);
        var dict = new Dictionary<string, string>
        {
            { "RemoteInstallKey", remoteInstallKey },
            { "HubAdministrationType", hubDbTypeAccessor.Value.ToString() },
            { "HubAppVersionKey", options.HubAppVersionKey },
            { "InstallationSource", options.GetInstallationSource(xtiEnv).ToString() }
        };
        return remoteCommandService.Run(machineName, "localInstall", dict);
    }
}