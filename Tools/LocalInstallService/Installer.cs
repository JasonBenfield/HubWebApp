using System.Diagnostics;
using System.Text;
using XTI_Core;

namespace LocalInstallService;

sealed class Installer
{
    private readonly IHostEnvironment hostEnv;

    public Installer(IHostEnvironment hostEnv)
    {
        this.hostEnv = hostEnv;
    }

    public async Task Run(HttpContext context)
    {
        if (context.Request.Method == "POST")
        {
            var command = context.Request.Form["command"].FirstOrDefault() ?? "";
            if (command.Equals("install", StringComparison.OrdinalIgnoreCase))
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
                var xtiFolder = new XtiFolder(hostEnv);
                var path = Path.Combine(xtiFolder.ToolsPath(), "LocalInstallApp", "LocalInstallApp.exe");
                var args = new StringBuilder();
                args.Append($"--environment {envName}");
                args.Append($" --AppName {appName} --AppType {appType}");
                args.Append($" --VersionKey {versionKey}");
                args.Append($" --InstallationUserName {installationUserName} --InstallationPassword {installationPassword}");
                args.Append($" --Domain {domain}");
                args.Append($" --RepoOwner {repoOwner} --RepoName {repoName}");
                args.Append($" --Release {release}");
                args.Append($" --MachineName {machineName}");
                Process.Start(path, args.ToString());
                await context.Response.WriteAsync($"Ran {path} {args}");
            }
        }
        else
        {
            await context.Response.WriteAsync("Running");
        }
    }
}