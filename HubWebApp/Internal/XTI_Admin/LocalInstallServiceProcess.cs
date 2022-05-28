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
        using var client = httpClientFactory.CreateClient();
        using var content = new FormUrlEncodedContent
        (
            new[]
            {
                KeyValuePair.Create("command", "localInstall"),
                KeyValuePair.Create("envName", xtiEnv.EnvironmentName),
                KeyValuePair.Create("RemoteInstallKey", remoteInstallKey),
                KeyValuePair.Create("HubAdministrationType", hubDbTypeAccessor.Value.ToString())
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