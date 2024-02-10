using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class RegisterAppRequest
{
    public RegisterAppRequest()
        : this(AppVersionKey.None, new AppApiTemplateModel())
    {
    }

    public RegisterAppRequest(AppVersionKey versionKey, AppApiTemplateModel appTemplate)
    {
        VersionKey = versionKey;
        AppTemplate = appTemplate;
    }

    public AppVersionKey VersionKey { get; set; }
    public AppApiTemplateModel AppTemplate { get; set; } 
}