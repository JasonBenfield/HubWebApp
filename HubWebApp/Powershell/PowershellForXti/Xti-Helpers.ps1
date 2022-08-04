
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
    if([string]::IsNullOrWhiteSpace($RepoOwner)) {
        $slnDir = (get-item $pwd).Parent.FullName
    }
    else {
        $slnDir = $env:XTI_DIR
        if([string]::IsNullOrWhiteSpace($slnDir)) {
            $slnDir = "c:\xti"
        }
        $slnDir = "$($slnDir)\src\$($RepoOwner)"
    }
    $slnDir = "$($slnDir)\$($RepoName)"
    cd $slnDir
    $env:SolutionDir = $slnDir
}