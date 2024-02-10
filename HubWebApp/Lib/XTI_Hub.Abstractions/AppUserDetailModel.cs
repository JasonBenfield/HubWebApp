using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record AppUserDetailModel(AppUserModel User, AppUserGroupModel UserGroup)
{
    public AppUserDetailModel()
        : this(new AppUserModel(), new AppUserGroupModel())
    {
    }
}
