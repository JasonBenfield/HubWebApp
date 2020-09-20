using System.Collections.Generic;
using System.Security.Claims;
using XTI_App;

namespace HubWebApp.AuthApi
{
    public sealed class XtiClaimsCreator
    {
        private readonly AppSession session;
        private readonly AppUser user;

        public XtiClaimsCreator(AppSession session, AppUser user)
        {
            this.session = session;
            this.user = user;
        }

        public IEnumerable<Claim> Values() => new[]
        {
            new Claim("UserID", (user?.ID ?? 0).ToString()),
            new Claim("SessionID", (session?.ID ?? 0).ToString())
        };
    }
}
