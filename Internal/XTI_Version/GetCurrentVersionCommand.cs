using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub;
using XTI_VersionToolApi;

namespace XTI_Version;

public sealed class GetCurrentVersionCommand : VersionCommand
{
    private readonly AppFactory appFactory;
    private readonly IClock clock;

    public GetCurrentVersionCommand(AppFactory appFactory, IClock clock)
    {
        this.appFactory = appFactory;
        this.clock = clock;
    }

    public async Task Execute(VersionToolOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.AppName)) { throw new ArgumentException("App Name is required"); }
        if (string.IsNullOrWhiteSpace(options.AppType)) { throw new ArgumentException("App Type is required"); }
        var appKey = options.AppKey();
        App app;
        if (string.IsNullOrWhiteSpace(options.Domain) && appKey.Type.Equals(AppType.Values.WebApp))
        {
            app = await appFactory.Apps.App(appKey);
        }
        else
        {
            app = await appFactory.Apps.AddOrUpdate(appKey, options.Domain, clock.Now());
        }
        var currentVersion = await app.CurrentVersion();
        var output = new VersionOutput();
        output.Output(currentVersion.ToModel(), options.OutputPath);
    }
}