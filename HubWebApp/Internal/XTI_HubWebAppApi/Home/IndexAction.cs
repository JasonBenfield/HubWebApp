using Microsoft.Extensions.Hosting;

namespace XTI_HubWebAppApi.Home;

public sealed class IndexAction : AppAction<EmptyRequest, WebViewResult>
{
    private readonly IHostEnvironment hostEnv;
    private readonly WebViewResultFactory viewFactory;

    public IndexAction(IHostEnvironment hostEnv, WebViewResultFactory viewFactory)
    {
        this.hostEnv = hostEnv;
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        File.WriteAllText("c:\\xti\\test241127.txt", $"ContentRootPath: {hostEnv.ContentRootPath}");
        return Task.FromResult(viewFactory.Default("home", "Home"));
    }
}