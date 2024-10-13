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
        public static CodeWriter Current { get; private set; }

        private SteamApiDefinition def;

		public CodeWriter( SteamApiDefinition def )
        {
			this.def = def;
            Current = this;
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
                Header();
                CustomEnums();
                Footer();
                System.IO.File.WriteAllText( $"{folder}CustomEnums.cs", sb.ToString() );
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
                Callbacks();
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamCallbacks.cs", sb.ToString() );
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
                sb = new StringBuilder();
                Header( "Steamworks.Data" );
                StructFunctions();
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamStructFunctions.cs", sb.ToString() );
            }

			{
			//	GenerateGlobalFunctions( "SteamAPI", $"{folder}../Generated/SteamAPI.cs" );
			//	GenerateGlobalFunctions( "SteamGameServer", $"{folder}../Generated/SteamGameServer.cs" );
			//	GenerateGlobalFunctions( "SteamInternal", $"{folder}../Generated/SteamInternal.cs" );
			}

            foreach ( var iface in def.Interfaces )
            {
                GenerateInterface( iface, $"{folder}/Interfaces/" );
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

        public bool IsStruct( string name )
        {
            if ( def.structs.Any( x => x.Name == name || Cleanup.ConvertType( x.Name ) == name ) )
                return true;

            return false;
        }

        public bool IsTypeDef( string name )
        {
            if ( def.typedefs.Any( x => x.Name == name || Cleanup.ConvertType( x.Name ) == name ) )
                return true;

            return false;
        }

        public bool IsCallback( string name )
        {
            if ( def.callback_structs.Any( x => x.Name == name || Cleanup.ConvertType( x.Name ) == name ) )
                return true;

            return false;
        }

        public bool IsEnum( string name )
        {
            if ( def.enums.Any( x => x.Name == name || Cleanup.ConvertType( x.Name ) == name ) )
                return true;

            return false;
        }
    }
}
