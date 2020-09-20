﻿namespace HubWebApp.AuthApi
{
    public sealed class LoginResult
    {
        public LoginResult(string token)
        {
            Token = token;
        }

        public string Token { get; }
    }
}
