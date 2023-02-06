function Xti-NewVersion {
    param(
        [ValidateSet("major", "minor", "patch")]
        $VersionType = "minor",
        [ValidateSet("Default", "DB")]
        $HubAdministrationType = "Default",
        $HubAppVersionKey = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName
    )
    ThrowIfNotSolutionDir
    Xti-Admin -EnvName Production -Command NewVersion -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -VersionType $VersionType -HubAdministrationType $HubAdministrationType -HubAppVersionKey "`"$($HubAppVersionKey)`""
}

function Xti-NewIssue {
    param(
        [Parameter(Mandatory)]
        [string] $IssueTitle,
        [switch] $Start,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        [string] $RepoOwner,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        [string] $RepoName
    )
    ThrowIfNotSolutionDir
    Xti-Admin -EnvName Production -Command NewIssue -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -IssueTitle "`"$($IssueTitle)`"" -StartIssue $Start
}

function Xti-StartIssue {
    param(
        [long]
        $IssueNumber = 0,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName
    )
    ThrowIfNotSolutionDir
    Xti-Admin -EnvName Production -Command StartIssue -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -IssueNumber $IssueNumber
}

function Xti-CompleteIssue {
    param(
        [ValidateSet("Default", "DB")]
        $HubAdministrationType = "Default",
        $HubAppVersionKey = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName
    )
    ThrowIfNotSolutionDir
    Xti-Admin -EnvName Production -Command CompleteIssue -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -HubAdministrationType $HubAdministrationType -HubAppVersionKey "`"$($HubAppVersionKey)`""
}