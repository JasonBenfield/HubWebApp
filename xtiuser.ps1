param (
    [string] $envName='Production', 
    [string] $credentialKey, 
    [string] $userName, 
    [string] $password='', 
    [string] $roleNames
)

$currentDir = (Get-Item .).FullName
$env:DOTNET_ENVIRONMENT=$envName
Set-Location "$($env:XTI_Tools)\XTI_UserApp"
dotnet XTI_UserApp.dll --no-launch-profile -- --AppKey=Hub --CredentialKey $credentialKey --UserName $userName --Password $password RoleNames $roleNames
Set-Location $currentDir

if( $LASTEXITCODE -ne 0 ) {
    Throw "Unable to create user"
}