using XTI_App.Api;

namespace XTI_HubAppApi;

public class LoginFailedException : AppException
{
    protected LoginFailedException(string message) 
        : base(message, "User name or Password was incorrect")
    {
    }
}