using System.Net.NetworkInformation;
using XTI_App.Extensions;
using XTI_Core;
using XTI_GitHub;
using XTI_Hub.Abstractions;
using XTI_Installation;
using XTI_Internal.Abstractions;
using XTI_Internal.Implementations;

namespace XTI_SupportServiceAppApiActions.Installations;

public sealed class ExecutePendingCommandsAction : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly XtiFolder xtiFolder;
    private readonly XtiEnvironment xtiEnv;
    private readonly IHubService hubService;
    private readonly InstallAppProcessFactory installAppProcessFactory;
    private readonly IGitHubFactory gitHubFactory;

    public ExecutePendingCommandsAction(XtiFolder xtiFolder, XtiEnvironment xtiEnv, IHubService hubService, InstallAppProcessFactory installAppProcessFactory, IGitHubFactory gitHubFactory)
    {
        this.xtiFolder = xtiFolder;
        this.xtiEnv = xtiEnv;
        this.hubService = hubService;
        this.installAppProcessFactory = installAppProcessFactory;
        this.gitHubFactory = gitHubFactory;
    }

    public async Task<EmptyActionResult> Execute(EmptyRequest model, CancellationToken ct)
    {
        var domain = IPGlobalProperties.GetIPGlobalProperties().DomainName;
        var machineNames = new List<string> { Environment.MachineName };
        if (!string.IsNullOrWhiteSpace(domain))
        {
            machineNames.Add($"{Environment.MachineName}.{domain}");
        }
        var pendingCommands = await hubService.GetPendingCommands
        (
            [AppCommandName.Delete, AppCommandName.Install],
            machineNames.ToArray(),
            ct
        );
        foreach (var pendingCommand in pendingCommands)
        {
            await hubService.BeginCommand(pendingCommand.ID, ct);
            if (pendingCommand.CommandName.Equals(AppCommandName.Delete))
            {
                await DeleteApp(pendingCommand, ct);
            }
            else if (pendingCommand.CommandName.Equals(AppCommandName.Install))
            {
                var commandDetail = await hubService.GetInstallCommandDetail(pendingCommand.ID, ct);
                using (var publishedAssets = CreatePublishedAssets(commandDetail.InstallConfiguration.RepoOwner, commandDetail.InstallConfiguration.RepoName))
                {
                    var installFromRequestProcess = new InstallFromRequestProcess
                    (
                        hubService: hubService,
                        publishedAssets: publishedAssets,
                        installFactory: installAppProcessFactory,
                        xtiEnv: xtiEnv,
                        xtiFolder: xtiFolder
                    );
                    await installFromRequestProcess.Run(commandDetail, ct);
                }
            }
            await hubService.CommandEnded(pendingCommand.ID, ct);
        }
        return new EmptyActionResult();
    }

    private async Task DeleteApp(AppCommandModel pendingCommand, CancellationToken ct)
    {
        var deleteCommandDetail = await hubService.GetDeleteCommandDetail(pendingCommand.ID, ct);
        await hubService.BeginDelete(deleteCommandDetail.Installation.ID, ct);
        var versionKey = deleteCommandDetail.Installation.IsCurrent
            ? AppVersionKey.Current
            : deleteCommandDetail.Version.VersionKey;
        if (deleteCommandDetail.App.AppKey.IsAppType(AppType.Values.WebApp))
        {
            var iisWebSite = new IisWebSite
            (
                xtiFolder,
                xtiEnv,
                deleteCommandDetail.App.AppKey,
                versionKey,
                deleteCommandDetail.Installation.SiteName
            );
            await RunStep(pendingCommand, "Delete IIS Website", () => iisWebSite.Delete(ct), ct);
        }
        else if
        (
            deleteCommandDetail.App.AppKey.IsAppType(AppType.Values.ServiceApp) &&
            deleteCommandDetail.Installation.IsCurrent
        )
        {
            var winService = new WinServiceInstallation(xtiFolder, xtiEnv, deleteCommandDetail.App.AppKey);
            await RunStep(pendingCommand, "Delete Windows Service", () => winService.Delete(), ct);
        }
        var appFolder = xtiFolder.InstallPath(deleteCommandDetail.App.AppKey, versionKey);
        if (Directory.Exists(appFolder))
        {
            await RunStep
            (
                pendingCommand,
                "Delete App Folder",
                () =>
                {
                    Directory.Delete(appFolder, true);
                    return Task.CompletedTask;
                },
                ct
            );
        }
        await hubService.Deleted(deleteCommandDetail.Installation.ID, ct);
    }

    private async Task RunStep(AppCommandModel command, string activity, Func<Task> action, CancellationToken ct)
    {
        var step = await hubService.BeginCommandStep(command.ID, activity, ct);
        try
        {
            await action();
            await hubService.CommandStepEnded(step.ID, "", ct);
        }
        catch (Exception ex)
        {
            await hubService.CommandStepEnded(step.ID, ex.ToString(), ct);
            throw new AppCommandStepException(step);
        }
    }

    private IPublishedAssets CreatePublishedAssets(string repoOwner, string repoName)
    {
        var gitHubRepo = gitHubFactory.CreateGitHubRepository(repoOwner, repoName);
        return new GitHubPublishedAssets
        (
            gitHubRepo,
            "xti_support_installation"
        );
    }
}