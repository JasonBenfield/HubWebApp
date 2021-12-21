using XTI_App.Api;

namespace XTI_HubAppApi.Auth;

public class LoginFailedException : AppException
{
    protected LoginFailedException(string message) 
        : base(message, "User name or Password was incorrect")
    {
    }
}