[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    $version = $ENV:appveyor_build_version
)

BEGIN {

    if (-not $version -or [String]::IsNullOrWhitespace($version))
    {
        Write-Host "Please, specify a valid version number." -ForegroundColor Red
        throw;
    }

    Function Get-ScriptDirectory { Split-Path -parent $PSCommandPath }
    Function Update-Version {
        param([string]$path, [string]$version)
        [xml]$document = Get-Content -Path $path -Raw
        $newPrefix = $document.Project.PropertyGroup.VersionPrefix.Replace("42.43.44", $version)
        $document.Project.PropertyGroup.VersionPrefix = $newPrefix
        $document.Save($path)

        Write-Host "Updated version of $path to $version." -ForegroundColor Cyan
    }
}

PROCESS {

    "../src/jmespath.net/jmespath.net.csproj", `
    "../src/jmespath.net.parser/jmespath.net.parser.csproj" |% {

        Update-Version `
            -Version $version `
            (Join-Path `
                -Path (Get-ScriptDirectory) `
                -ChildPath $_ `
            )
    }
}