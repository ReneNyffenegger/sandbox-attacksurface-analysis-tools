dev-env

#msbuild C:\Users\Rene\github\sandbox-attacksurface-analysis-tools\EditSection\EditSection.csproj
msbuild C:\Users\Rene\github\sandbox-attacksurface-analysis-tools\NtApiDotNet\NtApiDotNet.csproj
msbuild C:\Users\Rene\github\sandbox-attacksurface-analysis-tools\NtObjectManager\NtObjectManager.csproj
# msbuild C:\Users\Rene\github\sandbox-attacksurface-analysis-tools\NtApiDotNet\NtApiDotNet.Build.csproj

cp C:\Users\Rene\github\sandbox-attacksurface-analysis-tools\bin\Debug\NtObjectManager.dll NtObjectManager

add-type -path "$pwd\bin\debug\NtApiDotNet.dll"

# cd NtObjectManager\
Import-Module "$pwd\NtObjectManager\NtObjectManager.psd1"

# ls NtObject:\KernelObjects
ls NtObject:\
