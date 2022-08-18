using XTI_App.Abstractions;
using XTI_Core;
using XTI_Processes;

namespace XTI_Admin;

internal sealed class PublishSetupProcess
{
    private readonly Scopes scopes;

    public PublishSetupProcess(Scopes scopes)
    {
        this.scopes = scopes;
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
            var privateFiles = Directory.GetFiles(publishSetupDir, "*.private.*");
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
        scopes.GetRequiredService<PublishedFolder>().AppDir(appKey, versionKey);

    private static string getAppName(AppKey appKey) => appKey.Name.DisplayText.Replace(" ", "");

    private string getConfiguration()
    {
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        return xtiEnv.IsProduction()
            ? "Release"
            : "Debug";
    }
}
