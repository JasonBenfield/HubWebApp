﻿namespace XTI_HubWebAppApiActions.Home;

public sealed class IndexAction : AppAction<EmptyRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public IndexAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(EmptyRequest model, CancellationToken stoppingToken)=>
        Task.FromResult(viewFactory.Default("home", "Home"));
}