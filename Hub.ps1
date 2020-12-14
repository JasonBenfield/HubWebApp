Import-Module PowershellForXti -Force

$script:hubConfig = [PSCustomObject]@{
    RepoOwner = "JasonBenfield"
    RepoName = "HubWebApp"
    AppName = "Hub"
    AppType = "WebApp"
    ProjectDir = "C:\XTI\src\HubWebApp\Apps\HubWebApp"
}

function Hub-New-XtiIssue {
    param(
        [Parameter(Mandatory)]
        [string] $IssueTitle,
        $Labels = @(),
        [string] $Body = "",
        [switch] $Start
    )
    $script:hubConfig | New-XtiIssue @PsBoundParameters
}

function Hub-Xti-StartIssue {
    param(
        [Parameter(Position=0)]
        [long]$IssueNumber = 0,
        $IssueBranchTitle = "",
        $AssignTo = ""
    )
    $script:hubConfig | Xti-StartIssue @PsBoundParameters
}

function Hub-New-XtiVersion {
    param(
        [ValidateSet(“Development", "Production", "Staging", "Test")]
        $EnvName = "Production",
        [ValidateSet(“major”, "minor", "patch")]
        $VersionType = "minor"
    )
    $script:hubConfig | New-XtiVersion @PsBoundParameters
}

function Hub-Xti-Merge {
    param(
        $CommitMessage
    )
    $script:hubConfig | Xti-Merge @PsBoundParameters
}

function Hub-New-XtiPullRequest {
    param(
        $CommitMessage
    )
    $script:hubConfig | New-XtiPullRequest @PsBoundParameters
}

function Hub-Xti-PostMerge {
    param(
    )
    $script:hubConfig | Xti-PostMerge @PsBoundParameters
}

function Xti-CopyShared {
    $source = "..\SharedWebApp\Apps\SharedWebApp"
    $target = ".\Apps\HubWebApp"
    robocopy "$source\Scripts\Shared\" "$target\Scripts\Shared\" *.ts /e /purge /njh /njs /np /ns /nc /nfl /ndl /a+:R
    robocopy "$source\Scripts\Shared\" "$target\Scripts\Shared\" /xf *.ts /e /purge /njh /njs /np /ns /nc /nfl /ndl /a-:R
    robocopy "$source\Views\Exports\Shared\" "$target\Views\Exports\Shared\" /e /purge /njh /njs /np /ns /nc /nfl /ndl /a+:R
}

function Xti-CopyAuthenticator {
    $source = "..\AuthenticatorWebApp\Apps\AuthenticatorWebApp"
    $target = ".\Apps\HubWebApp"
    robocopy "$source\Scripts\Authenticator\" "$target\Scripts\Authenticator\" *.ts /e /purge /njh /njs /np /ns /nc /nfl /ndl /a+:R
    robocopy "$source\Scripts\Authenticator\" "$target\Scripts\Authenticator\" /xf *.ts /e /purge /njh /njs /np /ns /nc /nfl /ndl /a-:R
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
	    Xti-BackupMainDb -EnvName "Production" -BackupFilePath $backupFilePath
    }
    if($EnvName -eq "Staging") { 
        Write-Progress -Activity $activity -Status "Restoring the app database" -PercentComplete 15
	    Xti-RestoreMainDb -EnvName $EnvName -BackupFilePath $backupFilePath
    }

    Write-Progress -Activity $activity -Status "Updating the app database" -PercentComplete 18
    Xti-UpdateMainDb -EnvName $EnvName

    if ($EnvName -eq "Test"){
        Write-Progress -Activity $activity -Status "Resetting the app database" -PercentComplete 20
	    Xti-ResetMainDb -EnvName $EnvName
    }

    Write-Progress -Activity $activity -Status "Generating the api" -PercentComplete 30
    Hub-GenerateApi -EnvName $EnvName -DisableClients

    Xti-CopyShared
    Xti-CopyAuthenticator

    Write-Progress -Activity $activity -Status "Running web pack" -PercentComplete 40
    $script:hubConfig | Hub-Webpack

    Write-Progress -Activity $activity -Status "Building solution" -PercentComplete 50
    dotnet build 

    Hub-Setup -EnvName $EnvName

    if ($EnvName -eq "Test") {
        Invoke-WebRequest -Uri https://test.guinevere.com/Authenticator/Current/StopApp
        Write-Progress -Activity $activity -Status "Creating user" -PercentComplete 70
        Hub-New-AdminUser -EnvName $EnvName
    }

    Write-Progress -Activity $activity -Status "Publishing website" -PercentComplete 80
    
    if($EnvName -eq "Production") {
        $branch = Get-CurrentBranchname
        Xti-BeginPublish -BranchName $branch
    }
    $script:hubConfig | Xti-PublishWebApp -EnvName $EnvName

    if($EnvName -eq "Production") {
        Hub-GenerateApi -EnvName $EnvName -DisableControllers
        $script:hubConfig | Xti-PublishPackage -DisableUpdateVersion -Prod
        Xti-EndPublish -BranchName $branch
    }
    else {
        $script:hubConfig | Xti-PublishPackage -DisableUpdateVersion
    }
}

function Hub-New-AdminUser {
    param(
        [ValidateSet(“Development", "Production", "Staging", "Test")]
        [string] $EnvName="Production"
    )
    $password = Xti-GeneratePassword
    $script:hubConfig | New-XtiUser -EnvName $EnvName -UserName HubAdmin -Password $password
    $script:hubConfig | New-XtiUserRoles -EnvName $EnvName -UserName HubAdmin -RoleNames Admin
    $script:hubConfig | New-XtiUserCredentials -EnvName $EnvName -CredentialKey HubAdmin -UserName HubAdmin -Password $password
}

function Hub-GenerateApi {
    param (
        [ValidateSet("Development", "Production", "Staging", "Test")]
        [string] $EnvName='Production',
        [switch] $DisableClient,
        [switch] $DisableControllers
    )
    $currentDir = (Get-Item .).FullName
    Set-Location Apps/HubApiGeneratorApp
    dotnet run --environment=$EnvName -- --Output:TsClient:Disable $DisableClient --Output:CsClient:Disable $DisableClient --Output:CsControllers:Disable $DisableControllers
    Set-Location $currentDir
}

function Hub-Setup {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [string] $EnvName="Development"
    )
    $currentDir = (Get-Item .).FullName
    Set-Location Apps/HubSetupConsoleApp
    dotnet run --no-launch-profile --environment=$EnvName
    Set-Location $currentDir

    if( $LASTEXITCODE -ne 0 ) {
        Throw "Hub setup failed with exit code $LASTEXITCODE"
    }
}

function Hub-Webpack {
    param(
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        [string] $ProjectDir
    )
    $currentDir = (Get-Item .).FullName
    Set-Location $ProjectDir
    webpack
    Set-Location $currentDir
}

function Hub-ResetTest {
	Xti-ResetMainDb -EnvName Test
    Hub-Setup -EnvName Test
    New-XtiHubUser -EnvName Test -CredentialKey HubAdmin -UserName HubAdmin -RoleNames Admin
}