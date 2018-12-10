Function Get-ScriptDirectory { Split-Path -parent $PSCommandPath }

$newVersion = $env:appveyor_build_version

Write-Host $newVersion

$project = "$(Get-ScriptDirectory)\jmespath.net.signed.csproj"
[xml]$document = Get-Content -Path $project -Raw
$document.Project.PropertyGroup.VersionPrefix = $document.Project.PropertyGroup.VersionPrefix.Replace("42.43.44", $newVersion)
$document.Save($project)