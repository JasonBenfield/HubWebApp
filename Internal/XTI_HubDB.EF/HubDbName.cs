using XTI_DB;

namespace XTI_HubDB.EF;

public sealed class HubDbName : XtiDbName
{
    public HubDbName(string environmentName) : base(environmentName, "Hub")
    {
    }
}