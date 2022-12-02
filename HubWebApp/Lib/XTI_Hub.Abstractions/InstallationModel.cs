using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTI_Hub.Abstractions;

public sealed record InstallationModel(int ID, InstallStatus Status, bool IsCurrent, string Domain)
{
    public InstallationModel()
        : this(0, InstallStatus.Values.GetDefault(), false, "")
{
}
}
