using System.Security.Cryptography.X509Certificates;
using XTI_Core;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

public sealed class RemoteCommandService
{
    private readonly XtiEnvironment xtiEnv;
    private readonly IHttpClientFactory httpClientFactory;
    private readonly StoredObjectFactory storedObjFactory;

    public RemoteCommandService(XtiEnvironment xtiEnv, IHttpClientFactory httpClientFactory, StoredObjectFactory storedObjFactory)
    {
        this.xtiEnv = xtiEnv;
        this.httpClientFactory = httpClientFactory;
        this.storedObjFactory = storedObjFactory;
    }

    public async Task Run
    (
        string machineName, 
        string command, 
        AdminOptions commandOptions
    )
    {
        var optionsKey = await storedObjFactory.CreateStoredObject(new RemoteOptionsStorageName().Value)
            .Store
            (
                GenerateKeyModel.SixDigit(),
                commandOptions,
                TimeSpan.FromMinutes(30)
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
