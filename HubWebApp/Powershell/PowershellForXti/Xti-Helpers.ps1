
function ThrowIfNotSolutionDir {
    if($env:SolutionDir -ne $pwd.Path) {
        throw "Invalid directory `"$($pwd.Path)`". The solution directory is `"$($env:SolutionDir)`"."
    }
}

function CD-Solution {
    cd $env:SolutionDir
}

function CD-Repo {
    param (
        [Parameter(Mandatory)]
        $RepoName = "",
        $RepoOwner = ""
    )
    $xtiDir = $env:XTI_DIR
    if([string]::IsNullOrWhiteSpace($xtiDir)) {
        $xtiDir = "c:\xti"
    }
    $repoOwnerDir = ""
    if([string]::IsNullOrWhiteSpace($RepoOwner)) {
        $currentDir = (get-item $pwd).FullName
        $xtiDirRegex = $xtiDir.replace('\', '\\');
        if($currentDir.toLower() -match "^$($xtiDirRegex)\\src\\\w+$") {
            $repoOwnerDir = $currentDir
        }
        else{
            $repoOwnerDir = (get-item $pwd).Parent.FullName
        }
    }
    else {
        $repoOwnerDir = "$($xtiDir)\src\$($RepoOwner)"
    }
    $slnDir = "$($repoOwnerDir)\$($RepoName)"
    cd $slnDir
    $env:SolutionDir = $slnDir
}