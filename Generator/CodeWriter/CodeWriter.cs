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
                System.IO.File.WriteAllText( $"{folder}SteamNative.Enums.cs", sb.ToString() );
            }

            {
                sb = new StringBuilder();
                Header();
                Types();
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamNative.Types.cs", sb.ToString() );
            }

            {
                sb = new StringBuilder();
                Header();
                Structs();
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamNative.Structs.cs", sb.ToString() );
            }

            {
                sb = new StringBuilder();
                Header();
                Constants();
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamNative.Constants.cs", sb.ToString() );
            }

            {
                sb = new StringBuilder();
                Header();
                PlatformInterface();
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamNative.Platform.Interface.cs", sb.ToString() );
            }

            {
                sb = new StringBuilder();
                Header();
                PlatformClass( "Windows", "steam_api64.dll", true );
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamNative.Platform.Win64.cs", sb.ToString() );
            }

            {
                sb = new StringBuilder();
                Header();
                PlatformClass( "Linux", "libsteam_api.so", false );
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamNative.Platform.Linux64.cs", sb.ToString() );
            }

            {
                sb = new StringBuilder();
                Header();
                PlatformClass( "Mac", "libsteam_api.dylib", false );
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamNative.Platform.Mac.cs", sb.ToString() );
            }

            {
                GenerateClasses( $"{folder}SteamNative." );
            }

			{
				GenerateVTableClass( "ISteamApps", $"{folder}../Generated/Interfaces/ISteamApps.cs" );
				GenerateVTableClass( "ISteamUtils", $"{folder}../Generated/Interfaces/ISteamUtils.cs" );
				GenerateVTableClass( "ISteamParentalSettings", $"{folder}../Generated/Interfaces/ISteamParentalSettings.cs" );
				GenerateVTableClass( "ISteamMusic", $"{folder}../Generated/Interfaces/ISteamMusic.cs" );
				GenerateVTableClass( "ISteamVideo", $"{folder}../Generated/Interfaces/ISteamVideo.cs" );
				GenerateVTableClass( "ISteamUser", $"{folder}../Generated/Interfaces/ISteamUser.cs" );
			}
		}

        void WorkoutTypes()
        {
            def.typedefs.Add( new SteamApiDefinition.TypeDef() { Name = "CGameID", Type = "ulong" } );
            def.typedefs.Add( new SteamApiDefinition.TypeDef() { Name = "CSteamID", Type = "ulong" } );

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

        private List<Argument> BuildArguments( SteamApiDefinition.MethodDef.ParamType[] ps )
        {
            var args = new List<Argument>();
            if ( ps == null ) return args;

            foreach ( var p in ps )
            {
                var a = new Argument( p.Name, p.Type, TypeDefs );
                args.Add( a );
            }

            return args;
        }

        private void Header( string NamespaceName = "SteamNative" )
        {
            WriteLine( "using System;" );
            WriteLine( "using System.Runtime.InteropServices;" );
            WriteLine( "using System.Linq;" );
            WriteLine();
            StartBlock( "namespace " + NamespaceName );
        }

        private void Footer()
        {
            EndBlock();
        }
    }
}
