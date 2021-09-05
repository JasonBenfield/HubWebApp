﻿using HubWebAppApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HubWebApp.Fakes
{
    public sealed class FakeAccessForLogin : AccessForLogin
    {
        public FakeAccessForLogin()
        {
            Token = Guid.NewGuid().ToString("N");
        }

        public string Token { get; }
        public IEnumerable<Claim> Claims { get; private set; } = Enumerable.Empty<Claim>();

        protected override Task<string> _GenerateToken(IEnumerable<Claim> claims)
        {
            Claims = claims;
            return Task.FromResult(Token);
        }

        protected override Task _Logout()
        {
            Claims = Enumerable.Empty<Claim>();
            return Task.CompletedTask;
        }
    }
}
