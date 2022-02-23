using XTI_App.Abstractions;
using XTI_Core;
using XTI_Git.Abstractions;
using XTI_Processes;

namespace XTI_Admin;

internal sealed class PublishLibProcess
{
    private readonly Scopes scopes;

    public PublishLibProcess(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Run(string semanticVersion)
    {
        Console.WriteLine("Packing Lib Projects");
        var libDir = Path.Combine(Environment.CurrentDirectory, "Lib");
        if (Directory.Exists(libDir))
        {
            var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
            var xtiFolder = scopes.GetRequiredService<XtiFolder>();
            var envName = xtiEnv.IsProduction()
                ? "Production"
                : "Development";
            var outputPath = Path.Combine
            (
                xtiFolder.FolderPath(),
                "Packages",
                envName
            );
            var credentialsAccessor = scopes.GetRequiredService<IGitHubCredentialsAccessor>();
            var credentials = await credentialsAccessor.Value();
            foreach (var dir in Directory.GetDirectories(libDir))
            {
                var packProcess = new WinProcess("dotnet")
                    .WriteOutputToConsole()
                    .UseArgumentNameDelimiter("")
                    .AddArgument("pack")
                    .AddArgument(dir)
                    .UseArgumentNameDelimiter("-")
                    .AddArgument("c", getConfiguration())
                    .AddArgument("o", new Quoted(outputPath))
                    .UseArgumentValueDelimiter("=")
                    .AddArgument("p:PackageVersion", semanticVersion);
                if (!xtiEnv.IsProduction())
                {
                    packProcess
                        .UseArgumentNameDelimiter("--")
                        .AddArgument("include-source")
                        .AddArgument("include-symbols");
                }
                var packResult = await packProcess.Run();
                packResult.EnsureExitCodeIsZero();
                if (xtiEnv.IsProduction())
                {
                    var options = scopes.GetRequiredService<AdminOptions>();
                    var publishProcess = new WinProcess("dotnet")
                        .WriteOutputToConsole()
                        .UseArgumentNameDelimiter("")
                        .AddArgument("nuget")
                        .AddArgument("push")
                        .AddArgument(new Quoted(Path.Combine(outputPath, $"{new DirectoryInfo(dir).Name}.{semanticVersion}.nupkg")))
                        .UseArgumentNameDelimiter("--")
                        .AddArgument("api-key", credentials.Password)
                        .AddArgument("source", $"https://nuget.pkg.github.com/{options.RepoOwner}/index.json");
                    var publishResult = await publishProcess.Run();
                    publishResult.EnsureExitCodeIsZero();
                }
            }
        }
    }

    private string getConfiguration()
    {
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        return xtiEnv.IsProduction()
            ? "Release"
            : "Debug";
    }
}
