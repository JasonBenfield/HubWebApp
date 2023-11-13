using XTI_Core;

namespace XTI_Admin;

internal sealed class RemoteCommandService
{
    private readonly XtiEnvironment xtiEnv;
    private readonly IHttpClientFactory httpClientFactory;

    public RemoteCommandService(XtiEnvironment xtiEnv, IHttpClientFactory httpClientFactory)
    {
        this.xtiEnv = xtiEnv;
        this.httpClientFactory = httpClientFactory;
    }

    public async Task Run(string machineName, string command, Dictionary<string, string> dict)
    {
        using var client = httpClientFactory.CreateClient();
        client.Timeout = TimeSpan.FromHours(1);
        dict.Add("command", command);
        dict.Add("envName", xtiEnv.EnvironmentName);
        using var content = new FormUrlEncodedContent(dict);
        var installServiceUrl = $"http://{machineName}:61862";
        Console.WriteLine($"Posting to '{installServiceUrl}'");
        using var response = await client.PostAsync(installServiceUrl, content);
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response: {response.StatusCode} {responseBody}");
        response.EnsureSuccessStatusCode();
    }
}
