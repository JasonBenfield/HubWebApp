using XTI_Core;

namespace XTI_AuthenticatorWebAppApi;

partial class AuthenticatorAppApiBuilder
{
    partial void Configure()
    {
        source.SerializedDefaultOptions = XtiSerializer.Serialize(new AuthenticatorWebAppOptions());
    }
}
