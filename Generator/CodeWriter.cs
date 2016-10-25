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
        private StringBuilder sb = new StringBuilder();
        private SteamApiDefinition def;

        private Dictionary<string, TypeDef> TypeDefs = new Dictionary<string, TypeDef>();

        public CodeWriter( SteamApiDefinition def )
        {
            this.def = def;

            def.methods.Add( new SteamApiDefinition.MethodDef()
            {
                ClassName = "SteamApi",
                Name = "SteamAPI_Init",
                ReturnType = "void",
                NeedsSelfPointer = false
            } );

            def.methods.Add( new SteamApiDefinition.MethodDef()
            {
                ClassName = "SteamApi",
                Name = "SteamAPI_RunCallbacks",
                ReturnType = "void",
                NeedsSelfPointer = false
            } );

            def.methods.Add( new SteamApiDefinition.MethodDef()
            {
                ClassName = "SteamApi",
                Name = "SteamGameServer_RunCallbacks",
                ReturnType = "void",
                NeedsSelfPointer = false
            } );

            def.methods.Add( new SteamApiDefinition.MethodDef()
            {
                ClassName = "SteamApi",
                Name = "SteamAPI_RegisterCallback",
                ReturnType = "void",
                NeedsSelfPointer = false,
                Params = new SteamApiDefinition.MethodDef.ParamType[]
                {
                    new SteamApiDefinition.MethodDef.ParamType()
                    {
                        Name = "pCallback",
                        Type = "void *"
                    },
                    new SteamApiDefinition.MethodDef.ParamType()
                    {
                        Name = "callback",
                        Type = "int"
                    },
                }
            } );

            def.methods.Add( new SteamApiDefinition.MethodDef()
            {
                ClassName = "SteamApi",
                Name = "SteamAPI_UnregisterCallback",
                ReturnType = "void",
                NeedsSelfPointer = false,
                Params = new SteamApiDefinition.MethodDef.ParamType[]
                {
                    new SteamApiDefinition.MethodDef.ParamType()
                    {
                        Name = "pCallback",
                        Type = "void *"
                    }
                }
            } );

            def.methods.Add( new SteamApiDefinition.MethodDef()
            {
                ClassName = "SteamApi",
                Name = "SteamInternal_GameServer_Init",
                ReturnType = "bool",
                NeedsSelfPointer = false,
                Params = new SteamApiDefinition.MethodDef.ParamType[]
                {
                    new SteamApiDefinition.MethodDef.ParamType()
                    {
                        Name = "unIP",
                        Type = "uint32"
                    },
                    new SteamApiDefinition.MethodDef.ParamType()
                    {
                        Name = "usPort",
                        Type = "uint16"
                    },
                    new SteamApiDefinition.MethodDef.ParamType()
                    {
                        Name = "usGamePort",
                        Type = "uint16"
                    },
                    new SteamApiDefinition.MethodDef.ParamType()
                    {
                        Name = "usQueryPort",
                        Type = "uint16"
                    },
                    new SteamApiDefinition.MethodDef.ParamType()
                    {
                        Name = "eServerMode",
                        Type = "int"
                    },
                    new SteamApiDefinition.MethodDef.ParamType()
                    {
                        Name = "pchVersionString",
                        Type = "const char *"
                    }
                },
            } );

            def.methods.Add( new SteamApiDefinition.MethodDef()
            {
                ClassName = "SteamApi",
                Name = "SteamAPI_Shutdown",
                ReturnType = "void",
                NeedsSelfPointer = false
            } );




            def.methods.Add( new SteamApiDefinition.MethodDef()
            {
                ClassName = "SteamApi",
                Name = "SteamAPI_GetHSteamUser",
                ReturnType = "HSteamUser",
                NeedsSelfPointer = false
            } );

            def.methods.Add( new SteamApiDefinition.MethodDef()
            {
                ClassName = "SteamApi",
                Name = "SteamAPI_GetHSteamPipe",
                ReturnType = "HSteamPipe",
                NeedsSelfPointer = false
            } );

            def.methods.Add( new SteamApiDefinition.MethodDef()
            {
                ClassName = "SteamApi",
                Name = "SteamGameServer_GetHSteamUser",
                ReturnType = "HSteamUser",
                NeedsSelfPointer = false
            } );

            def.methods.Add( new SteamApiDefinition.MethodDef()
            {
                ClassName = "SteamApi",
                Name = "SteamGameServer_GetHSteamPipe",
                ReturnType = "HSteamPipe",
                NeedsSelfPointer = false
            } );

            def.methods.Add( new SteamApiDefinition.MethodDef()
            {
                ClassName = "SteamApi",
                Name = "SteamInternal_CreateInterface",
                ReturnType = "void *",
                Params = new SteamApiDefinition.MethodDef.ParamType[]
                {
                    new SteamApiDefinition.MethodDef.ParamType()
                    {
                        Name = "version",
                        Type = "const char *"
                    }
                },
                NeedsSelfPointer = false
            } );

            

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
                PlatformInterface();
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamNative.Platform.Interface.cs", sb.ToString() );
            }

            {
                sb = new StringBuilder();
                Header();
                PlatformClass( "Win32", "steam_api.dll", true );
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamNative.Platform.Win32.cs", sb.ToString() );
            }

            {
                sb = new StringBuilder();
                Header();
                PlatformClass( "Win64", "steam_api64.dll", true );
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamNative.Platform.Win64.cs", sb.ToString() );
            }

            {
                sb = new StringBuilder();
                Header();
                PlatformClass( "Linux32", "libsteam_api.so", false );
                Footer();
                System.IO.File.WriteAllText( $"{folder}SteamNative.Platform.Linux32.cs", sb.ToString() );
            }

            {
                sb = new StringBuilder();
                Header();
                PlatformClass( "Linux64", "libsteam_api64.so", false );
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
                Classes( $"{folder}SteamNative." );
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

        private string ToManagedType( string type )
        {
            type = type.Replace( "ISteamHTMLSurface::", "" );
            type = type.Replace( "class ", "" );
            type = type.Replace( "struct ", "" );
            type = type.Replace( "const void", "void" );
            type = type.Replace( "union ", "" );
            type = type.Replace( "enum ", "" );

            switch ( type )
            {
                case "uint64": return "ulong";
                case "uint32": return "uint";
                case "int32": return "int";
                case "int64": return "long";
                case "void *": return "IntPtr";
                case "uint8 *": return "IntPtr";
                case "int16": return "short";
                case "uint8": return "byte";
                case "int8": return "char";
                case "unsigned short": return "ushort";
                case "unsigned int": return "uint";
                case "uint16": return "ushort";
                case "const char *": return "string";
                case "_Bool": return "bool";
                case "CSteamID": return "ulong";

                case "SteamAPIWarningMessageHook_t": return "IntPtr";
            }

            //type = type.Trim( '*', ' ' );

            // Enums - skip the 'E'
            if ( type[0] == 'E' )
            {
                return type.Substring( 1 );
            }

            if ( type.StartsWith( "ISteamMatchmak" ) && type.Contains( "Response" )  )
                return "IntPtr";

            return type;
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

        

        private int indent = 0;
        public string Indent { get { return new string( '\t', indent ); } }

        private void EndBlock( string end = "" )
        {
            indent--;
            sb.AppendLine( $"{Indent}}}{end}" );
        }


        private void WriteLine( string v = "" )
        {
            sb.AppendLine( $"{Indent}{v}" );
        }

        private void StartBlock( string v )
        {
            sb.AppendLine( $"{Indent}{v}" );
            sb.AppendLine( $"{Indent}{{" );

            indent++;
        }

        private void WriteLines( List<string> beforeLines )
        {
            foreach ( var line in beforeLines )
            {
                if ( line == "}" )
                    indent--;

                WriteLine( line );

                if ( line == "{" )
                    indent++;
            }
        }


        private void Footer()
        {
            EndBlock();
        }

        private void Header()
        {
            WriteLine( "using System;" );
            WriteLine( "using System.Runtime.InteropServices;" );
            WriteLine();
            StartBlock( "namespace SteamNative" );
        }
    }
}

class TypeDef
{
    public string Name;
    public string NativeType;
    public string ManagedType;
}

