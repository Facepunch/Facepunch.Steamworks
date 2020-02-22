node ( 'vs2017' )
{
	try
	{
		stage 'Checkout'
			checkout scm

		stage 'Restore'
			bat "dotnet restore Facepunch.Steamworks/Facepunch.Steamworks.Win32.csproj"
			bat "dotnet restore Facepunch.Steamworks/Facepunch.Steamworks.Win64.csproj"
			bat "dotnet restore Facepunch.Steamworks/Facepunch.Steamworks.Posix.csproj"
			
		stage 'Build Release'
			bat "dotnet build Facepunch.Steamworks/Facepunch.Steamworks.Win32.csproj --configuration Release"
			bat "dotnet build Facepunch.Steamworks/Facepunch.Steamworks.Win64.csproj --configuration Release"
			bat "dotnet build Facepunch.Steamworks/Facepunch.Steamworks.Posix.csproj --configuration Release"
			
		stage 'Build Debug'
			bat "dotnet build Facepunch.Steamworks/Facepunch.Steamworks.Win32.csproj --configuration Debug"
			bat "dotnet build Facepunch.Steamworks/Facepunch.Steamworks.Win64.csproj --configuration Debug"
			bat "dotnet build Facepunch.Steamworks/Facepunch.Steamworks.Posix.csproj --configuration Debug"

		stage 'Archive'
			archiveArtifacts artifacts: 'Facepunch.Steamworks/bin/**/*'
			bat( "copy /Y Facepunch.Steamworks\\bin\\Debug\\netstandard2.0\\Facepunch.Steamworks.Win32.dll UnityPlugin\\" )
			bat( "copy /Y Facepunch.Steamworks\\bin\\Debug\\netstandard2.0\\Facepunch.Steamworks.Win32.pdb UnityPlugin\\" )
			bat( "copy /Y Facepunch.Steamworks\\bin\\Debug\\netstandard2.0\\Facepunch.Steamworks.Win32.xml UnityPlugin\\" )
			bat( "copy /Y Facepunch.Steamworks\\bin\\Debug\\netstandard2.0\\Facepunch.Steamworks.Win64.dll UnityPlugin\\" )
			bat( "copy /Y Facepunch.Steamworks\\bin\\Debug\\netstandard2.0\\Facepunch.Steamworks.Win64.pdb UnityPlugin\\" )
			bat( "copy /Y Facepunch.Steamworks\\bin\\Debug\\netstandard2.0\\Facepunch.Steamworks.Win64.xml UnityPlugin\\" )
			bat( "copy /Y Facepunch.Steamworks\\bin\\Debug\\netstandard2.0\\Facepunch.Steamworks.Posix.dll UnityPlugin\\" )
			bat( "copy /Y Facepunch.Steamworks\\bin\\Debug\\netstandard2.0\\Facepunch.Steamworks.Posix.pdb UnityPlugin\\" )
			bat( "copy /Y Facepunch.Steamworks\\bin\\Debug\\netstandard2.0\\Facepunch.Steamworks.Posix.xml UnityPlugin\\" )
			archiveArtifacts artifacts: 'UnityPlugin/**/*'
	}
	finally
	{
		cleanWs()
	}
}