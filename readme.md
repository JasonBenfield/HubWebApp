# Setup XTI Environment
Set XTI_Dir environment variable to override the default XTI directory of c:\\Xti

Download Tools.zip from release

Extract to %XTI_Dir%

Download Powershell.zip from release

Extract to %XTI_Dir%

Add %XTI_Dir%\\Powershell to PSModulePath environment variable

Add appsettings.json to %XTI_DIR%\\AppData\\<EnvironmentName> see [sample appsettings.json](sample.appsettings.json)

Run Powershell Commands:

Xti-Install -EnvName <EnvironmentName> -RepoOwner JasonBenfield -RepoName HubWebApp -AppName Hub -AppType WebApp -InstallationSource GitHub -DestinationMachine <MachineName> -SiteName <SiteName> -Domain <Domain>

Xti-Install -EnvName <EnvironmentName> -RepoOwner JasonBenfield -RepoName HubWebApp -AppName Authenticator -AppType WebApp -InstallationSource GitHub -DestinationMachine <MachineName> -SiteName <SiteName> -Domain <Domain>

Xti-Install -EnvName <EnvironmentName> -RepoOwner JasonBenfield -RepoName HubWebApp -AppName Support -AppType ServiceApp -InstallationSource GitHub -DestinationMachine <MachineName>

Run Command:

sc create xti_install_service start= auto displayname= "XTI Install Service" binpath= "C:\XTI\Tools\XTI_InstallService\XTI_InstallService.exe"

Start XTI Install Service

# Apply certificate to iis express for a custom domain
c:\\Program Files (x86)\\IIS Express\\IisExpressAdminCmd.exe setupsslUrl -url:https://development.domain.com:44303/ -CertHash:THUMB