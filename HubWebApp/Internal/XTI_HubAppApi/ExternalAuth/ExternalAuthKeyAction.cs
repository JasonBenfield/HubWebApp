using System.Text.Json;
using XTI_Core;

namespace XTI_HubAppApi.ExternalAuth;

internal sealed class ExternalAuthKeyAction : AppAction<ExternalAuthKeyModel, string>
{
    private readonly HubFactory hubFactory;
    private readonly IClock clock;

    public ExternalAuthKeyAction(HubFactory hubFactory, IClock clock)
    {
        this.hubFactory = hubFactory;
        this.clock = clock;
    }

    public async Task<string> Execute(ExternalAuthKeyModel model)
    {
        var app = await hubFactory.Apps.App(model.AppKey);
        var user = await hubFactory.Users.UserByExternalKey(app, model.ExternalUserKey);
        var authKey = await new StoreObjectProcess(hubFactory)
            .Run
            (
                new StorageName("XTI Authenticated"),
                GeneratedStorageKeyType.Values.SixDigit,
                JsonSerializer.Serialize(new AuthenticatedModel { UserName = user.UserName().Value }),
                clock.Now().AddMinutes(30)
            );
        return authKey;
    }
}
