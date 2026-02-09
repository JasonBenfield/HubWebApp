using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Hub.Abstractions;
using XTI_Internal.Abstractions;

namespace XTI_Installation;

public sealed class InstallFromRequestProcess
{
    private readonly IHubService hubService;
    private readonly IPublishedAssets publishedAssets;
    private readonly InstallAppProcessFactory installFactory;
    private readonly XtiEnvironment xtiEnv;
    private readonly XtiFolder xtiFolder;

    public InstallFromRequestProcess(IHubService hubService, IPublishedAssets publishedAssets, InstallAppProcessFactory installFactory, XtiEnvironment xtiEnv, XtiFolder xtiFolder)
    {
        this.hubService = hubService;
        this.publishedAssets = publishedAssets;
        this.installFactory = installFactory;
        this.xtiEnv = xtiEnv;
        this.xtiFolder = xtiFolder;
    }

    public async Task Run(AppInstallCommandDetailModel installCommandDetail, CancellationToken ct)
    {
        var requestedInstallation = new RequestedInstallation(hubService, installCommandDetail);
        requestedInstallation.SetIsCurrent(false);
        var appKey = installCommandDetail.App.AppKey;
        var versionKey = AppVersionKey.Current;
        if (xtiEnv.IsProduction())
        {
            versionKey = installCommandDetail.Version.VersionKey;
        }
        var release = installCommandDetail.GetRelease();
        var setupAppPath = await requestedInstallation.RunStep
        (
            "Download Setup App",
            () => publishedAssets.LoadSetup(release, appKey, versionKey, ct),
            ct
        );
        var publishedAppPath = await requestedInstallation.RunStep
        (
            "Download App",
            () => publishedAssets.LoadApps(release, appKey, versionKey, ct),
            ct
        );
        var versionName = installCommandDetail.Version.VersionName;
        await requestedInstallation.RunStep
        (
            "Run Setup",
            () => new RunSetupProcess(xtiEnv).Run
            (
                versionName: versionName,
                appKey: appKey,
                versionKey: versionKey,
                repoOwner: installCommandDetail.App.RepoOwner,
                repoName: installCommandDetail.App.RepoName,
                setupAppDir: setupAppPath
            ),
            ct
        );
        var installAppProcess = installFactory.Create(appKey);
        if (xtiEnv.IsProduction())
        {
            var versionInstallation = await hubService.BeginInstallation(installCommandDetail.Command.ID, isCurrent: false, ct: ct);
            await installAppProcess.Run(publishedAppPath, requestedInstallation, ct);
            await hubService.Installed(versionInstallation.ID, ct);
            await WriteInstallationID(versionInstallation.ID, requestedInstallation, ct);
        }
        if (installCommandDetail.InstallRequest.InstallAsCurrent)
        {
            requestedInstallation.SetIsCurrent(true);
            var currentInstallation = await hubService.BeginInstallation(installCommandDetail.InstallConfiguration.ID, isCurrent: true, ct: ct);
            await installAppProcess.Run(publishedAppPath, requestedInstallation, ct);
            await hubService.Installed(currentInstallation.ID, ct);
            await WriteInstallationID(currentInstallation.ID, requestedInstallation, ct);
        }
    }

    private Task WriteInstallationID(int installationID, RequestedInstallation requestedInstallation, CancellationToken ct) =>
        requestedInstallation.RunStep
        (
            "Write Installation ID",
            async () =>
            {
                var serializedInstallationID = XtiSerializer.Serialize
                (
                    new { InstallationID = installationID }
                );
                var installDir = xtiFolder.InstallPath(requestedInstallation.AppKey, requestedInstallation.VersionKey);
                using var writer = new StreamWriter(Path.Combine(installDir, "installation.json"), false);
                await writer.WriteAsync(serializedInstallationID);
            },
            ct
        );
}
