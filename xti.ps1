Import-Module PowershellForXti -Force

function Xti-AddHubDbMigration {
    param ([Parameter(Mandatory)]$Name)
    $env:DOTNET_ENVIRONMENT="Development"
    dotnet ef --startup-project ./HubWebApp/Internal/HubDbTool migrations add $Name --project ./HubWebApp/Internal/XTI_HubDB.EF.SqlServer
}

function Xti-RemoveLastHubDbMigration {
	param ()
	$env:DOTNET_ENVIRONMENT="Development"
	dotnet ef --startup-project ./HubWebApp/Internal/HubDbTool migrations remove --project ./HubWebApp/Internal/XTI_HubDB.EF.SqlServer
}

function Xti-ResetHubDb {
    param (
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        [ValidateSet(“Development", "Production", "Staging", "Test")]
        [string] $EnvName='Test',
        [switch] $Force
    )
    dotnet run --environment $EnvName --project ./HubWebApp/Internal/HubDbTool --Command reset --Force $Force
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
    dotnet run --environment $EnvName --project ./HubWebApp/Internal/HubDbTool --Command backup --BackupFilePath=$BackupFilePath
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
    dotnet run --environment $EnvName --project ./HubWebApp/Internal/HubDbTool --Command restore --BackupFilePath $BackupFilePath
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
    dotnet run --environment $EnvName --project ./HubWebApp/Internal/HubDbTool --Command update
    if( $LASTEXITCODE -ne 0 ) {
        Throw "Update failed"
    }
}

function Xti-UpdateNpm {
	Start-Process -FilePath "cmd.exe" -WorkingDirectory HubWebApp/Apps/HubWebApp -ArgumentList "/c", "npm install @jasonbenfield/sharedwebapp@latest"
	Start-Process -FilePath "cmd.exe" -WorkingDirectory AuthenticatorWebApp/Apps/AuthenticatorWebApp -ArgumentList "/c", "npm install @jasonbenfield/sharedwebapp@latest"
}