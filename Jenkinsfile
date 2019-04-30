node ( 'vs2017' )
{
	stage 'Checkout'
		checkout scm

	stage 'Restore'
		bat "dotnet restore Facepunch.Steamworks/Facepunch.Steamworks.csproj"
		
	stage 'Build Release'
		bat "dotnet build Facepunch.Steamworks/Facepunch.Steamworks.csproj --configuration Release"
		
	stage 'Build Debug'
		bat "dotnet build Facepunch.Steamworks/Facepunch.Steamworks.csproj --configuration Debug"

	stage 'Archive'
		archiveArtifacts artifacts: 'Facepunch.Steamworks/bin/**/*'
		bat( "xcopy 'Facepunch.Steamworks/bin/Debug/netstandard2.0/Facepunch.Steamworks.dll' 'UnityPlugin/Facepunch.Steamworks.dll' /O /X /E /H /K" )
		bat( "xcopy 'Facepunch.Steamworks/bin/Debug/netstandard2.0/Facepunch.Steamworks.pdb' 'UnityPlugin/Facepunch.Steamworks.pdb' /O /X /E /H /K" )
		bat( "xcopy 'Facepunch.Steamworks/bin/Debug/netstandard2.0/Facepunch.Steamworks.xml' 'UnityPlugin/Facepunch.Steamworks.xml' /O /X /E /H /K" )
		archiveArtifacts unityplugin: 'UnityPlugin/**/*'
}