node {
	stage 'Checkout'
		checkout scm

	stage 'Build'
		bat 'nuget restore Facepunch.Steamworks.sln'
		bat "\"${tool 'MSBuild'}\" Facepunch.Steamworks/Facepunch.Steamworks.csproj /p:Configuration=Release /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
		bat "\"${tool 'MSBuild'}\" Facepunch.Steamworks/Facepunch.Steamworks.csproj /p:Configuration=Debug /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"

	stage 'Archive'
		archiveArtifacts Release: 'Facepunch.Steamworks/bin/Release/**'
		archiveArtifacts Debug: 'Facepunch.Steamworks/bin/Debug/**'

}