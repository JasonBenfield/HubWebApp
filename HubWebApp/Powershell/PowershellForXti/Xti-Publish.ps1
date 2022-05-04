function Xti-Publish {
    param (
        [ValidateSet("Production", “Development")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [ValidateSet("", "WebApp", "ServiceApp", "ConsoleApp", "Package")]
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppType = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $DestinationMachine = "",
        [switch] $NoInstall,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
		$Domain = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
		$SiteName = "",
        [ValidateSet("Default", "DB")]
        $HubAdministrationType = "Default"
    )
    $Command = "PublishAndInstall"
    if($NoInstall){
        $Command = "Publish"
    }
    Xti-Admin -EnvName $EnvName -Command $Command -AppName "`"$($AppName)`"" -AppType $AppType -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -DestinationMachine "`"$($DestinationMachine)`"" -Domain "`"$($Domain)`"" -SiteName "`"$($SiteName)`"" -HubAdministrationType $HubAdministrationType
}

function Xti-Build {
    param (
        [ValidateSet("Production", “Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [ValidateSet("", "WebApp", "ServiceApp", "ConsoleApp", "Package")]
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppType = "",
        [ValidateSet("Default", "DB")]
        $HubAdministrationType = "Default"
    )
    Xti-Admin -EnvName $EnvName -Command Build -AppName "`"$($AppName)`"" -AppType $AppType -HubAdministrationType $HubAdministrationType
}

function Xti-Setup {
    param (
        [ValidateSet("Production", “Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [ValidateSet("", "WebApp", "ServiceApp", "ConsoleApp", "Package")]
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppType = "",
        [ValidateSet("Default", "DB")]
        $HubAdministrationType = "Default",
        [ValidateSet("Default", "GitHub")]
        $InstallationSource = "Default"
    )
    Xti-Admin -EnvName $EnvName -Command Setup -AppName "`"$($AppName)`"" -AppType $AppType -HubAdministrationType $HubAdministrationType -InstallationSource $InstallationSource
}

function Xti-Install {
    param (
        [ValidateSet("Production", “Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppType = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $DestinationMachine = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
		$Domain = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
		$SiteName = "",
        [ValidateSet("Default", "DB")]
        $HubAdministrationType = "Default",
        [ValidateSet("Default", "GitHub")]
        $InstallationSource = "Default"
    )
    Xti-Admin -EnvName $EnvName -Command Install -AppName "`"$($AppName)`"" -AppType $AppType -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -DestinationMachine "`"$($DestinationMachine)`"" -Domain "`"$($Domain)`"" -SiteName "`"$($SiteName)`"" -HubAdministrationType $HubAdministrationType -InstallationSource $InstallationSource
}

function Xti-PublishLib {
    param (
        [ValidateSet("Production", “Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $AppName,
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $AppType,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName,
        [ValidateSet("Default", "DB")]
        $HubAdministrationType = "Default"
    )
    Xti-Admin -EnvName $EnvName -Command PublishLib -AppName "`"$($AppName)`"" -AppType $AppType -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -HubAdministrationType $HubAdministrationType
}

function Xti-AddInstallationUser {
    param (
        [ValidateSet("Production", “Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [ValidateSet("Default", "DB")]
        $HubAdministrationType = "Default"
    )
    Xti-Admin -EnvName $EnvName -Command AddInstallationUser -AppName "`"$($AppName)`"" -HubAdministrationType $HubAdministrationType
}

function Xti-AddSystemUser {
    param (
        [ValidateSet("Production", “Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppType = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $Domain = "",
        [ValidateSet("Default", "DB")]
        $HubAdministrationType = "Default"
    )
    Xti-Admin -EnvName $EnvName -Command AddSystemUser -AppName "`"$($AppName)`"" -AppType $AppType -Domain "`"$($Domain)`"" -HubAdministrationType $HubAdministrationType
}

function Xti-DecryptTempLog {
    param (
        [ValidateSet("Production", “Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName
    )
    Xti-Admin -EnvName $EnvName -Command DecryptTempLog
}

function Xti-UploadTempLog {
    param (
        [ValidateSet("Production", “Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName
    )
    Xti-Admin -EnvName $EnvName -Command UploadTempLog
}

function Xti-Admin {
    param (
        [ValidateSet("Production", “Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [ValidateSet("PublishAndInstall", “Build", "Setup", "Publish", "PublishLib", "Install", "NewVersion", "NewIssue", "StartIssue", "CompleteIssue", "AddInstallationUser", "AddSystemUser", "DecryptTempLog", "UploadTempLog", "ShowCredentials", "StoreCredentials")]
        [Parameter(Mandatory)]
        $Command,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppName,
        [ValidateSet("", "WebApp", “ServiceApp", "ConsoleApp", "Package")]
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppType,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName = "",
        $DestinationMachine = "",
		$Domain = "",
		$SiteName = "",
		$InstallationUserName = "",
		$InstallationPassword = "",
		$Release = "",
        [ValidateSet("", “major”, "minor", "patch")]
		$VersionType = "",
		$IssueTitle = "",
		$IssueNumber = 0,
        [switch] $StartIssue,
        [ValidateSet("Default", "DB")]
        $HubAdministrationType = "Default",
        [ValidateSet("Default", "GitHub")]
        $InstallationSource = "Default",
		$CredentialKey = "",
		$UserName = "",
		$Password = ""
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
    if(-not [string]::IsNullOrWhiteSpace($RepoOwner) -and $RepoOwner -ne "`"`"") {
        $Args += "--RepoOwner $RepoOwner"
    }
    if(-not [string]::IsNullOrWhiteSpace($RepoName) -and $RepoName -ne "`"`"") {
        $Args += "--RepoName $RepoName"
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
    $ArgsText = $Args -join " "
    Write-Host "ArgsText: $ArgsText"
    $p = Start-Process -PassThru -FilePath "$($env:Xti_Dir)\Tools\XTI_AdminTool\XTI_AdminTool.exe" -ArgumentList $ArgsText -NoNewWindow
    $p.WaitForExit()
}
