function Xti-NewVersion {
    param(
        [ValidateSet("major", "minor", "patch")]
        $VersionType = "minor",
        [ValidateSet("Default", "DB")]
        $HubAdministrationType = "Default",
        $HubAppVersionKey = "",
        $RepoOwner = "",
        $RepoName = ""
    )
    ThrowIfNotSolutionDir
    Xti-Admin -EnvName Production -Command NewVersion -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -VersionType $VersionType -HubAdministrationType $HubAdministrationType -HubAppVersionKey "`"$($HubAppVersionKey)`""
}

function Xti-NewIssue {
    param(
        [Parameter(Mandatory)]
        [string] $IssueTitle,
        [switch] $Start = $false,
        $RepoOwner = "",
        $RepoName = ""
    )
    ThrowIfNotSolutionDir
    if($Start) {
        Xti-Admin -EnvName Production -Command NewIssue -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -IssueTitle "`"$($IssueTitle)`"" -StartIssue $Start
    }
    else{
        Xti-Admin -EnvName Production -Command NewIssue -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -IssueTitle "`"$($IssueTitle)`""
    }
}

function Xti-StartIssue {
    param(
        [long]
        $IssueNumber = 0,
        $RepoOwner = "",
        $RepoName = ""
    )
    ThrowIfNotSolutionDir
    Xti-Admin -EnvName Production -Command StartIssue -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -IssueNumber $IssueNumber
}

function Xti-CompleteIssue {
    param(
        [ValidateSet("Default", "DB")]
        $HubAdministrationType = "Default",
        $HubAppVersionKey = "",
        $RepoOwner = "",
        $RepoName = ""
    )
    ThrowIfNotSolutionDir
    Xti-Admin -EnvName Production -Command CompleteIssue -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -HubAdministrationType $HubAdministrationType -HubAppVersionKey "`"$($HubAppVersionKey)`""
}