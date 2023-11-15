using XTI_App.Abstractions;
using XTI_Core;
using XTI_Processes;

namespace XTI_Admin;

public sealed class PublishSetupProcess
{
    private readonly PublishedFolder publishedFolder;
    private readonly XtiEnvironment xtiEnv;

    public PublishSetupProcess(PublishedFolder publishedFolder, XtiEnvironment xtiEnv)
    {
        this.publishedFolder = publishedFolder;
        this.xtiEnv = xtiEnv;
    }

    public async Task Run(AppKey appKey, AppVersionKey versionKey)
    {
        Console.WriteLine("Publishing setup");
        var setupAppDir = Path.Combine
        (
            Environment.CurrentDirectory,
            "Apps",
            $"{getAppName(appKey)}SetupApp"
        );
        if (Directory.Exists(setupAppDir))
        {
            var publishDir = getPublishDir(appKey, versionKey);
            var publishSetupDir = Path.Combine(publishDir, "Setup");
            Console.WriteLine($"Publishing setup to '{publishSetupDir}'");
            var publishProcess = new WinProcess("dotnet")
                .WriteOutputToConsole()
                .UseArgumentNameDelimiter("")
                .AddArgument("publish")
                .AddArgument(new Quoted(setupAppDir))
                .UseArgumentNameDelimiter("-")
                .AddArgument("c", getConfiguration())
                .UseArgumentValueDelimiter("=")
                .AddArgument("p:PublishProfile", "Default")
                .AddArgument("p:PublishDir", publishSetupDir);
            var result = await publishProcess.Run();
            result.EnsureExitCodeIsZero();
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

    private string getPublishDir(AppKey appKey, AppVersionKey versionKey) =>
        publishedFolder.AppDir(appKey, versionKey);

    private static string getAppName(AppKey appKey) => appKey.Name.DisplayText.Replace(" ", "");

    private string getConfiguration() =>
        xtiEnv.IsProduction() ? "Release" : "Debug";
}
