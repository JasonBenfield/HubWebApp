param ([string] $versionType='Minor')
$env:DOTNET_ENVIRONMENT="Production"
$versionApp = $env:XTI_Tools + "\XTI_VersionApp\XTI_VersionApp.exe"
& $versionApp --no-launch-profile -- --ManageVersion:Command=New --ManageVersion:NewVersion:App=Hub --ManageVersion:NewVersion:Type=$versionType --ManageVersion:NewVersion:RepoOwner=JasonBenfield --ManageVersion:NewVersion:RepoName=HubWebApp