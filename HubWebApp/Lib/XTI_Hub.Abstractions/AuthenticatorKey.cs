using XTI_Core;

namespace XTI_Hub.Abstractions;

public sealed class AuthenticatorKey : TextKeyValue, IEquatable<AuthenticatorKey>
{
    public static readonly AuthenticatorKey Unknown = new AuthenticatorKey();

    public AuthenticatorKey() : this("Unknown")
    {
    }

    public AuthenticatorKey(string value) : base(value)
    {
    }

    public bool Equals(AuthenticatorKey? other) => _Equals(other);
}