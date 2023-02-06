using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record AppRequestDetailModel
(
    AppRequestModel Request,
    ResourceGroupModel ResourceGroup,
    ResourceModel Resource,
    ModifierCategoryModel ModCategory,
    ModifierModel Modifier,
    InstallLocationModel InstallLocation,
    InstallationModel Installation,
    XtiVersionModel Version,
    AppModel App,
    AppSessionModel Session,
    AppUserGroupModel UserGroup,
    AppUserModel User
)
{
    public AppRequestDetailModel()
        :this
        (
            new AppRequestModel(), 
            new ResourceGroupModel(), 
            new ResourceModel(),
            new ModifierCategoryModel(),
            new ModifierModel(),
            new InstallLocationModel(),
            new InstallationModel(),
            new XtiVersionModel(),
            new AppModel(),
            new AppSessionModel(),
            new AppUserGroupModel(),
            new AppUserModel()
        )
    {
    }
}
