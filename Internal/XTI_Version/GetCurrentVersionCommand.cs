using XTI_Hub;
using XTI_VersionToolApi;

namespace XTI_Version;

public sealed class GetCurrentVersionCommand : VersionCommand
{
    private readonly AppFactory appFactory;

    public GetCurrentVersionCommand(AppFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task Execute(VersionToolOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.AppName)) { throw new ArgumentException("App Name is required"); }
        if (string.IsNullOrWhiteSpace(options.AppType)) { throw new ArgumentException("App Type is required"); }
        var app = await appFactory.Apps.App(options.AppKey());
        var currentVersion = await app.CurrentVersion();
        var output = new VersionOutput();
        output.Output(currentVersion.ToModel(), options.OutputPath);
    }
}