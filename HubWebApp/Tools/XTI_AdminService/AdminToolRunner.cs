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
                await RunAdminTool(context, xtiFolder, writer);
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

    private static async Task RunAdminTool(HttpContext context, XtiFolder xtiFolder, StreamWriter writer)
    {
        var envName = context.Request.Form["envName"].FirstOrDefault() ?? "";
        var optionsKey = context.Request.Form["OptionsKey"].FirstOrDefault() ?? "";
        var hubAdminType = context.Request.Form["HubAdministrationType"].FirstOrDefault() ?? "";
        var adminToolPath = GetAdminToolPath(xtiFolder);
        var process = new XtiProcess(adminToolPath)
            .WriteOutputToConsole()
            .UseEnvironment(envName)
            .AddConfigOptions
            (
                new
                {
                    Command = "FromRemote",
                    RemoteOptionsKey = optionsKey,
                    HubAdministrationType = hubAdminType
                }
            );
        await writer.WriteLineAsync($"[{DateTime.Now:HH:mm:ss}] Running {process.CommandText()}");
        await writer.FlushAsync();
        var result = await process.Run();
        await writer.WriteLineAsync($"[{DateTime.Now:HH:mm:ss}] Ran {process.CommandText()}\r\nExit Code: {result.ExitCode}\r\nOutput:\r\n");
        await writer.WriteLineAsync(string.Join("\r\n", result.OutputLines));
        await writer.WriteLineAsync("");
        await writer.FlushAsync();
        result.EnsureExitCodeIsZero();
    }

    private static string GetAdminToolPath(XtiFolder xtiFolder) =>
        Path.Combine(xtiFolder.ToolsPath(), "XTI_AdminTool", "XTI_AdminTool.exe");
}