param ([string] $branch)
$env:DOTNET_ENVIRONMENT="Production"
$versionApp = $env:XTI_Tools + "\XTI_VersionApp\XTI_VersionApp.exe"
& $versionApp --no-launch-profile -- --ManageVersion:Command=BeginPublish --ManageVersion:PublishVersion:Branch=$branch