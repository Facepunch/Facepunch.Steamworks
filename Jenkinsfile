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
}