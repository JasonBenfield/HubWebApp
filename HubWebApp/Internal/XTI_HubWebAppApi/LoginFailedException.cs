namespace XTI_HubWebAppApi;

public class LoginFailedException : AppException
{
    protected LoginFailedException(string message) 
        : base(message, "User name or Password was incorrect")
    {
    }
}