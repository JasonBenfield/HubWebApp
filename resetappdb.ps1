param ([string] $envName='Test')

$currentDir = (Get-Item .).FullName
$env:DOTNET_ENVIRONMENT=$envName
Set-Location "$($env:XTI_Tools)\AppDbApp"
dotnet AppDbApp.dll --no-launch-profile -- --Command=reset
Set-Location $currentDir