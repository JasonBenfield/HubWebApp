using XTI_TempLog.Abstractions;

namespace XTI_HubAppClient;

partial class StartRequestModel : IStartRequestModel
{
    public StartRequestModel(IStartRequestModel source)
    {
        RequestKey = source.RequestKey;
        SessionKey = source.SessionKey;
        AppType = source.AppType;
        Path = source.Path;
        TimeStarted = source.TimeStarted;
        ActualCount = source.ActualCount;
    }
}