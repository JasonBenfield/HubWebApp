param ([string] $branch)
$versionApp = $env:XTI_Tools + "\XTI_VersionApp\XTI_VersionApp.exe"
& $versionApp --no-launch-profile -- --ManageVersion:Command=EndPublish --ManageVersion:PublishVersion:Branch=$branch