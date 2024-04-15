using XTI_Core;

namespace XTI_HubWebAppApi.AppInquiry;

internal sealed class GetDefaultOptionsAction : AppAction<EmptyRequest, string>
{
    private readonly AppFromPath appFromPath;

    public GetDefaultOptionsAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<string> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var appModel = app.ToModel();
        string defaultOptions;
        if (appModel.AppKey.IsAppType(AppType.Values.WebApp))
        {
            defaultOptions = XtiSerializer.Serialize(new DefaultWebAppOptions());
        }
        else if (appModel.AppKey.IsAppType(AppType.Values.ConsoleApp))
        {
            defaultOptions = XtiSerializer.Serialize(new DefaultConsoleAppOptions());
        }
        else if (appModel.AppKey.IsAppType(AppType.Values.ServiceApp))
        {
            defaultOptions = XtiSerializer.Serialize(new DefaultServiceAppOptions());
        }
        else
        {
            defaultOptions = "";
        }
        return defaultOptions;
    }
}
