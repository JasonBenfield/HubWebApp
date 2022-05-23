﻿using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class LocalInstallServiceProcess
{
    private readonly Scopes scopes;
    private readonly AppKey appKey;
    private readonly AppVersionName versionName;

    public LocalInstallServiceProcess(Scopes scopes, AppKey appKey, AppVersionName versionName)
    {
        this.scopes = scopes;
        this.appKey = appKey;
        this.versionName = versionName;
    }

    public async Task Run(string machineName, string remoteInstallKey)
    {
        var gitRepoInfo = scopes.GetRequiredService<GitRepoInfo>();
        var appVersion = await new CurrentVersion(scopes, versionName).Value();
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        var release = $"v{appVersion.VersionNumber.Format()}";
        var httpClientFactory = scopes.GetRequiredService<IHttpClientFactory>();
        using var client = httpClientFactory.CreateClient();
        using var content = new FormUrlEncodedContent
        (
            new[]
            {
                KeyValuePair.Create("command", "localInstall"),
                KeyValuePair.Create("envName", xtiEnv.EnvironmentName),
                KeyValuePair.Create("appName", appKey.Name.Value),
                KeyValuePair.Create("appType", appKey.Type.DisplayText.Replace(" ", "")),
                KeyValuePair.Create("versionKey", appVersion.VersionKey.DisplayText),
                KeyValuePair.Create("repoOwner", gitRepoInfo.RepoOwner),
                KeyValuePair.Create("repoName", gitRepoInfo.RepoName),
                KeyValuePair.Create("RemoteInstallKey", remoteInstallKey),
                KeyValuePair.Create("release", release)
            }
        );
        var installServiceUrl = $"http://{machineName}:61862";
        Console.WriteLine($"Posting to '{installServiceUrl}' {appKey.Name.Value} {appKey.Type.DisplayText} {appVersion.VersionKey.DisplayText} {remoteInstallKey} {release}");
        using var response = await client.PostAsync(installServiceUrl, content);
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response: {response.StatusCode} {responseBody}");
        response.EnsureSuccessStatusCode();
    }
}