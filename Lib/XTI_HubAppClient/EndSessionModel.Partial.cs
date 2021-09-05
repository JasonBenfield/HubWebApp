using XTI_TempLog.Abstractions;

namespace XTI_HubAppClient
{
    partial class EndSessionModel : IEndSessionModel
    {
        public EndSessionModel(IEndSessionModel source)
        {
            SessionKey = source.SessionKey;
            TimeEnded = source.TimeEnded;
        }
    }
}
