Import-Module PowershellForXti -Force

$script:hubConfig = [PSCustomObject]@{
    RepoOwner = "JasonBenfield"
    RepoName = "HubWebApp"
    AppName = "Hub"
    AppType = "WebApp"
    AppsToImport = "Shared"
}

function Hub-NewVersion {
    param(
        [ValidateSet(“major”, "minor", "patch")]
        $VersionType = "minor"
    )
    $script:hubConfig | New-XtiVersion @PsBoundParameters
}

function Hub-Issues {
    param(
    )
    $script:hubConfig | Xti-Issues @PsBoundParameters
}

function Hub-NewIssue {
    param(
        [Parameter(Mandatory)]
        [string] $IssueTitle,
        [switch] $Start
    )
    $script:hubConfig | New-XtiIssue @PsBoundParameters
}

function Hub-StartIssue {
    param(
        [Parameter(Position=0)]
        [long]$IssueNumber = 0
    )
    $script:hubConfig | Xti-StartIssue @PsBoundParameters
}

function Hub-CompleteIssue {
    param(
    )
    $script:hubConfig | Xti-CompleteIssue @PsBoundParameters
}

function Hub-Build {
    param(
        [ValidateSet("Development", "Production", "Staging", "Test")]
        $EnvName = "Development"
    )
    $script:hubConfig | Xti-BuildWebApp @PsBoundParameters
}

function Hub-Publish {
    param(
        [ValidateSet("Production", "Development")]
        [string] $EnvName="Development"
    )
    $DestinationMachine = Get-DestinationMachine -EnvName $EnvName
    $PsBoundParameters.Add("DestinationMachine", $DestinationMachine)
    $script:hubConfig | Xti-Publish @PsBoundParameters
}

function Hub-Install {
    param(
        [ValidateSet("Development", "Production", "Staging", "Test")]
        $EnvName = "Development"
    )
    $DestinationMachine = Get-DestinationMachine -EnvName $EnvName
    $PsBoundParameters.Add("DestinationMachine", $DestinationMachine)
    $script:hubConfig | Xti-Install @PsBoundParameters
}

function Add-DBMigrations {
    param ([Parameter(Mandatory)]$Name)
    $env:DOTNET_ENVIRONMENT="Development"
    dotnet ef --startup-project ./Tools/HubDbTool migrations add $Name --project ./Lib/XTI_HubDB.EF.SqlServer
}

function Get-DestinationMachine {
    param(
        $EnvName
    )
    if($EnvName -eq "Development")
    {
        $DestinationMachine = ""
    }
    else
    {
        $DestinationMachine = "finduilas.xartogg.com"
    }
    return $DestinationMachine
}
