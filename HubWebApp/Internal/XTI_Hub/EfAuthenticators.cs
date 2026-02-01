using Microsoft.EntityFrameworkCore;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAuthenticators
{
    private readonly EfHubDB db;

    public EfAuthenticators(EfHubDB db)
    {
        this.db = db;
    }

    public async Task<AuthenticatorModel> AddOrUpdate(AuthenticatorKey authenticatorKey, CancellationToken ct)
    {
        var authenticator = await db.Context
            .Authenticators.Retrieve()
            .Where(auth => auth.AuthenticatorKey == authenticatorKey.Value)
            .FirstOrDefaultAsync(ct);
        if (authenticator == null)
        {
            authenticator = new AuthenticatorEntity
            {
                AuthenticatorKey = authenticatorKey.Value,
                AuthenticatorName = authenticatorKey.DisplayText
            };
            await db.Context.Authenticators.Create(authenticator, ct);
        }
        return new AuthenticatorModel(authenticator.ID, new AuthenticatorKey(authenticatorKey.DisplayText));
    }
}
