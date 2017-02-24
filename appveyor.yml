version: '1.0.{build}'

configuration:
- Debug
- Release

platform: Any CPU

environment:
  # Don't report back to the mothership
    DOTNET_CLI_TELEMETRY_OPTOUT: 1
    init:
  - ps: $Env:LABEL = "CI" + $Env:APPVEYOR_BUILD_NUMBER.PadLeft(5, "0")

before_build:
- appveyor-retry dotnet restore -v Minimal

build_script:
- dotnet build "src\jmespath.net" -c %CONFIGURATION% --no-dependencies --version-suffix %LABEL%

after_build:

test_script:
- dotnet test "test\jmespath.net.tests" -c %CONFIGURATION%

cache:
- '%USERPROFILE%\.nuget\packages'

on_finish:
