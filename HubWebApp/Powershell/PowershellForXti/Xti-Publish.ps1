﻿function Xti-Publish {
    param (
        [ValidateSet("Production", "Development")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [ValidateSet("", "WebApp", "WebPackage", "ServiceApp", "ConsoleApp", "Package")]
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppType = "",
        [switch] $NoInstall,
        [ValidateSet("Default", "DB", "HubClient")]
        $HubAdministrationType = "Default",
        $HubAppVersionKey = "",
        [ValidateSet("Default", "GitHub", "Folder")]
        $InstallationSource = "Default",
        $InstallConfigurationName = ""
    )
    ThrowIfNotSolutionDir
    $Command = "PublishAndInstall"
    if($NoInstall){
        $Command = "Publish"
    }
    Xti-Admin -EnvName $EnvName -Command $Command -AppName "`"$($AppName)`"" -AppType $AppType -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -InstallConfigurationName "`"$($InstallConfigurationName)`"" -HubAdministrationType $HubAdministrationType -InstallationSource $InstallationSource -HubAppVersionKey "`"$($HubAppVersionKey)`""
}

function Xti-Build {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [ValidateSet("", "WebApp", "WebPackage", "ServiceApp", "ConsoleApp", "Package")]
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppType = "",
        [ValidateSet("Default", "DB", "HubClient")]
        $HubAdministrationType = "Default",
        $HubAppVersionKey = ""
    )
    ThrowIfNotSolutionDir
    Xti-Admin -EnvName $EnvName -Command Build -AppName "`"$($AppName)`"" -AppType $AppType -HubAdministrationType $HubAdministrationType -HubAppVersionKey "`"$($HubAppVersionKey)`""
}

function Xti-Setup {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [ValidateSet("", "WebApp", "WebPackage", "ServiceApp", "ConsoleApp", "Package")]
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppType = "",
        [ValidateSet("Default", "DB", "HubClient")]
        $HubAdministrationType = "Default",
        [ValidateSet("Default", "GitHub", "Folder")]
        $InstallationSource = "Default",
        $HubAppVersionKey = ""
    )
    ThrowIfNotSolutionDir
    Xti-Admin -EnvName $EnvName -Command Setup -AppName "`"$($AppName)`"" -AppType $AppType -HubAdministrationType $HubAdministrationType -InstallationSource $InstallationSource -HubAppVersionKey "`"$($HubAppVersionKey)`""
}

function Xti-Install {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [ValidateSet("", "WebApp", "WebPackage", "ServiceApp", "ConsoleApp", "Package")]
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppType = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $VersionNumber,
        [ValidateSet("Default", "DB", "HubClient")]
        $HubAdministrationType = "Default",
        [ValidateSet("Default", "GitHub", "Folder")]
        $InstallationSource = "Default",
        $InstallConfigurationName = "",
        $HubAppVersionKey = "",
        $Release = ""
    )
    ThrowIfNotSolutionDir
    Xti-Admin -EnvName $EnvName -Command Install -AppName "`"$($AppName)`"" -AppType $AppType -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -VersionNumber "`"$($VersionNumber)`"" -InstallConfigurationName "`"$($InstallConfigurationName)`"" -HubAdministrationType $HubAdministrationType -InstallationSource $InstallationSource -HubAppVersionKey "`"$($HubAppVersionKey)`"" -Release "`"$($Release)`""
}

function Xti-PublishLib {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [ValidateSet("", "WebApp", "WebPackage", "ServiceApp", "ConsoleApp", "Package")]
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppType = "",
        $RepoOwner,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName,
        [ValidateSet("Default", "DB", "HubClient")]
        $HubAdministrationType = "Default",
        $HubAppVersionKey = ""
    )
    ThrowIfNotSolutionDir
    Xti-Admin -EnvName $EnvName -Command PublishLib -AppName "`"$($AppName)`"" -AppType $AppType -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -HubAdministrationType $HubAdministrationType
}

function Xti-AddInstallationUser {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [ValidateSet("Default", "DB", "HUbClient")]
        $HubAdministrationType = "Default",
        $HubAppVersionKey = ""
    )
    Xti-Admin -EnvName $EnvName -Command AddInstallationUser -HubAdministrationType $HubAdministrationType -HubAppVersionKey "`"$($HubAppVersionKey)`""
}

