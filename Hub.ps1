Import-Module PowershellForXti -Force

$script:hubConfig = [PSCustomObject]@{
    RepoOwner = "JasonBenfield"
    RepoName = "HubWebApp"
    AppName = "Hub"
    AppType = "WebApp"
    ProjectDir = "Apps\HubWebApp"
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

function Hub-Publish {
    param(
        [ValidateSet("Production", “Development", "Staging", "Test")]
        [string] $EnvName="Development"
    )
    
    $ErrorActionPreference = "Stop"

    Write-Output "Publishing to $EnvName"
    
    $timestamp = Get-Date -Format "yyMMdd_HHmmssfff"
    $backupFilePath = "$($env:XTI_AppData)\$EnvName\Backups\app_$timestamp.bak"
    if($EnvName -eq "Production" -or $EnvName -eq "Staging") {
        Write-Output "Backuping up the app database"
	    Xti-BackupMainDb -EnvName "Production" -BackupFilePath $backupFilePath
    }
    if($EnvName -eq "Staging") { 
        Write-Output "Restoring the app database"
	    Xti-RestoreMainDb -EnvName $EnvName -BackupFilePath $backupFilePath
    }

    Write-Output "Updating the app database"
    Xti-UpdateMainDb -EnvName $EnvName

    if ($EnvName -eq "Test"){
        Write-Output "Resetting the app database"
	    Xti-ResetMainDb -EnvName $EnvName
    }
    
    $defaultVersion = ""
    if($EnvName -eq "Production") {
        $branch = Get-CurrentBranchname
        Xti-BeginPublish -BranchName $branch
        $releaseBranch = Parse-ReleaseBranch -BranchName $branch
        $defaultVersion = $releaseBranch.VersionKey
    }
    
    Write-Output "Generating the api"
    Hub-GenerateApi -EnvName $EnvName -DefaultVersion $defaultVersion
    
    tsc -p "$($script:hubConfig.ProjectDir)\Scripts\$($script:hubConfig.AppName)\tsconfig.json"
    
    if($EnvName -eq "Production") {
        Hub-ImportWeb -Prod
    }
    else {
        Hub-ImportWeb
    }

    Write-Output "Running web pack"
    $script:hubConfig | Hub-Webpack

    Write-Output "Building solution"
    dotnet build 

    Hub-Setup -EnvName $EnvName -VersionKey $defaultVersion
    
    if ($EnvName -eq "Test") {
        Invoke-WebRequest -Uri https://test.guinevere.com/Authenticator/Current/StopApp
        Write-Output "Creating user"
        Hub-New-AdminUser -EnvName $EnvName
    }

    Write-Output "Publishing website"
    
    $script:hubConfig | Xti-PublishWebApp -EnvName $EnvName

    if($EnvName -eq "Production") {
        Write-Output "Publishing Package"
        $script:hubConfig | Xti-PublishPackage -DisableUpdateVersion -Prod
        Write-Output "End publish"
        Xti-EndPublish -BranchName $branch
        Write-Output "Merging Pull Request"
        $script:hubConfig | Xti-Merge
    }
    else {
        Write-Output "Publishing Package"
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
        [string] $DefaultVersion
    )
    dotnet run --project Apps/HubApiGeneratorApp --environment $EnvName --Output:DefaultVersion "`"$DefaultVersion`""
    tsc -p Apps/HubWebApp/Scripts/Hub/tsconfig.json
    if( $LASTEXITCODE -ne 0 ) {
        Throw "Hub api generator failed with exit code $LASTEXITCODE"
    }
}

function Hub-Setup {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [string] $EnvName="Development",
        [string] $VersionKey = ""
    )
    dotnet run --project Apps/HubSetupConsoleApp --environment=$EnvName --Setup:VersionKey="`"$VersionKey`""
    if( $LASTEXITCODE -ne 0 ) {
        Throw "Hub setup failed with exit code $LASTEXITCODE"
    }
}

function Hub-Webpack {
    param(
    )
    $ProjectDir = $script:hubConfig.ProjectDir
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

function Hub-ImportWeb {
    param(
        [switch] $Prod
    )
    $script:hubConfig | Xti-ImportWeb -Prod:$Prod -AppToImport Shared
    $script:hubConfig | Xti-ImportWeb -Prod:$Prod -AppToImport Authenticator
}
