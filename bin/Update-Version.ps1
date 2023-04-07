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
    Function Update-CsprojVersion {
        param([string]$path, [string]$version)
        [xml]$document = Get-Content -Path $path -Raw
        $newPrefix = $document.Project.PropertyGroup.VersionPrefix.Replace("42.43.44", $version)
        $document.Project.PropertyGroup.VersionPrefix = $newPrefix
        $document.Save($path)

        Write-Host "Updated version of $path to $version." -ForegroundColor Cyan
    }
    Function Update-NuspecVersion {
        param([string]$path, [string]$version)
        [xml]$document = Get-Content -Path $path -Raw
        $newPrefix = $document.package.metadata.version.Replace("42.43.44", $version)
        $document.package.metadata.version = $newPrefix
        $document.Save($path)

        Write-Host "Updated version of $path to $version." -ForegroundColor Cyan
    }
}

PROCESS {

    "../src/jmespath.net/jmespath.net.csproj", `
    "../src/jmespath.net.parser/jmespath.net.parser.csproj" |% {

        Update-CsprojVersion `
            -Version $version `
            (Join-Path `
                -Path (Get-ScriptDirectory) `
                -ChildPath $_ `
            )
    }

    "../src/jmespath.net.parser/jmespath.net.parser.nuspec" |% {

        Update-NuspecVersion `
            -Version $version `
            (Join-Path `
                -Path (Get-ScriptDirectory) `
                -ChildPath $_ `
            )
    }
}