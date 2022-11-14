using Microsoft.EntityFrameworkCore;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AuthenticatorRepository
{
    private readonly HubFactory hubFactory;

    public AuthenticatorRepository(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AuthenticatorModel> AddOrUpdate(AuthenticatorKey authenticatorKey)
    {
        var authenticator = await hubFactory.DB
            .Authenticators.Retrieve()
            .Where(auth => auth.AuthenticatorKey == authenticatorKey.Value)
            .FirstOrDefaultAsync();
        if (authenticator == null)
        {
            authenticator = new AuthenticatorEntity
            {
                AuthenticatorKey = authenticatorKey.Value,
                AuthenticatorName = authenticatorKey.DisplayText
            };
            await hubFactory.DB.Authenticators.Create(authenticator);
        }
        return new AuthenticatorModel(authenticator.ID, new AuthenticatorKey(authenticatorKey.DisplayText));
    }
}
