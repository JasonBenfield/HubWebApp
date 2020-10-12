namespace HubWebApp.AuthApi
{
    public sealed class PasswordIncorrectException : LoginFailedException
    {
        public PasswordIncorrectException(string userName)
            : base($"Password not correct for user {userName}")
        {
        }
    }
}
