using XTI_Core;
using XTI_Hub;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

public sealed class RemoteCommandService
{
    private readonly XtiEnvironment xtiEnv;
    private readonly IHttpClientFactory httpClientFactory;
    private readonly IHubAdministration hubAdmin;

    public RemoteCommandService(XtiEnvironment xtiEnv, IHttpClientFactory httpClientFactory, IHubAdministration hubAdmin)
    {
        this.xtiEnv = xtiEnv;
        this.httpClientFactory = httpClientFactory;
        this.hubAdmin = hubAdmin;
    }

    public async Task Run
    (
        string machineName,
        string command,
        AdminOptions commandOptions
    )
    {
        var optionsKey = await hubAdmin.StoreSingleUse
        (
            new RemoteOptionsStorageName().Value,
            GenerateKeyModel.SixDigit(),
            XtiSerializer.Serialize(commandOptions),
            TimeSpan.FromMinutes(30),
            default
        );
        using var client = httpClientFactory.CreateClient();
        client.Timeout = TimeSpan.FromHours(1);
        var dict = new Dictionary<string, string>
        {
            { "command", command },
            { "envName", xtiEnv.EnvironmentName },
            { "OptionsKey", optionsKey },
            { "HubAdministrationType", commandOptions.HubAdministrationType.ToString() }
        };
        using var content = new FormUrlEncodedContent(dict);
        var installServiceUrl = $"http://{machineName}:61862";
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Posting to '{installServiceUrl}'");
        using var response = await client.PostAsync(installServiceUrl, content);
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Response: {response.StatusCode} {responseBody}");
        response.EnsureSuccessStatusCode();
    }
}
