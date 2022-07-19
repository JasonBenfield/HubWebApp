// Generated Code
namespace XTI_AuthenticatorAppClient;
public sealed partial class UserGroup : AppClientGroup
{
    public UserGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "User")
    {
        Actions = new UserActions(clientUrl);
    }

    public UserActions Actions { get; }

    public Task<ResourcePathAccess[]> GetUserAccess(ResourcePath[] model) => Post<ResourcePathAccess[], ResourcePath[]>("GetUserAccess", "", model);
}