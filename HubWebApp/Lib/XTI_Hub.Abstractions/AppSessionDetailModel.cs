using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record AppSessionDetailModel
(
    AppSessionModel Session,
    AppUserGroupModel UserGroup,
    AppUserModel User
)
{
    public AppSessionDetailModel()
        :this
        (
            new AppSessionModel(),
            new AppUserGroupModel(),
            new AppUserModel()
        )
    {
    }
}
