using XTI_App.Abstractions;
using XTI_Core;
using XTI_Processes;

namespace XTI_Installation;

public sealed class RunSetupProcess
{
    private readonly XtiEnvironment xtiEnv;

    public RunSetupProcess(XtiEnvironment xtiEnv)
    {
        this.xtiEnv = xtiEnv;
    }

    public async Task Run(AppVersionName versionName, AppKey appKey, AppVersionKey versionKey, string repoOwner, string repoName, string setupAppDir)
    {
        var appName = appKey.Name.DisplayText.Replace(" ", "");
        var setupResult = await new XtiProcess(Path.Combine(setupAppDir, $"{appName}SetupApp.exe"))
            .UseEnvironment(xtiEnv.EnvironmentName)
            .WriteOutputToConsole()
            .AddConfigOptions
            (
                new
                {
                    VersionName = versionName.Value,
                    VersionKey = versionKey.Value,
                    RepoOwner = repoOwner,
                    RepoName = repoName
                }
            )
            .Run();
        setupResult.EnsureExitCodeIsZero();
    }
}