function Xti-AddSystemUser {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        [ValidateSet("", "WebApp", "WebPackage", "ServiceApp", "ConsoleApp", "Package")]
        $AppType = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $Domain = "",
        [ValidateSet("Default", "DB", "HubClient")]
        $HubAdministrationType = "Default",
        $HubAppVersionKey = ""
    )
    Xti-Admin -EnvName $EnvName -Command AddSystemUser -AppName "`"$($AppName)`"" -AppType $AppType -Domain "`"$($Domain)`"" -HubAdministrationType $HubAdministrationType -HubAppVersionKey "`"$($HubAppVersionKey)`""
}

function Xti-AddAdminUser {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $UserName = "",
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $Password = "",
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        [ValidateSet("", "WebApp", "WebPackage", "ServiceApp", "ConsoleApp", "Package")]
        $AppType = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $CredentialKey = "",
        [ValidateSet("Default", "DB", "HubClient")]
        $HubAdministrationType = "Default",
        $HubAppVersionKey = ""
    )
    Xti-Admin -EnvName $EnvName -Command AddAdminUser -UserName "`"$($UserName)`"" -Password "`"$($Password)`"" -CredentialKey "`"$($CredentialKey)`"" -HubAdministrationType $HubAdministrationType -AppName "`"$($AppName)`"" -AppType $AppType -HubAppVersionKey "`"$($HubAppVersionKey)`""
}

function Xti-DecryptTempLog {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName
    )
    Xti-Admin -EnvName $EnvName -Command DecryptTempLog
}

function Xti-UploadTempLog {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        $DestinationMachine = "",
        [ValidateSet("Default", "DB", "HubClient")]
        $HubAdministrationType = "Default"
    )
    Xti-Admin -EnvName $EnvName -Command UploadTempLog -DestinationMachine "`"$($DestinationMachine)`"" -HubAdministrationType $HubAdministrationType
}

function Xti-RestartApp {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        [ValidateSet("", "WebApp", "WebPackage", "ServiceApp", "ConsoleApp", "Package")]
        $AppType = "",
        $VersionKey = "",
        $DestinationMachine = ""
    )
    Xti-Admin -EnvName $EnvName -Command RestartApp -DestinationMachine "`"$($DestinationMachine)`"" -AppName "`"$($AppName)`"" -AppType $AppType -VersionKey "`"$($VersionKey)`""
}

function Xti-StoreCredentials {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
		$CredentialKey = "",
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
		$UserName = "",
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
		$Password = "",
        $DestinationMachine = ""
    )
    Xti-Admin -EnvName $EnvName -Command StoreCredentials -DestinationMachine "`"$($DestinationMachine)`"" -CredentialKey "`"$($CredentialKey)`"" -UserName "`"$($UserName)`"" -Password "`"$($Password)`""
}

function Xti-ShowCredentials {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
		$CredentialKey = ""
    )
    Xti-Admin -EnvName $EnvName -Command ShowCredentials -CredentialKey "`"$($CredentialKey)`""
}

function Xti-ConfigureInstallTemplate {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
		$TemplateName = "",
		$DestinationMachine = "",
		$Domain = "",
        $SiteName = "",
        [ValidateSet("Default", "DB", "HubClient")]
        $HubAdministrationType = "Default"
    )
    Xti-Admin -EnvName $EnvName -Command ConfigureInstallTemplate -InstallTemplateName "`"$($TemplateName)`"" -DestinationMachine "`"$($DestinationMachine)`"" -Domain "`"$($Domain)`"" -SiteName "`"$($SiteName)`"" -HubAdministrationType $HubAdministrationType
}

