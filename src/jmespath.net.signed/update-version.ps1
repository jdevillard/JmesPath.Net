Function Get-ScriptDirectory { Split-Path -parent $PSCommandPath }

Write-Host $env:appveyor_build_version
Set-Content -Path "$(Get-ScriptDirectory)\project.json" -Value (Get-Content "$(Get-ScriptDirectory)\project.json").Replace("`"version`": `"42.43.44`",", "`"version`": `"$($env:appveyor_build_version)`",")