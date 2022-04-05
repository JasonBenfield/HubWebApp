using XTI_App.Abstractions;
using XTI_Core;

namespace XTI_Admin;

internal sealed class LocalInstallServiceProcess
{
    private readonly Scopes scopes;
    private readonly AppKey appKey;

    public LocalInstallServiceProcess(Scopes scopes, AppKey appKey)
    {
        this.scopes = scopes;
        this.appKey = appKey;
    }

    public async Task Run()
    {
        var options = scopes.GetRequiredService<AdminOptions>();
        var appVersion = await new CurrentVersion(scopes).Value();
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        var release = $"v{appVersion.VersionNumber.Format()}";
        var httpClientFactory = scopes.GetRequiredService<IHttpClientFactory>();
        using var client = httpClientFactory.CreateClient();
        using var content = new FormUrlEncodedContent
        (
            new[]
            {
                KeyValuePair.Create("command", "install"),
                KeyValuePair.Create("envName", xtiEnv.EnvironmentName),
                KeyValuePair.Create("appName", appKey.Name.Value),
                KeyValuePair.Create("appType", appKey.Type.DisplayText.Replace(" ", "")),
                KeyValuePair.Create("versionKey", appVersion.VersionKey.DisplayText),
                KeyValuePair.Create("repoOwner", options.RepoOwner),
                KeyValuePair.Create("repoName", options.RepoName),
                KeyValuePair.Create("installationUserName", options.InstallationUserName),
                KeyValuePair.Create("installationPassword", options.InstallationPassword),
                KeyValuePair.Create("release", release),
                KeyValuePair.Create("destinationMachine", options.DestinationMachine)
            }
        );
        var installServiceUrl = $"http://{options.DestinationMachine}:61862";
        Console.WriteLine($"Posting to '{installServiceUrl}' {appKey.Name.Value} {appKey.Type.DisplayText} {appVersion.VersionKey} {options.InstallationUserName} {options.InstallationPassword} {release}");
        using var response = await client.PostAsync(installServiceUrl, content);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);
    }
}