using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Git;
using XTI_Git.Abstractions;

namespace XTI_Admin;

internal sealed class VersionKeyFromCurrentBranch
{
    private readonly IServiceProvider sp;

    public VersionKeyFromCurrentBranch(Scopes scopes)
    {
        sp = scopes.Production();
    }

    public AppVersionKey Value()
    {
        var gitRepo = sp.GetRequiredService<IXtiGitRepository>();
        var branchName = gitRepo.CurrentBranchName();
        var xtiBranchName = XtiBranchName.Parse(branchName);
        if (xtiBranchName is not XtiVersionBranchName versionBranchName)
        {
            throw new ArgumentException($"Branch '{branchName}' is not a version branch");
        }
        return AppVersionKey.Parse(versionBranchName.Version.Key);
    }
}