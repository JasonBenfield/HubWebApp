using XTI_App.Api;
using XTI_Hub.Abstractions;

namespace XTI_Hub;

public sealed class ExternalUserNotFoundException : AppException
{
    public ExternalUserNotFoundException(AuthenticatorKey authenticatorKey, string externalUserKey) :
        base
        (
            "External User not found", 
            $"User not found for authenticator '{authenticatorKey.DisplayText}' with user key '{externalUserKey}'"
        )
    {
    }
}
