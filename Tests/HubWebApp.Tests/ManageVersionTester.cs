using HubWebApp.Fakes;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using XTI_Core.Fakes;
using XTI_Git;
using XTI_Git.Abstractions;
using XTI_Git.Fakes;
using XTI_GitHub;
using XTI_GitHub.Fakes;
using XTI_Secrets.Fakes;
using XTI_Version;
using XTI_VersionToolApi;

namespace HubWebApp.Tests;

internal sealed class ManageVersionTester
{
    private IServiceProvider? sp;
    private App? app;

    private IServiceProvider SP { get => sp ?? throw new ArgumentNullException(nameof(sp)); }

    public VersionToolOptions Options { get; private set; } = new VersionToolOptions();

    public App App { get => app ?? throw new ArgumentNullException(nameof(app)); }

    public async Task Setup()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices
            (
                (hostContext, services) =>
                {
                    services.AddFakesForHubWebApp(hostContext.Configuration);
                    services.AddScoped(sp => sp.GetRequiredService<AppApiFactory>().CreateForSuperUser());
                    services.AddScoped<IXtiGitFactory, FakeGitFactory>();
                    services.AddScoped<IGitHubFactory, FakeGitHubFactory>();
                    services.AddScoped<IOptions<VersionToolOptions>, FakeOptions<VersionToolOptions>>();
                    services.AddScoped<VersionGitFactory>();
                    services.AddScoped<VersionCommandFactory>();
                    services.AddFakeSecretCredentials();
                }
            )
            .Build();
        var scope = host.Services.CreateScope();
        sp = scope.ServiceProvider;
        var setup = SP.GetRequiredService<IAppSetup>();
        await setup.Run(AppVersionKey.Current);
        var appFactory = SP.GetRequiredService<AppFactory>();
        app = await appFactory.Apps.App(HubInfo.AppKey);
        Options = sp.GetRequiredService<IOptions<VersionToolOptions>>().Value;
        Options.CommandNewVersion
        (
            HubInfo.AppKey.Name.Value,
            HubInfo.AppKey.Type.DisplayText,
            "",
            AppVersionType.Values.Patch.DisplayText,
            "JasonBenfield",
            "XTI_App"
        );
        var gitFactory = SP.GetRequiredService<VersionGitFactory>();
        var gitHubRepo = gitFactory.CreateGitHubRepo();
        var repoInfo = await gitHubRepo.RepositoryInformation();
        var gitRepo = gitFactory.CreateGitRepo();
        await gitRepo.CheckoutBranch(repoInfo.DefaultBranch);
    }

    public Task Execute()
    {
        var command = Command();
        return command.Execute(Options);
    }

    public VersionCommand Command()
    {
        var commandFactory = SP.GetRequiredService<VersionCommandFactory>();
        var commandName = VersionCommandName.FromValue(Options.Command);
        var command = commandFactory.Create(commandName);
        return command;
    }

    public async Task Checkout(AppVersion version)
    {
        var gitFactory = SP.GetRequiredService<VersionGitFactory>();
        var gitRepo = gitFactory.CreateGitRepo();
        var versionBranchName = new XtiVersionBranchName(new XtiGitVersion(version.Type().DisplayText, version.Key().DisplayText));
        await gitRepo.CheckoutBranch(versionBranchName.Value);
    }
}