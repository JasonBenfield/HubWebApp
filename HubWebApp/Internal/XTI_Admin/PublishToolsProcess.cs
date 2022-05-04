using XTI_App.Abstractions;
using XTI_Core;
using XTI_Processes;

namespace XTI_Admin;

internal sealed class PublishToolsProcess
{
    private readonly Scopes scopes;

    public PublishToolsProcess(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Run(AppKey appKey, AppVersionKey versionKey)
    {
        var publishDir = scopes.GetRequiredService<PublishedFolder>().AppDir(appKey, versionKey);
        var srcPsDir = Path.Combine(Environment.CurrentDirectory, "Powershell");
        if (Directory.Exists(srcPsDir))
        {
            foreach (var srcDir in Directory.GetDirectories(srcPsDir))
            {
                var prjName = new DirectoryInfo(srcDir).Name;
                var publishPsDir = Path.Combine(publishDir, "Powershell", prjName);
                Console.WriteLine($"Publishing Powershell to '{publishPsDir}'");
                await new RobocopyProcess(srcDir, publishPsDir)
                    .CopySubdirectoriesIncludingEmpty()
                    .NoDirectoryLogging()
                    .NoFileClassLogging()
                    .NoFileLogging()
                    .NoFileSizeLogging()
                    .NoJobHeader()
                    .NoJobSummary()
                    .NoProgressDisplayed()
                    .Purge()
                    .Run();
            }
        }
        var srcToolsDir = Path.Combine(Environment.CurrentDirectory, "Tools");
        if (Directory.Exists(srcToolsDir))
        {
            foreach (var srcDir in Directory.GetDirectories(srcToolsDir))
            {
                var prjName = new DirectoryInfo(srcDir).Name;
                var publishToolsDir = Path.Combine(publishDir, "Tools", prjName);
                Console.WriteLine($"Publishing tools to '{publishToolsDir}'");
                var publishProcess = new WinProcess("dotnet")
                    .WriteOutputToConsole()
                    .UseArgumentNameDelimiter("")
                    .AddArgument("publish")
                    .AddArgument(new Quoted(srcDir))
                    .UseArgumentNameDelimiter("-")
                    .AddArgument("c", "Release")
                    .UseArgumentValueDelimiter("=")
                    .AddArgument("p:PublishProfile", "Default")
                    .AddArgument("p:PublishDir", publishToolsDir)
                    .AddArgument("p:TypeScriptCompileBlocked", "true");
                var result = await publishProcess.Run();
                result.EnsureExitCodeIsZero();
            }
        }
    }
}
