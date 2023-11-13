using XTI_Core;
using XTI_Processes;

namespace XTI_AdminService;

internal sealed class AdminToolRunner
{
    private readonly XtiEnvironment xtiEnv;

    public AdminToolRunner(XtiEnvironment xtiEnv)
    {
        this.xtiEnv = xtiEnv;
    }

    public async Task Run(HttpContext context)
    {
        var xtiFolder = new XtiFolder(xtiEnv);
        var appDataFolder = xtiFolder.AppDataFolder()
            .WithSubFolder("AdminService");
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
                    await RunLocalInstall(context, xtiFolder, writer, command);
                }
                else if (command.Equals("UploadTempLog", StringComparison.OrdinalIgnoreCase))
                {
                    await UploadTempLog(context, xtiFolder, writer, command);
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

    private static async Task RunLocalInstall(HttpContext context, XtiFolder xtiFolder, StreamWriter writer, string command)
    {
        var envName = context.Request.Form["envName"].FirstOrDefault() ?? "";
        var remoteInstallKey = context.Request.Form["RemoteInstallKey"].FirstOrDefault() ?? "";
        var hubAdministrationType = context.Request.Form["HubAdministrationType"].FirstOrDefault() ?? "Default";
        var hubAppVersionKey = context.Request.Form["HubAppVersionKey"].FirstOrDefault() ?? "";
        var installationSource = context.Request.Form["InstallationSource"].FirstOrDefault() ?? "Default";
        var adminToolPath = GetAdminToolPath(xtiFolder);
        var process = new XtiProcess(adminToolPath)
            .WriteOutputToConsole()
            .UseEnvironment(envName)
            .AddConfigOptions
            (
                new
                {
                    Command = command,
                    RemoteInstallKey = remoteInstallKey,
                    HubAdministrationType = hubAdministrationType,
                    HubAppVersionKey = hubAppVersionKey,
                    InstallationSource = installationSource
                }
            );
        await writer.WriteLineAsync($"Running {process.CommandText()}");
        var result = await process.Run();
        await writer.WriteLineAsync($"Ran {process.CommandText()}\r\nExit Code: {result.ExitCode}\r\nOutput:\r\n");
        await writer.WriteLineAsync(string.Join("\r\n", result.OutputLines));
        result.EnsureExitCodeIsZero();
    }

    private static async Task UploadTempLog(HttpContext context, XtiFolder xtiFolder, StreamWriter writer, string command)
    {
        var envName = context.Request.Form["envName"].FirstOrDefault() ?? "";
        var hubAdministrationType = context.Request.Form["HubAdministrationType"].FirstOrDefault() ?? "Default";
        var adminToolPath = GetAdminToolPath(xtiFolder);
        var process = new XtiProcess(adminToolPath)
            .WriteOutputToConsole()
            .UseEnvironment(envName)
            .AddConfigOptions
            (
                new
                {
                    Command = command,
                    HubAdministrationType = hubAdministrationType
                }
            );
        await writer.WriteLineAsync($"Running {process.CommandText()}");
        var result = await process.Run();
        await writer.WriteLineAsync($"Ran {process.CommandText()}\r\nExit Code: {result.ExitCode}\r\nOutput:\r\n");
        await writer.WriteLineAsync(string.Join("\r\n", result.OutputLines));
        result.EnsureExitCodeIsZero();
    }

    private static string GetAdminToolPath(XtiFolder xtiFolder)
    {
        return Path.Combine(xtiFolder.ToolsPath(), "XTI_AdminTool", "XTI_AdminTool.exe");
    }
}