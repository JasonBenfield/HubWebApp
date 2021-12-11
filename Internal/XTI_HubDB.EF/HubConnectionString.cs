using Microsoft.Extensions.Options;
using XTI_DB;

namespace XTI_HubDB.EF
{
    public sealed class HubConnectionString : XtiConnectionString
    {
        public HubConnectionString(IOptions<DbOptions> options, string envName)
            : this(options.Value, envName)
        {
        }

        public HubConnectionString(DbOptions options, string envName)
            : base(options, new HubDbName(envName))
        {
        }
    }
}
