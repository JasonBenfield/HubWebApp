using XTI_Core;

namespace XTI_Admin;

internal sealed class LocalInstallServiceProcess
{
    private readonly Scopes scopes;

    public LocalInstallServiceProcess(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Run(string machineName, string remoteInstallKey)
    {
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        var hubDbTypeAccessor = scopes.GetRequiredService<HubDbTypeAccessor>();
        var httpClientFactory = scopes.GetRequiredService<IHttpClientFactory>();
        var options = scopes.GetRequiredService<AdminOptions>();
        using var client = httpClientFactory.CreateClient();
        client.Timeout = TimeSpan.FromHours(1);
        using var content = new FormUrlEncodedContent
        (
            new[]
            {
                KeyValuePair.Create("command", "localInstall"),
                KeyValuePair.Create("envName", xtiEnv.EnvironmentName),
                KeyValuePair.Create("RemoteInstallKey", remoteInstallKey),
                KeyValuePair.Create("HubAdministrationType", hubDbTypeAccessor.Value.ToString()),
                KeyValuePair.Create("HubAppVersionKey", options.HubAppVersionKey),
                KeyValuePair.Create("InstallationSource", options.GetInstallationSource(xtiEnv).ToString())
            }
        );
        var installServiceUrl = $"http://{machineName}:61862";
        Console.WriteLine($"Posting to '{installServiceUrl}' {remoteInstallKey}");
        using var response = await client.PostAsync(installServiceUrl, content);
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response: {response.StatusCode} {responseBody}");
        response.EnsureSuccessStatusCode();
    }
}