using XTI_App.Api;

namespace HubWebApp.AuthApi
{
    public class LoginFailedException : AppException
    {
        protected LoginFailedException(string message) : base(message, "User name or Password was incorrect")
        {
        }
    }
}
