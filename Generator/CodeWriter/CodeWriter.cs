using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generator;

namespace Generator
{
    public partial class CodeWriter
    {
        private SteamApiDefinition def;
		public CodeParser Parser;

		public CodeWriter( CodeParser parser, SteamApiDefinition def )
        {
			Parser = parser;

			this.def = def;
            WorkoutTypes();
        }

        public void ToFolder( string folder )
        {
            {
                sb = new StringBuilder();
                Header();
                Enums();
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamEnums.cs", sb.ToString() );
            }

            {
                sb = new StringBuilder();
                Header( "Steamworks.Data" );
                Types();
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamTypes.cs", sb.ToString() );
            }

            {
                sb = new StringBuilder();
                Header( "Steamworks.Data" );
                Structs();
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamStructs.cs", sb.ToString() );
            }

            {
                sb = new StringBuilder();
                Header( "Steamworks.Data" );
                Constants();
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamConstants.cs", sb.ToString() );
            }

			{
				GenerateGlobalFunctions( "SteamAPI", $"{folder}../Generated/SteamAPI.cs" );
				GenerateGlobalFunctions( "SteamGameServer", $"{folder}../Generated/SteamGameServer.cs" );
				GenerateGlobalFunctions( "SteamInternal", $"{folder}../Generated/SteamInternal.cs" );
			}

			{
				GenerateVTableClass( "ISteamApps", $"{folder}../Generated/Interfaces/ISteamApps.cs" );
				GenerateVTableClass( "ISteamUtils", $"{folder}../Generated/Interfaces/ISteamUtils.cs" );
				GenerateVTableClass( "ISteamParentalSettings", $"{folder}../Generated/Interfaces/ISteamParentalSettings.cs" );
				GenerateVTableClass( "ISteamMusic", $"{folder}../Generated/Interfaces/ISteamMusic.cs" );
				GenerateVTableClass( "ISteamVideo", $"{folder}../Generated/Interfaces/ISteamVideo.cs" );
				GenerateVTableClass( "ISteamUser", $"{folder}../Generated/Interfaces/ISteamUser.cs" );
				GenerateVTableClass( "ISteamMatchmakingServers", $"{folder}../Generated/Interfaces/ISteamMatchmakingServers.cs" );
				GenerateVTableClass( "ISteamFriends", $"{folder}../Generated/Interfaces/ISteamFriends.cs" );
				GenerateVTableClass( "ISteamGameServer", $"{folder}../Generated/Interfaces/ISteamGameServer.cs" );
				GenerateVTableClass( "ISteamGameServer", $"{folder}../Generated/Interfaces/ISteamGameServer.cs" );
				GenerateVTableClass( "ISteamScreenshots", $"{folder}../Generated/Interfaces/ISteamScreenshots.cs" );
				GenerateVTableClass( "ISteamUserStats", $"{folder}../Generated/Interfaces/ISteamUserStats.cs" );
				GenerateVTableClass( "ISteamUGC", $"{folder}../Generated/Interfaces/ISteamUGC.cs" );
				GenerateVTableClass( "ISteamRemoteStorage", $"{folder}../Generated/Interfaces/ISteamRemoteStorage.cs" );
				GenerateVTableClass( "ISteamInventory", $"{folder}../Generated/Interfaces/ISteamInventory.cs" );
			}
		}

        void WorkoutTypes()
        {
            foreach ( var c in def.typedefs )
            {
                if ( c.Name.StartsWith( "uint" ) || c.Name.StartsWith( "int" ) || c.Name.StartsWith( "lint" ) || c.Name.StartsWith( "luint" ) || c.Name.StartsWith( "ulint" ) )
                    continue;

                // Unused, messers
                if ( c.Name == "Salt_t" || c.Name == "compile_time_assert_type" || c.Name == "ValvePackingSentinel_t" || c.Name.Contains( "::" ) || c.Type.Contains( "(*)" ) )
                    continue;

                var type = c.Type;

                type = ToManagedType( type );

				TypeDefs.Add( c.Name, new TypeDef()
                {
                    Name = c.Name,
                    NativeType = c.Type,
                    ManagedType = type,
                } );
            }
        }

        private void Header( string NamespaceName = "Steamworks" )
        {
            WriteLine( "using System;" );
            WriteLine( "using System.Runtime.InteropServices;" );
            WriteLine( "using System.Linq;" );
            WriteLine( "using Steamworks.Data;" );
            WriteLine( "using System.Threading.Tasks;" );
            WriteLine();
            StartBlock( "namespace " + NamespaceName );
        }

        private void Footer()
        {
            EndBlock();
        }
    }
}
