Import-Module PowershellForXti -Force

if(Test-Path ".\xti.private.ps1"){
. .\xti.Private.ps1
}

$BaseXtiPublish = ${Function:Xti-Publish}

function Xti-Publish {
    param(
        [ValidateSet("Production", "Development")]
        [string] $EnvName="Development"
    )
    $DestinationMachine = Get-DestinationMachine -EnvName $EnvName
    $PsBoundParameters.Add("DestinationMachine", $DestinationMachine)
    $Domain = Get-Domain -EnvName $EnvName
    $PsBoundParameters.Add("Domain", $Domain)
    $SiteName = Get-SiteName -EnvName $EnvName
    $PsBoundParameters.Add("SiteName", $SiteName)
    & $BaseXtiPublish @PsBoundParameters
}

$BaseXtiInstall = ${Function:Xti-Install}

function Xti-Install {
    param(
        [ValidateSet("Development", "Production", "Staging", "Test")]
        $EnvName = "Development",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppType = "",
        [ValidateSet("Default", "DB")]
        $HubAdministrationType = "Default",
        [ValidateSet("Default", "GitHub")]
        $InstallationSource = "Default"
    )
    & $BaseXtiInstall @PsBoundParameters
}

$BaseXtiPublishLib = ${Function:Xti-PublishLib}

function Xti-PublishLib {
    param (
        [ValidateSet("Production", "Development", "Staging", "Test")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $EnvName,
        [ValidateSet("Default", "DB")]
        $HubAdministrationType = "Default"
    )
    & $BaseXtiPublishLib @PsBoundParameters
}

function Add-HubDBMigrations {
    param ([Parameter(Mandatory)]$Name)
    $env:DOTNET_ENVIRONMENT="Development"
    dotnet ef --startup-project ./Tools/HubDbTool migrations add $Name --project ./HubWebApp/Internal/XTI_HubDB.EF.SqlServer
}

function Xti-ResetHubDb {
    param (
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        [ValidateSet(“Development", "Production", "Staging", "Test")]
        [string] $EnvName='Test',
        [switch] $Force
    )
    dotnet run --environment $EnvName --project ./Tools/HubDbTool --Command reset --Force $Force
    if( $LASTEXITCODE -ne 0 ) {
        Throw "Reset failed"
    }
}

function Xti-BackupHubDb {
    param (
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        [ValidateSet(“Development", "Production", "Staging", "Test")]
        [string] $EnvName='Production', 
        [Parameter(Mandatory)]
        [string] $BackupFilePath
    )
    if($EnvName -eq "Production" -or $EnvName -eq "Staging") {
        $dirPath = [System.IO.Path]::GetDirectoryName($BackupFilePath)
        if(-not(Test-Path $dirPath -PathType Container)) { 
            New-Item -ItemType Directory -Force -Path $dirPath
        }
    }
    dotnet run --environment $EnvName --project ./Tools/HubDbTool --Command backup --BackupFilePath=$BackupFilePath
    if( $LASTEXITCODE -ne 0 ) {
        Throw "Backup failed"
    }
}

function Xti-RestoreHubDb {
    param (
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        [ValidateSet(“Development", "Production", "Staging", "Test")]
        [string] $EnvName='Staging', 
        [Parameter(Mandatory)]
        [string] $BackupFilePath
    )
    dotnet run --environment $EnvName --project ./Tools/HubDbTool --Command restore --BackupFilePath $BackupFilePath
    if( $LASTEXITCODE -ne 0 ) {
        Throw "Restore failed"
    }
}

function Xti-UpdateHubDb {
    param (
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        [ValidateSet(“Development", "Production", "Staging", "Test")]
        $EnvName='Test'
    )
    dotnet run --environment $EnvName --project ./Tools/HubDbTool --Command update
    if( $LASTEXITCODE -ne 0 ) {
        Throw "Update failed"
    }
}
