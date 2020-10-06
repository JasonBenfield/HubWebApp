param ([string] $envName='Production', [string] $backupFilePath)

$currentDir = (Get-Item .).FullName
$env:DOTNET_ENVIRONMENT=$envName
Set-Location "$($env:XTI_Tools)\AppDbApp"
dotnet AppDbApp.dll --no-launch-profile -- --Command=backup --BackupFilePath $backupFilePath
Set-Location $currentDir

if( $LASTEXITCODE -ne 0 ) {
    Throw "Backup failed"
}