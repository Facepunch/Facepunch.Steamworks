dotnet clean
dotnet build Facepunch.Steamworks/Facepunch.Steamworks.csproj -r win-x86 -c Release
dotnet build Facepunch.Steamworks/Facepunch.Steamworks.csproj -r win-x64 -c Release
dotnet build Facepunch.Steamworks/Facepunch.Steamworks.csproj -r linux-x86 -c Release
dotnet build Facepunch.Steamworks/Facepunch.Steamworks.csproj -r linux-x64 -c Release
nuget pack Facepunch.Steamworks/Facepunch.Steamworks.multiplatform.nuspec