using XTI_App.Abstractions;
using XTI_Core;
using XTI_Processes;

namespace XTI_Admin;

public sealed class PublishSetupProcess
{
    private readonly PublishedFolder publishedFolder;
    private readonly XtiEnvironment xtiEnv;
    private readonly AppVersionNameAccessor versionNameAccessor;
    private readonly GitRepoInfo gitRepoInfo;

    public PublishSetupProcess(PublishedFolder publishedFolder, XtiEnvironment xtiEnv, AppVersionNameAccessor versionNameAccessor, GitRepoInfo gitRepoInfo)
    {
        this.publishedFolder = publishedFolder;
        this.xtiEnv = xtiEnv;
        this.versionNameAccessor = versionNameAccessor;
        this.gitRepoInfo = gitRepoInfo;
    }

    public async Task Run(AppKey appKey, AppVersionKey versionKey)
    {
        Console.WriteLine("Publishing setup");
        var setupAppDir = Path.Combine
        (
            Environment.CurrentDirectory,
            "Apps",
            $"{GetAppName(appKey)}SetupApp"
        );
        if (Directory.Exists(setupAppDir))
        {
            var publishDir = GetPublishDir(appKey, versionKey);
            var publishSetupDir = Path.Combine(publishDir, "Setup");
            Console.WriteLine($"Publishing setup to '{publishSetupDir}'");
            var publishProcess = new WinProcess("dotnet")
                .WriteOutputToConsole()
                .UseArgumentNameDelimiter("")
                .AddArgument("publish")
                .AddArgument(new Quoted(setupAppDir))
                .UseArgumentNameDelimiter("-")
                .AddArgument("c", GetConfiguration())
                .UseArgumentValueDelimiter("=")
                .AddArgument("p:PublishProfile", "Default")
                .AddArgument("p:PublishDir", publishSetupDir);
            var result = await publishProcess.Run();
            result.EnsureExitCodeIsZero();
            var appsettingsPath = Path.Combine(publishSetupDir, "appsettings.json");
            if (!File.Exists(appsettingsPath))
            {
                var versionName = versionNameAccessor.Value;
                File.WriteAllText
                (
                    appsettingsPath,
                    $$"""
                    {
                        "VersionName": "{{versionName.Value}}",
                        "VersionKey": "{{versionKey.Value}}",
                        "RepoOwner": "{{gitRepoInfo.RepoOwner}}",
                        "RepoName": "{{gitRepoInfo.RepoName}}"
                    }
                    """
                );
            }
            var privateFiles = Directory.GetFiles(publishSetupDir, "*.private.*")
                .Where(f => !f.EndsWith(".dll", StringComparison.OrdinalIgnoreCase));
            foreach (var privateFile in privateFiles)
            {
                File.Delete(privateFile);
            }
        }
        else
        {
            Console.WriteLine($"Setup App Not Found at '{setupAppDir}'");
        }
    }

    private string GetPublishDir(AppKey appKey, AppVersionKey versionKey) =>
        publishedFolder.AppDir(appKey, versionKey);

    private static string GetAppName(AppKey appKey) => appKey.Name.DisplayText.Replace(" ", "");

    private string GetConfiguration() =>
        xtiEnv.IsProduction() ? "Release" : "Debug";
}
