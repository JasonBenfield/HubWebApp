using XTI_TempLog.Abstractions;

namespace XTI_HubAppClient;

partial class AuthenticateSessionModel : IAuthenticateSessionModel
{
    public AuthenticateSessionModel(IAuthenticateSessionModel source)
    {
        SessionKey = source.SessionKey;
        UserName = source.UserName;
    }
}