function Xti-ConfigureInstall {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        [ValidateSet("", "WebApp", "WebPackage", "ServiceApp", "ConsoleApp", "Package")]
        $AppType = "",
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
		$TemplateName = "",
        $InstallSequence = 0,
        [ValidateSet("Default", "DB", "HubClient")]
        $HubAdministrationType = "Default",
		$ConfigurationName = "",
		$RepoOwner = "",
		$RepoName = ""
    )
    Xti-Admin -EnvName $EnvName -Command ConfigureInstall -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -InstallConfigurationName "`"$($ConfigurationName)`"" -AppName "`"$($AppName)`"" -AppType $AppType -InstallTemplateName "`"$($TemplateName)`"" -InstallSequence "`"$($InstallSequence)`"" -HubAdministrationType $HubAdministrationType
}

function Xti-DeleteInstallConfiguration {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        [ValidateSet("", "WebApp", "WebPackage", "ServiceApp", "ConsoleApp", "Package")]
        $AppType = "",
        [ValidateSet("Default", "DB", "HubClient")]
        $HubAdministrationType = "Default",
		$ConfigurationName = "",
		$RepoOwner = "",
		$RepoName = ""
    )
    Xti-Admin -EnvName $EnvName -Command DeleteInstallConfiguration -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -InstallConfigurationName "`"$($ConfigurationName)`"" -AppName "`"$($AppName)`"" -AppType $AppType -HubAdministrationType $HubAdministrationType
}

