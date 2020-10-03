$currentDir = (Get-Item .).FullName
Set-Location Apps/HubApiGeneratorApp
dotnet run
Set-Location $currentDir