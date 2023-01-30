using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record AppUserGroupModel(int ID, AppUserGroupName GroupName, ModifierKey PublicKey)
{
	public AppUserGroupModel()
		:this(0,new AppUserGroupName(), new ModifierKey())
	{
	}
}
