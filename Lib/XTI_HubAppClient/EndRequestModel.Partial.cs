using XTI_TempLog.Abstractions;

namespace XTI_HubAppClient
{
    partial class EndRequestModel : IEndRequestModel
    {
        public EndRequestModel(IEndRequestModel source)
        {
            RequestKey = source.RequestKey;
            TimeEnded = source.TimeEnded;
        }
    }
}
