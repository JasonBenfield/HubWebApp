namespace XTI_Hub.Abstractions;

public sealed class ExternalAuthKeyModel
{
    public ExternalAuthKeyModel()
        :this(new AuthenticatorKey(), "")
    {
    }

    public ExternalAuthKeyModel(AuthenticatorKey authenticatorKey, string externalUserKey)
    {
        AuthenticatorKey = authenticatorKey.DisplayText;
        ExternalUserKey = externalUserKey;
    }

    public string AuthenticatorKey { get; set; }
    public string ExternalUserKey { get; set; }
}
