using System;

namespace XTI_HubDB.Entities
{
    public sealed class AppRoleRecord
    {
        public int ID { get; set; }
        public int AppID { get; set; }
        public string Name { get; set; } = "xti_notfound";
        public DateTimeOffset TimeDeactivated { get; set; } = DateTimeOffset.MaxValue;
    }
}
