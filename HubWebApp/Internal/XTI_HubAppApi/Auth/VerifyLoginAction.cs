using System.Text.Json;
using XTI_Core;

namespace XTI_HubAppApi.Auth;

public sealed class VerifyLoginAction : AppAction<VerifyLoginForm, string>
{
    private readonly UnverifiedUser unverifiedUser;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly HubFactory hubFactory;
    private readonly IClock clock;

    public VerifyLoginAction(UnverifiedUser unverifiedUser, IHashedPasswordFactory hashedPasswordFactory, HubFactory hubFactory, IClock clock)
    {
        this.unverifiedUser = unverifiedUser;
        this.hashedPasswordFactory = hashedPasswordFactory;
        this.hubFactory = hubFactory;
        this.clock = clock;
    }

    public async Task<string> Execute(VerifyLoginForm model)
    {
        var userName = model.UserName.Value() ?? "";
        var password = model.Password.Value() ?? "";
        var hashedPassword = hashedPasswordFactory.Create(password);
        await unverifiedUser.Verify(new AppUserName(userName), hashedPassword);
        var authKey = await new StoreObjectProcess(hubFactory)
            .Run
            (
                new StorageName("XTI Authenticated"),
                GeneratedStorageKeyType.Values.SixDigit,
                JsonSerializer.Serialize(new AuthenticatedModel {  UserName = userName }),
                clock.Now().AddMinutes(30)
            );
        return authKey;
    }
}