using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalInstallService
{
    sealed class Installer
    {
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
                    var systemUserName = context.Request.Form["systemUserName"].FirstOrDefault() ?? "";
                    var systemPassword = context.Request.Form["systemPassword"].FirstOrDefault() ?? "";
                    var versionKey = context.Request.Form["versionKey"].FirstOrDefault() ?? "";
                    var repoOwner = context.Request.Form["repoOwner"].FirstOrDefault() ?? "";
                    var repoName = context.Request.Form["repoName"].FirstOrDefault() ?? "";
                    var release = context.Request.Form["release"].FirstOrDefault() ?? "";
                    var xtiDir = Environment.GetEnvironmentVariable("XTI_Dir");
                    if (string.IsNullOrWhiteSpace(xtiDir))
                    {
                        xtiDir = "c:\\xti";
                    }
                    var path = Path.Combine(xtiDir, "tools", "LocalInstallApp", "LocalInstallApp.exe");
                    var args = new StringBuilder();
                    args.Append($"--environment {envName}");
                    args.Append($" --AppName {appName} --AppType {appType}");
                    args.Append($" --VersionKey {versionKey}");
                    args.Append($" --SystemUserName {systemUserName} --SystemPassword {systemPassword}");
                    args.Append($" --RepoOwner {repoOwner} --RepoName {repoName}");
                    args.Append($" --Release {release}");
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
}
