﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HubWebAppApi
{
    public abstract class AccessForAuthenticate : IAccess
    {
        public Task<string> GenerateToken(IEnumerable<Claim> claims) =>
            _GenerateToken(claims);

        protected abstract Task<string> _GenerateToken(IEnumerable<Claim> claims);

        public Task Logout() =>
            _Logout();

        protected abstract Task _Logout();
    }
}
