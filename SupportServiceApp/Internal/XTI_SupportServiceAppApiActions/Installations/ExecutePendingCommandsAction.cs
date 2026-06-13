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
        var pendingCommandSummaries = await hubService.GetPendingCommands
        (
            [AppCommandName.Delete, AppCommandName.Install],
            machineNames.ToArray(),
            ct
        );
        foreach (var pendingCommandSummary in pendingCommandSummaries)
        {
            await hubService.BeginCommand(pendingCommandSummary.App.AppKey, pendingCommandSummary.Command.ID, ct);
            try
            {
                if (pendingCommandSummary.Command.CommandName.Equals(AppCommandName.Delete))
                {
                    await DeleteApp(pendingCommandSummary, ct);
                }
                else if (pendingCommandSummary.Command.CommandName.Equals(AppCommandName.Install))
                {
                    var commandDetail = await hubService.GetInstallCommandDetail(pendingCommandSummary.App.AppKey, pendingCommandSummary.Command.ID, ct);
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
                await hubService.CommandEnded(pendingCommandSummary.App.AppKey, pendingCommandSummary.Command.ID, ct);
            }
            catch (AppCommandStepException)
            {
            }
        }
        return new EmptyActionResult();
    }

    private async Task DeleteApp(AppCommandSummaryModel pendingCommandSummary, CancellationToken ct)
    {
        var appKey = pendingCommandSummary.App.AppKey;
        var deleteCommandDetail = await hubService.GetDeleteCommandDetail(appKey, pendingCommandSummary.Command.ID, ct);
        await hubService.BeginDelete(appKey, deleteCommandDetail.Installation.ID, ct);
        var versionKey = deleteCommandDetail.Installation.IsCurrent
            ? AppVersionKey.Current
            : deleteCommandDetail.Version.VersionKey;
        if (appKey.IsAppType(AppType.Values.WebApp))
        {
            await RunStep
            (
                pendingCommandSummary,
                "Delete IIS Website",
                () =>
                {
                    var iisWebSite = new IisWebSite
                    (
                        xtiFolder,
                        xtiEnv,
                        appKey,
                        versionKey,
                        deleteCommandDetail.Installation.SiteName
                    );
                    return iisWebSite.Delete(ct);
                },
                ct
            );
        }
        else if
        (
            appKey.IsAppType(AppType.Values.ServiceApp) &&
            deleteCommandDetail.Installation.IsCurrent
        )
        {
            await RunStep
            (
                pendingCommandSummary,
                "Delete Windows Service",
                () =>
                {
                    var winService = new WinServiceInstallation(xtiFolder, xtiEnv, deleteCommandDetail.App.AppKey);
                    return winService.Delete();
                },
                ct
            );
        }
        var appFolder = xtiFolder.InstallPath(appKey, versionKey);
        if (Directory.Exists(appFolder))
        {
            await RunStep
            (
                pendingCommandSummary,
                "Delete App Folder",
                () =>
                {
                    Directory.Delete(appFolder, true);
                    return Task.CompletedTask;
                },
                ct
            );
        }
        await hubService.Deleted(appKey, deleteCommandDetail.Installation.ID, ct);
    }

    private async Task RunStep(AppCommandSummaryModel commandSummary, string activity, Func<Task> action, CancellationToken ct)
    {
        var appKey = commandSummary.App.AppKey;
        var step = await hubService.BeginCommandStep(appKey, commandSummary.Command.ID, activity, ct);
        try
        {
            await action();
            await hubService.CommandStepEnded(appKey, step.ID, "", ct);
        }
        catch (Exception ex)
        {
            await hubService.CommandStepEnded(appKey, step.ID, ex.ToString(), ct);
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