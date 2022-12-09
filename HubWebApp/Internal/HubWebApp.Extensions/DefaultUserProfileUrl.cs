using XTI_HubWebAppApi;
using XTI_WebApp.Api;

namespace HubWebApp.Extensions;

internal sealed class DefaultUserProfileUrl : IUserProfileUrl
{
    private readonly HubAppApi api;

    public DefaultUserProfileUrl(HubAppApi api)
    {
        this.api = api;
    }

    public Task<string> Value() =>
        Task.FromResult(api.CurrentUser.Index.Path.Format());
}
