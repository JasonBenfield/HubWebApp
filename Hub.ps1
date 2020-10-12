Import-Module PowershellForXti -Force

$script:config = [PSCustomObject]@{
    RepoOwner = "JasonBenfield"
    RepoName = "HubWebApp"
    AppKey = "Hub"
    AppType = "WebApp"
    ProjectDir = "C:\XTI\src\HubWebApp\Apps\HubWebApp"
}

function Hub-New-XtiIssue {
    param(
        [Parameter(Mandatory)]
        [string] $IssueTitle,
        $Label = @(),
        [string] $Body = ""
    )
    $script:config | New-XtiIssue @PsBoundParameters
}

function Hub-Xti-StartIssue {
    param(
        [Parameter(Position=0)]
        [long]$IssueNumber = 0,
        $IssueBranchTitle = "",
        $AssignTo = ""
    )
    $script:config | Xti-StartIssue @PsBoundParameters
}

function Hub-New-XtiVersion {
    param(
        [ValidateSet(“Development", "Production", "Staging", "Test")]
        $EnvName = "Production",
        [ValidateSet(“major”, "minor", "patch")]
        $VersionType = "minor"
    )
    $script:config | New-XtiVersion @PsBoundParameters
}

function Hub-New-XtiPullRequest {
    param(
        $CommitMessage
    )
    $script:config | New-XtiPullRequest @PsBoundParameters
}

function Hub-Xti-PostMerge {
    param(
    )
    $script:config | Xti-PostMerge @PsBoundParameters
}

function Hub-Publish {
    param(
        [ValidateSet("Production", “Development", "Staging", "Test")]
        [string] $EnvName="Production"
    )
    
    $ErrorActionPreference = "Stop"

    $activity = "Publishing to $EnvName"
    
    $timestamp = Get-Date -Format "yyMMdd_HHmmssfff"
    $backupFilePath = "$($env:ProgramData)\XTI\Backups\$EnvName\app_$timestamp.bak"
    if($EnvName -eq "Production" -or $EnvName -eq "Staging") {
        Write-Progress -Activity $activity -Status "Backuping up the app database" -PercentComplete 10
	    Xti-BackupAppDb -envName "Production" -BackupFilePath $backupFilePath
        $env:DOTNET_ENVIRONMENT=$EnvName
        $env:ASPNETCORE_ENVIRONMENT=$EnvName
    }
    if($EnvName -eq "Staging") { 
        Write-Progress -Activity $activity -Status "Restoring the app database" -PercentComplete 15
	    Xti-RestoreAppDb -EnvName $EnvName -BackupFilePath $backupFilePath
    }

    Write-Progress -Activity $activity -Status "Updating the app database" -PercentComplete 18
    Xti-UpdateAppDb -EnvName $EnvName

    if ($EnvName -eq "Test"){
        Write-Progress -Activity $activity -Status "Resetting the app database" -PercentComplete 20
	    Xti-ResetAppDb -EnvName $EnvName
    }

    Write-Progress -Activity $activity -Status "Generating the api" -PercentComplete 30
    XtiHub-GenerateApi -EnvName $EnvName

    Write-Progress -Activity $activity -Status "Running web pack" -PercentComplete 40
    $script:config | XtiHub-Webpack

    Write-Progress -Activity $activity -Status "Building solution" -PercentComplete 50
    dotnet build 

    Write-Progress -Activity $activity -Status "Setting up Hub Web App" -PercentComplete 60
    XtiHub-Setup -EnvName $EnvName

    if ($EnvName -eq "Test") {
        Write-Progress -Activity $activity -Status "Creating user" -PercentComplete 70
        $script:config | New-XtiHubUser -EnvName $EnvName -CredentialKey HubAdmin -UserName HubAdmin -RoleNames Admin
    }

    Write-Progress -Activity $activity -Status "Publishing website" -PercentComplete 80

    $script:config | Xti-PublishWebApp -EnvName $EnvName
    if($EnvName -eq "Production") {
        $script:config | Xti-PublishPackage -DisableUpdateVersion
    }
}

function New-XtiHubUser {
    param(
        [ValidateSet(“Development", "Production", "Staging", "Test")]
        [string] $EnvName="Production", 
        [string] $CredentialKey = "", 
        [string] $UserName = "", 
        [string] $Password = "", 
        [string] $RoleNames = ""
    )
    $script:config | New-XtiUser @PsBoundParameters
}

function XtiHub-GenerateApi {
    param (
        [ValidateSet("Development", "Production", "Staging", "Test")]
        [string] $EnvName='Production'
    )
    $currentDir = (Get-Item .).FullName
    $env:DOTNET_ENVIRONMENT=$EnvName
    Set-Location Apps/HubApiGeneratorApp
    dotnet run
    Set-Location $currentDir
}

function XtiHub-Setup {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [string] $EnvName="Development"
    )

    $env:DOTNET_ENVIRONMENT=$EnvName
    $env:ASPNETCORE_ENVIRONMENT=$EnvName

    $currentDir = (Get-Item .).FullName
    Set-Location Apps/HubSetupConsoleApp
    dotnet run --no-launch-profile
    Set-Location $currentDir

    if( $LASTEXITCODE -ne 0 ) {
        Throw "Hub setup failed"
    }
}

function XtiHub-Webpack {
    param(
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        [string] $ProjectDir
    )
    $currentDir = (Get-Item .).FullName
    Set-Location $ProjectDir
    webpack
    Set-Location $currentDir
}