using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record AppDefinitionModel(AppKey AppKey, string Domain)
{
    public AppDefinitionModel()
        :this(AppKey.Unknown, "")
    {
    }
}
