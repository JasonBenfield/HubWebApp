using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed record AppUserGroupModel(int ID, AppUserGroupName GroupName, ModifierKey PublicKey);
