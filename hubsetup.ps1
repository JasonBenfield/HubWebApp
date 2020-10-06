param ([string] $envName='Development')

$env:DOTNET_ENVIRONMENT=$envName
$env:ASPNETCORE_ENVIRONMENT=$envName

$currentDir = (Get-Item .).FullName
Set-Location Apps/HubSetupConsoleApp
dotnet run --no-launch-profile
Set-Location $currentDir

if( $LASTEXITCODE -ne 0 ) {
    Throw "Hub setup failed"
}