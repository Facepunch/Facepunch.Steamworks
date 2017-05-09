node ( 'vs2017' )
{
	stage 'Checkout'
		checkout scm

	stage 'Restore'
		bat 'nuget restore Facepunch.Steamworks.sln'
		
	stage 'Build Release'
		bat "\"${tool 'MSBuild'}\" Facepunch.Steamworks/Facepunch.Steamworks.csproj /p:Configuration=Release /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
		
	stage 'Build Debug'
		bat "\"${tool 'MSBuild'}\" Facepunch.Steamworks/Facepunch.Steamworks.csproj /p:Configuration=Debug /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
		
	stage 'Build Release NetCore'
		bat "dotnet restore Facepunch.Steamworks/Facepunch.Steamworks.NetCore.csproj"
		
	stage 'Build Release NetCore'
		bat "dotnet build Facepunch.Steamworks/Facepunch.Steamworks.NetCore.csproj --configuration Release"
		
	stage 'Build Debug NetCore'
		bat "dotnet build Facepunch.Steamworks/Facepunch.Steamworks.NetCore.csproj --configuration Debug"

	stage 'Archive'
		archiveArtifacts artifacts: 'Facepunch.Steamworks/bin/**/*'
}