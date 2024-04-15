using XTI_App.Abstractions;
using XTI_Core;
using XTI_Git.Abstractions;
using XTI_Processes;

namespace XTI_Admin;

public sealed class PublishLibProcess
{
    private readonly XtiEnvironment xtiEnv;
    private readonly XtiFolder xtiFolder;
    private readonly GitRepoInfo gitRepoInfo;
    private readonly IGitHubCredentialsAccessor credentialsAccessor;

    public PublishLibProcess(XtiEnvironment xtiEnv, XtiFolder xtiFolder, GitRepoInfo gitRepoInfo, IGitHubCredentialsAccessor credentialsAccessor)
    {
        this.xtiEnv = xtiEnv;
        this.xtiFolder = xtiFolder;
        this.gitRepoInfo = gitRepoInfo;
        this.credentialsAccessor = credentialsAccessor;
    }

    public async Task Run(AppKey appKey, AppVersionKey versionKey, string semanticVersion)
    {
        var libDir = Path.Combine(Environment.CurrentDirectory, "Lib");
        if (Directory.Exists(libDir))
        {
            var envName = xtiEnv.IsProduction() ?
                "Production" :
                "Development";
            var outputPath = Path.Combine
            (
                xtiFolder.FolderPath(),
                "Packages",
                envName
            );
            var repositoryUrl = gitRepoInfo.RepositoryUrl();
            var credentials = await credentialsAccessor.Value();
            var libDirs = Directory.GetDirectories(libDir);
            if (libDirs.Any())
            {
                Console.WriteLine("Packing Lib Projects");
                foreach (var dir in libDirs)
                {
                    var prjFiles = Directory.GetFiles(dir, "*.csproj");
                    if (!prjFiles.Any())
                    {
                        throw new Exception($"Project file not found in '{dir}'");
                    }
                    var packProcess = new WinProcess("dotnet")
                        .WriteOutputToConsole()
                        .UseArgumentNameDelimiter("")
                        .AddArgument("pack")
                        .AddArgument(dir)
                        .UseArgumentNameDelimiter("-")
                        .AddArgument("c", GetConfiguration())
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
    }

    private string GetConfiguration() =>
        xtiEnv.IsProduction() ? "Release" : "Debug";
}
