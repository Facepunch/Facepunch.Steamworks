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
		copy 'Facepunch.Steamworks/bin/Debug/netstandard2.0/Facepunch.Steamworks.dll' 'UnityPlugin/Facepunch.Steamworks.dll'
		copy 'Facepunch.Steamworks/bin/Debug/netstandard2.0/Facepunch.Steamworks.pdb' 'UnityPlugin/Facepunch.Steamworks.pdb'
		copy 'Facepunch.Steamworks/bin/Debug/netstandard2.0/Facepunch.Steamworks.xml' 'UnityPlugin/Facepunch.Steamworks.xml'
		archiveArtifacts unityplugin: 'UnityPlugin/**/*'
}