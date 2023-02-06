using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record AppLogEntryDetailModel
(
    AppLogEntryModel LogEntry,
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
    AppUserModel User,
    int SourceLogEntryID,
    int TargetLogEntryID
)
{
    public AppLogEntryDetailModel()
        :this
        (
            new AppLogEntryModel(), 
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
            new AppUserModel(),
            0,
            0
        )
    {
    }
}