function Xti-Admin {
    param (
        [ValidateSet("Production", “Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [ValidateSet("PublishAndInstall", "Build", "Setup", "Publish", "PublishLib", "Install", "NewVersion", "NewIssue", "StartIssue", "CompleteIssue", "AddInstallationUser", "AddSystemUser", "AddAdminUser", "DecryptTempLog", "UploadTempLog", "ShowCredentials", "StoreCredentials", "RestartApp", "ConfigureInstallTemplate", "ConfigureInstall", "DeleteInstallConfiguration")]
        [Parameter(Mandatory)]
        $Command,
        $AppName = "",
        [ValidateSet("", "WebApp", "WebPackage", "ServiceApp", "ConsoleApp", "Package")]
        $AppType = "",
        $VersionKey = "",
        $RepoOwner = "",
        $RepoName = "",
        $VersionNumber,
        $HubAppVersionKey = "",
        $DestinationMachine = "",
		$Domain = "",
		$SiteName = "",
		$InstallationUserName = "",
		$InstallationPassword = "",
		$Release = "",
        [ValidateSet("", "major", "minor", "patch")]
		$VersionType = "",
		$IssueTitle = "",
		$IssueNumber = 0,
        [switch] $StartIssue = $false,
        [ValidateSet("Default", "DB", "HubClient")]
        $HubAdministrationType = "Default",
        [ValidateSet("Default", "GitHub", "Folder")]
        $InstallationSource = "Default",
		$CredentialKey = "",
		$UserName = "",
		$Password = "",
		$InstallTemplateName = "",
		$InstallConfigurationName = "",
		$InstallSequence = 0
    )
    $ErrorActionPreference = "Stop"
    $Args = @()
    $Args += "--environment $EnvName"
    $Args += "--Command $Command"
    if(-not [string]::IsNullOrWhiteSpace($AppName) -and $AppName -ne "`"`"") {
        $Args += "--AppName $AppName"
    }
    if(-not [string]::IsNullOrWhiteSpace($AppType)) {
        $Args += "--AppType $AppType"
    }
    if(-not [string]::IsNullOrWhiteSpace($VersionKey) -and $VersionKey -ne "`"`"") {
        $Args += "--VersionKey $VersionKey"
    }
    if(-not [string]::IsNullOrWhiteSpace($RepoOwner) -and $RepoOwner -ne "`"`"") {
        $Args += "--RepoOwner $RepoOwner"
    }
    if(-not [string]::IsNullOrWhiteSpace($RepoName) -and $RepoName -ne "`"`"") {
        $Args += "--RepoName $RepoName"
    }
    if(-not [string]::IsNullOrWhiteSpace($VersionNumber) -and $VersionNumber -ne "`"`"") {
        $Args += "--VersionNumber $VersionNumber"
    }
    if(-not [string]::IsNullOrWhiteSpace($HubAppVersionKey) -and $HubAppVersionKey -ne "`"`"") {
        $Args += "--HubAppVersionKey $HubAppVersionKey"
    }
    if(-not [string]::IsNullOrWhiteSpace($HubAdministrationType) -and $HubAdministrationType -ne "Default") {
        $Args += "--HubAdministrationType $HubAdministrationType"
    }
    if(-not [string]::IsNullOrWhiteSpace($InstallationSource) -and $InstallationSource -ne "Default") {
        $Args += "--InstallationSource $InstallationSource"
    }
    if(-not [string]::IsNullOrWhiteSpace($DestinationMachine) -and $DestinationMachine -ne "`"`"") {
        $Args += "--DestinationMachine $DestinationMachine"
    }
    if(-not [string]::IsNullOrWhiteSpace($Domain) -and $Domain -ne "`"`"") {
        $Args += "--Domain $Domain"
    }
    if(-not [string]::IsNullOrWhiteSpace($SiteName) -and $SiteName -ne "`"`"") {
        $Args += "--SiteName $SiteName"
    }
    if(-not [string]::IsNullOrWhiteSpace($InstallationUserName) -and $InstallationUserName -ne "`"`"") {
        $Args += "--InstallationUserName $InstallationUserName"
    }
    if(-not [string]::IsNullOrWhiteSpace($InstallationPassword) -and $InstallationPassword -ne "`"`"") {
        $Args += "--InstallationPassword $InstallationPassword"
    }
    if(-not [string]::IsNullOrWhiteSpace($Release)) {
        $Args += "--Release $Release"
    }
    if(-not [string]::IsNullOrWhiteSpace($VersionType)) {
        $Args += "--VersionType $VersionType"
    }
    if(-not [string]::IsNullOrWhiteSpace($IssueTitle) -and $IssueTitle -ne "`"`"") {
        $Args += "--IssueTitle $IssueTitle"
    }
    if(-not [string]::IsNullOrWhiteSpace($CredentialKey) -and $CredentialKey -ne "`"`"") {
        $Args += "--CredentialKey $CredentialKey"
    }
    if(-not [string]::IsNullOrWhiteSpace($UserName) -and $UserName -ne "`"`"") {
        $Args += "--UserName $UserName"
    }
    if(-not [string]::IsNullOrWhiteSpace($Password) -and $Password -ne "`"`"") {
        $Args += "--Password $Password"
    }
    if($IssueNumber -ne 0) {
        $Args += "--IssueNumber $IssueNumber"
    }
    if($StartIssue) {
        $Args += "--StartIssue true"
    }
    if(-not [string]::IsNullOrWhiteSpace($InstallTemplateName) -and $InstallTemplateName -ne "`"`"") {
        $Args += "--InstallTemplateName $InstallTemplateName"
    }
    if(-not [string]::IsNullOrWhiteSpace($InstallConfigurationName) -and $InstallConfigurationName -ne "`"`"") {
        $Args += "--InstallConfigurationName $InstallConfigurationName"
    }
    if($InstallSequence -ne 0) {
        $Args += "--InstallSequence $InstallSequence"
    }
    $ArgsText = $Args -join " "
    Write-Host "ArgsText: $ArgsText"
    $p = Start-Process -PassThru -FilePath "$($env:Xti_Dir)\Tools\XTI_AdminTool\XTI_AdminTool.exe" -ArgumentList $ArgsText -NoNewWindow
    $p.WaitForExit()
}
