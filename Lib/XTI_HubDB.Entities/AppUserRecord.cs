using System;

namespace XTI_HubDB.Entities
{
    public sealed class AppUserRecord
    {
        public int ID { get; set; }
        public string UserName { get; set; } = "xti_notfound";
        public string Password { get; set; } = "";
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public DateTimeOffset TimeAdded { get; set; } = DateTimeOffset.MaxValue;
    }
}
