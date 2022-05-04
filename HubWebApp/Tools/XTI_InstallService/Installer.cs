using XTI_Core;
using XTI_Processes;

namespace XTI_InstallService;

sealed class Installer
{
    private readonly XtiEnvironment xtiEnv;

    public Installer(XtiEnvironment xtiEnv)
    {
        this.xtiEnv = xtiEnv;
    }

    public async Task Run(HttpContext context)
    {
        var xtiFolder = new XtiFolder(xtiEnv);
        var appDataFolder = xtiFolder.AppDataFolder()
                .WithSubFolder("LocalInstallService");
        appDataFolder.TryCreate();
        using var writer = new StreamWriter(appDataFolder.FilePath($"log_{DateTime.Today:yyMMdd}.txt"), true);
        await writer.WriteLineAsync($"{DateTime.Now:HH:mm:ss} {context.Request.Method} {context.Request.Path}");
        try
        {
            if (context.Request.Method == "POST")
            {
                var command = context.Request.Form["command"].FirstOrDefault() ?? "";
                if (command.Equals("localinstall", StringComparison.OrdinalIgnoreCase))
                {
                    var envName = context.Request.Form["envName"].FirstOrDefault() ?? "";
                    var appName = context.Request.Form["appName"].FirstOrDefault() ?? "";
                    var appType = context.Request.Form["appType"].FirstOrDefault() ?? "";
                    var installationUserName = context.Request.Form["installationUserName"].FirstOrDefault() ?? "";
                    var installationPassword = context.Request.Form["installationPassword"].FirstOrDefault() ?? "";
                    var versionKey = context.Request.Form["versionKey"].FirstOrDefault() ?? "";
                    var domain = context.Request.Form["domain"].FirstOrDefault() ?? "";
                    var repoOwner = context.Request.Form["repoOwner"].FirstOrDefault() ?? "";
                    var repoName = context.Request.Form["repoName"].FirstOrDefault() ?? "";
                    var release = context.Request.Form["release"].FirstOrDefault() ?? "";
                    var machineName = context.Request.Form["machineName"].FirstOrDefault() ?? "";
                    var siteName = context.Request.Form["siteName"].FirstOrDefault() ?? "";
                    var adminToolPath = Path.Combine(xtiFolder.ToolsPath(), "XTI_AdminTool", "XTI_AdminTool.exe");
                    var process = new XtiProcess(adminToolPath)
                        .WriteOutputToConsole()
                        .UseEnvironment(envName)
                        .AddConfigOptions
                        (
                            new
                            {
                                Command = "Install",
                                AppName = appName,
                                AppType = appType,
                                VersionKey = versionKey,
                                InstallationUserName = installationUserName,
                                InstallationPassword = installationPassword,
                                Domain = domain,
                                RepoOwner = repoOwner,
                                RepoName = repoName,
                                Release = release,
                                DestinationMachine = machineName,
                                SiteName = siteName
                            }
                        );
                    await writer.WriteLineAsync($"Running {process.CommandText()}");
                    var result = await process.Run();
                    await writer.WriteLineAsync($"Ran {process.CommandText()}\r\nExit Code: {result.ExitCode}\r\nOutput:\r\n");
                    await writer.WriteLineAsync(string.Join("\r\n", result.OutputLines));
                    result.EnsureExitCodeIsZero();
                }
            }
            else
            {
                await context.Response.WriteAsync("Running");
            }
        }
        catch (Exception ex)
        {
            await writer.WriteLineAsync($"***Error***\r\n{ex}");
            throw;
        }
    }
}