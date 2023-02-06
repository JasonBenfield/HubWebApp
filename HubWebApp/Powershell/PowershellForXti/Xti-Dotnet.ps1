
function Xti-InstallTemplates {
    Xti-Dotnet -Command Install
}

function Xti-UninstallTemplates {
    Xti-Dotnet -Command Uninstall
}

function Xti-NewSln {
    param (
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $RepoOwner = "",
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $RepoName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $SrcDir = ""
    )
    Xti-Dotnet -RepoOwner "`"$($RepoOwner)`"" -RepoName "`"$($RepoName)`"" -SrcDir "`"$($SrcDir)`""
    CD-Repo -RepoOwner $RepoOwner -RepoName $RepoName
}

function Xti-NewWebApp {
    param (
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $Domain = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $SrcDir = ""
    )
    ThrowIfNotSolutionDir
    Xti-Dotnet -RepoOwner "`"$($RepoOwner)`""  -RepoName "`"$($RepoName)`""  -AppName "`"$($AppName)`""  -AppType WebApp -Domain "`"$($Domain)`"" -SrcDir "`"$($SrcDir)`""
}

function Xti-NewWebService {
    param (
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $Domain = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $SrcDir = ""
    )
    ThrowIfNotSolutionDir
    Xti-Dotnet -RepoOwner "`"$($RepoOwner)`""  -RepoName "`"$($RepoName)`""  -AppName "`"$($AppName)`""  -AppType WebService -Domain "`"$($Domain)`"" -SrcDir "`"$($SrcDir)`""
}

function Xti-NewWebPackage {
    param (
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $Domain = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $SrcDir = ""
    )
    ThrowIfNotSolutionDir
    Xti-Dotnet -RepoOwner "`"$($RepoOwner)`""  -RepoName "`"$($RepoName)`""  -AppName "`"$($AppName)`""  -AppType WebPackage -Domain "`"$($Domain)`"" -SrcDir "`"$($SrcDir)`""
}

function Xti-NewServiceApp {
    param (
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $SrcDir = ""
    )
    ThrowIfNotSolutionDir
    Xti-Dotnet -RepoOwner "`"$($RepoOwner)`""  -RepoName "`"$($RepoName)`""  -AppName "`"$($AppName)`""  -AppType ServiceApp -SrcDir "`"$($SrcDir)`""
}

function Xti-NewConsoleApp {
    param (
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $SrcDir = ""
    )
    ThrowIfNotSolutionDir
    Xti-Dotnet -RepoOwner "`"$($RepoOwner)`""  -RepoName "`"$($RepoName)`""  -AppName "`"$($AppName)`""  -AppType ConsoleApp -SrcDir "`"$($SrcDir)`""
}

function Xti-NewApiGroup {
    param (
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [ValidateSet("WebApp", “ServiceApp", "ConsoleApp")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $AppType,
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $GroupName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $SrcDir = ""
    )
    ThrowIfNotSolutionDir
    Xti-Dotnet -Command ApiGroup -RepoOwner "`"$($RepoOwner)`""  -RepoName "`"$($RepoName)`""  -AppName "`"$($AppName)`"" -GroupName "`"$($GroupName)`""  -AppType $AppType -SrcDir "`"$($SrcDir)`""
}

function Xti-NewTests {
    param (
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $AppName = "",
        [ValidateSet("WebApp", "ServiceApp", "ConsoleApp")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName = $true)]
        $AppType,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $TestType = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $SrcDir = ""
    )
    ThrowIfNotSolutionDir
    Xti-Dotnet -Command Tests -RepoOwner "`"$($RepoOwner)`""  -RepoName "`"$($RepoName)`""  -AppName "`"$($AppName)`"" -GroupName "`"$($GroupName)`""  -AppType $AppType -SrcDir "`"$($SrcDir)`"" -TestType "`"$($TestType)`""
}

function Xti-Dotnet {
    param (
        [ValidateSet("", "Install", "Uninstall", "ApiGroup", "Tests")]
        $Command,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppName,
        [ValidateSet("", "WebApp", "WebService", "WebPackage", "ServiceApp", "ConsoleApp", "Package")]
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $AppType,
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoOwner = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $RepoName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $GroupName = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $SrcDir = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $Domain = "",
        [Parameter(ValueFromPipelineByPropertyName = $true)]
        $TestType = ""
    )
    $ErrorActionPreference = "Stop"
    if($Command -ne "Install" -and $Command -ne "Uninstall") {
        $curDir = Get-Location
        if([string]::IsNullOrWhiteSpace($RepoOwner) -or $RepoOwner -eq "`"`"") {
            $RepoOwner = (Get-Item -Path $curDir).Parent.Name
            if([string]::IsNullOrWhiteSpace($SrcDir) -or $SrcDir -eq "`"`"") {
                $SrcDir = (Get-Item -Path $curDir).Parent.Parent.FullName
            }
        }
        if([string]::IsNullOrWhiteSpace($RepoName) -or $RepoName -eq "`"`"") {
            $RepoName = (Get-Item -Path $curDir).Name
        }
    }
    $Args = @()
    $Args += "--environment Production"
    if(-not [string]::IsNullOrWhiteSpace($Command) -and $Command -ne "`"`"") {
        $Args += "--Command $Command"
    }
    if(-not [string]::IsNullOrWhiteSpace($AppName) -and $AppName -ne "`"`"") {
        $Args += "--AppName $AppName"
    }
    if(-not [string]::IsNullOrWhiteSpace($AppType)) {
        $Args += "--AppType $AppType"
    }
    if(-not [string]::IsNullOrWhiteSpace($RepoOwner) -and $RepoOwner -ne "`"`"") {
        $Args += "--RepoOwner $RepoOwner"
    }
    if(-not [string]::IsNullOrWhiteSpace($RepoName) -and $RepoName -ne "`"`"") {
        $Args += "--RepoName $RepoName"
    }
    if(-not [string]::IsNullOrWhiteSpace($GroupName) -and $GroupName -ne "`"`"") {
        $Args += "--GroupName $GroupName"
    }
    if(-not [string]::IsNullOrWhiteSpace($SrcDir) -and $SrcDir -ne "`"`"") {
        $Args += "--SrcDir $SrcDir"
    }
    if(-not [string]::IsNullOrWhiteSpace($Domain) -and $Domain -ne "`"`"") {
        $Args += "--Domain $Domain"
    }
    if(-not [string]::IsNullOrWhiteSpace($TestType) -and $TestType -ne "`"`"") {
        $Args += "--TestType $TestType"
    }
    $ArgsText = $Args -join " "
    Write-Host "ArgsText: $ArgsText"
    $p = Start-Process -PassThru -FilePath "$($env:Xti_Dir)\Tools\XTI_DotnetTool\XTI_DotnetTool.exe" -ArgumentList $ArgsText -NoNewWindow
    $p.WaitForExit()
}