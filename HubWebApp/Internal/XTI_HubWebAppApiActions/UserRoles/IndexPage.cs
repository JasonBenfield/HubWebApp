﻿namespace XTI_HubWebAppApiActions.UserRoles;

public sealed class IndexPage : AppAction<UserRoleQueryRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public IndexPage(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(UserRoleQueryRequest model, CancellationToken ct) =>
        Task.FromResult(viewFactory.Default("userRoles", "User Roles"));
}