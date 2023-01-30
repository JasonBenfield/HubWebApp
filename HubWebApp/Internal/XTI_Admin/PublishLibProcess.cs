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

    public async Task Run(AppKey appKey, AppVersionKey versionKey, string semanticVersion)
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
            var gitRepoInfo = scopes.GetRequiredService<GitRepoInfo>();
            var repositoryUrl = gitRepoInfo.RepositoryUrl();
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
                    .AddArgument("p:PackageVersion", semanticVersion)
                    .AddArgument("p:RepositoryUrl", repositoryUrl)
                    .AddArgument("p:XtiAppName", new Quoted(appKey.Name.DisplayText))
                    .AddArgument("p:XtiAppType", new Quoted(appKey.Type.DisplayText))
                    .AddArgument("p:XtiAppKey", new Quoted(appKey.Format()))
                    .AddArgument("p:XtiVersion", new Quoted(versionKey.DisplayText));
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
                    var publishProcess = new WinProcess("dotnet")
                        .WriteOutputToConsole()
                        .UseArgumentNameDelimiter("")
                        .AddArgument("nuget")
                        .AddArgument("push")
                        .AddArgument(new Quoted(Path.Combine(outputPath, $"{new DirectoryInfo(dir).Name}.{semanticVersion}.nupkg")))
                        .UseArgumentNameDelimiter("--")
                        .AddArgument("api-key", credentials.Password)
                        .AddArgument("source", $"https://nuget.pkg.github.com/{gitRepoInfo.RepoOwner}/index.json")
                        .AddArgument("skip-duplicate");
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
