namespace XTI_Hub.Abstractions;

public sealed record UserAuthenticatorModel(AuthenticatorModel Authenticator, string ExternalUserID)
{
    public UserAuthenticatorModel()
        : this(new AuthenticatorModel(), "")
    {
    }
}
