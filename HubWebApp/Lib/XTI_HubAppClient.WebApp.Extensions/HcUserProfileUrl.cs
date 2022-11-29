using XTI_WebApp.Api;

namespace XTI_HubAppClient.WebApp.Extensions;

internal sealed class HcUserProfileUrl : IUserProfileUrl
{
    private readonly HubAppClient hubClient;

    public HcUserProfileUrl(HubAppClient hubClient)
    {
        this.hubClient = hubClient;
    }

    public Task<string> Value()=>
        hubClient.CurrentUser.Actions.Index.Url();
}
