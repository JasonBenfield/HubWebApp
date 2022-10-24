using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record UserAuthenticatorModel(AppKey AuthenticatorAppKey, string ExternalUserID)
{
	public UserAuthenticatorModel()
		:this(AppKey.Unknown, "")
	{
	}
}
