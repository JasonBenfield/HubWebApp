﻿namespace XTI_HubWebAppApi.Logs;

internal sealed class AppRequestViewAction : AppAction<AppRequestRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public AppRequestViewAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(AppRequestRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("request", "Request"));
}
