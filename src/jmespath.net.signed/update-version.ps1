Function Get-ScriptDirectory { Split-Path -parent $PSCommandPath }

$attribute = Get-Content "$(Get-ScriptDirectory)\Properties\AssemblyInfo.cs" |? { $_ -match "AssemblyFileVersion" } | Select-Object -First 1
$attribute -match '[0-9]+(\.[0-9]+){2,3}'
$version = $matches[0]
Set-Content -Path "$(Get-ScriptDirectory)\project.json" -Value (Get-Content "$(Get-ScriptDirectory)\project.json").Replace("`"version`": `"42.43.44.45`",", "`"version`": `"$($version)`",")