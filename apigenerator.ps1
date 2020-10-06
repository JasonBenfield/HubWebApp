param ([string] $envName='Production')

$currentDir = (Get-Item .).FullName
$env:DOTNET_ENVIRONMENT=$envName
Set-Location Apps/HubApiGeneratorApp
dotnet run
Set-Location $currentDir