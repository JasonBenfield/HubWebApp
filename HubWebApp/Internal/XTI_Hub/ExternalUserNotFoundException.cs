using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub;

public sealed class ExternalUserNotFoundException : AppException
{
    public ExternalUserNotFoundException(AppKey appKey, string externalUserKey) :
        base
        (
            "External User not found", 
            $"User not found for authenticator '{appKey.Name.DisplayText}' with user key '{externalUserKey}'"
        )
    {
    }
}
