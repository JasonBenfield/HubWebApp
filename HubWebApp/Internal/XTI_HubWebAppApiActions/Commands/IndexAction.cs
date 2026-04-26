namespace XTI_HubWebAppApiActions.Commands;

public sealed class IndexAction : AppAction<AppCommandIDRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public IndexAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(AppCommandIDRequest requestData, CancellationToken stoppingToken)=>
        Task.FromResult(viewFactory.Default("commands", "Commands"));
}