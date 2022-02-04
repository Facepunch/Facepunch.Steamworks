set buildPath=%~dp0\Facepunch.Steamworks\bin\Release\net46
set outPath=%~dp0\UnityPlugin

xcopy /yf "%buildPath%\Facepunch.Steamworks.Posix.dll" "%outPath%\Facepunch.Steamworks.Posix.dll"
xcopy /yf "%buildPath%\Facepunch.Steamworks.Posix.xml" "%outPath%\Facepunch.Steamworks.Posix.xml"
xcopy /yf "%buildPath%\Facepunch.Steamworks.Posix.pdb" "%outPath%\Facepunch.Steamworks.Posix.pdb"
xcopy /yf "%buildPath%\Facepunch.Steamworks.Win32.dll" "%outPath%\Facepunch.Steamworks.Win32.dll"
xcopy /yf "%buildPath%\Facepunch.Steamworks.Win32.xml" "%outPath%\Facepunch.Steamworks.Win32.xml"
xcopy /yf "%buildPath%\Facepunch.Steamworks.Win32.pdb" "%outPath%\Facepunch.Steamworks.Win32.pdb"
xcopy /yf "%buildPath%\Facepunch.Steamworks.Win64.dll" "%outPath%\Facepunch.Steamworks.Win64.dll"
xcopy /yf "%buildPath%\Facepunch.Steamworks.Win64.xml" "%outPath%\Facepunch.Steamworks.Win64.xml"
xcopy /yf "%buildPath%\Facepunch.Steamworks.Win64.pdb" "%outPath%\Facepunch.Steamworks.Win64.pdb"