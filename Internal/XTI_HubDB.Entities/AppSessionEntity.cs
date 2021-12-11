using System;

namespace XTI_HubDB.Entities
{
    public sealed class AppSessionEntity
    {
        public int ID { get; set; }
        public string SessionKey { get; set; } = "";
        public int UserID { get; set; }
        public string RequesterKey { get; set; } = "";
        public DateTimeOffset TimeStarted { get; set; } = DateTimeOffset.MinValue;
        public DateTimeOffset TimeEnded { get; set; } = DateTimeOffset.MaxValue;
        public string RemoteAddress { get; set; } = "";
        public string UserAgent { get; set; } = "";
    }
}
