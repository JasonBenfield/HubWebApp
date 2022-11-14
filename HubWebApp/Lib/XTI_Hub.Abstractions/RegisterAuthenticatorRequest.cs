namespace XTI_Hub.Abstractions;

public sealed class RegisterAuthenticatorRequest
{
    public RegisterAuthenticatorRequest()
        : this(AuthenticatorKey.Unknown)
    {
    }

    public RegisterAuthenticatorRequest(AuthenticatorKey authenticatorKey)
    {
        AuthenticatorName = authenticatorKey.DisplayText;
    }

    public string AuthenticatorName { get; set; }
}
