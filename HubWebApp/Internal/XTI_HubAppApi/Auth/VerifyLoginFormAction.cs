namespace XTI_HubAppApi.Auth;

public sealed class VerifyLoginFormAction : AppAction<EmptyRequest, WebPartialViewResult>
{
    public Task<WebPartialViewResult> Execute(EmptyRequest model)
        => Task.FromResult(new WebPartialViewResult("VerifyLoginForm"));
}