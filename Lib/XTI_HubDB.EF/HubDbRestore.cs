using System.Threading.Tasks;
using XTI_DB;

namespace XTI_HubDB.EF
{
    public sealed class HubDbRestore
    {
        private readonly HubDbContext hubDbContext;

        public HubDbRestore(HubDbContext XTI_HubDBContext)
        {
            this.hubDbContext = XTI_HubDBContext;
        }

        public Task Run(string environmentName, string backupFilePath)
            => new DbRestore(hubDbContext).Run(new HubDbName(environmentName), backupFilePath);
    }
}
