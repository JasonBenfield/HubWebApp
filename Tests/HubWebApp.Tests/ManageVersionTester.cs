using HubWebApp.Fakes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Git.Abstractions;
using XTI_Hub;
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
                    services.AddScoped<GitFactory, FakeGitFactory>();
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
        Options = new VersionToolOptions();
        Options.CommandNewVersion
        (
            HubInfo.AppKey.Name.Value,
            HubInfo.AppKey.Type.DisplayText,
            "",
            AppVersionType.Values.Patch.DisplayText,
            "JasonBenfield",
            "XTI_App"
        );
        var gitFactory = SP.GetRequiredService<GitFactory>();
        var gitHubRepo = await gitFactory.CreateGitHubRepo("JasonBenfield", "XTI_App");
        var defaultBranchName = await gitHubRepo.DefaultBranchName();
        var gitRepo = await gitFactory.CreateGitRepo();
        gitRepo.CheckoutBranch(defaultBranchName);
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
        var gitFactory = SP.GetRequiredService<GitFactory>();
        var gitRepo = await gitFactory.CreateGitRepo();
        var versionBranchName = new XtiVersionBranchName(new XtiGitVersion(version.Type().DisplayText, version.Key().DisplayText));
        gitRepo.CheckoutBranch(versionBranchName.Value);
    }
}