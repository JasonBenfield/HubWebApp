param ([string] $envName='Staging', [string] $backupFilePath)

$currentDir = (Get-Item .).FullName
$env:DOTNET_ENVIRONMENT=$envName
Set-Location "$($env:XTI_Tools)\AppDbApp"
dotnet AppDbApp.dll --no-launch-profile -- --Command=restore --BackupFilePath $backupFilePath
Set-Location $currentDir

if( $LASTEXITCODE -ne 0 ) {
    Throw "Restore failed"
}