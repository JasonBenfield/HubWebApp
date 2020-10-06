param ([string] $envName='Development')

$ErrorActionPreference = "Stop"

$env:DOTNET_ENVIRONMENT=$envName
$env:ASPNETCORE_ENVIRONMENT=$envName

$timestamp = Get-Date -Format "yyMMdd_HHmmssfff"
$backupFilePath = "$($env:ProgramData)\XTI\Backups\$envName\app_$timestamp.bak"
if($envName -eq "Production" -or $envName -eq "Staging") {
    Write-Progress -Activity "Publishing" -Status "Backuping up the app database" -PercentComplete 10
	./backupappdb.ps1 -envName "Production" -backupFilePath $backupFilePath
    $env:DOTNET_ENVIRONMENT=$envName
    $env:ASPNETCORE_ENVIRONMENT=$envName
}
$activity = "Publishing to $envName"
if($envName -eq "Staging") { 
    Write-Progress -Activity $activity -Status "Restoring the app database" -PercentComplete 15
	./restoreappdb.ps1 -envName $envName -backupFilePath $backupFilePath
}

Write-Progress -Activity $activity -Status "Updating the app database" -PercentComplete 18
& "$($env:XTI_Tools)\app_db_update.ps1" -env $envName

if ($envName -eq "Test"){
    Write-Progress -Activity $activity -Status "Resetting the app database" -PercentComplete 20
	./resetappdb.ps1 -envName $envName
}

Write-Progress -Activity $activity -Status "Generating the api" -PercentComplete 30
./apigenerator.ps1 -envName $envName

Write-Progress -Activity $activity -Status "Running web pack" -PercentComplete 40
./runwebpack.ps1

Write-Progress -Activity $activity -Status "Building solution" -PercentComplete 50
dotnet build 

Write-Progress -Activity $activity -Status "Setting up Hub Web App" -PercentComplete 60
./hubsetup.ps1 -envName $envName

if ($envName -eq "Test") {
    Write-Progress -Activity $activity -Status "Creating user" -PercentComplete 70
    ./xtiuser.ps1 -envName $envName -credentialKey HubAdmin -userName HubAdmin -roleNames Admin
}

Write-Progress -Activity $activity -Status "Publishing website" -PercentComplete 80
& "$($env:XTI_Tools)\publishapp.ps1" -appKey Hub -envName $envName -projectDir ./Apps/HubWebApp

Write-Progress -Activity $activity -Status "Finished publishing to $envName" -Completed