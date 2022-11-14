namespace XTI_Hub.Abstractions;

public sealed record AuthenticatorModel(int ID, AuthenticatorKey AuthenticatorKey)
{
	public AuthenticatorModel()
		:this(0, AuthenticatorKey.Unknown)
	{
	}
}
