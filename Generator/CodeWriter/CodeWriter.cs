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

        public CodeWriter( SteamApiDefinition def )
        {
            this.def = def;

            def.methods.Add( new SteamApiDefinition.MethodDef()
            {
                ClassName = "SteamApi",
                Name = "SteamAPI_Init",
                ReturnType = "bool",
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
                Name = "SteamGameServer_Shutdown",
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
                GenerateClasses( $"{folder}SteamNative." );
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

        private void Header()
        {
            WriteLine( "using System;" );
            WriteLine( "using System.Runtime.InteropServices;" );
            WriteLine();
            StartBlock( "namespace SteamNative" );
        }

        private void Footer()
        {
            EndBlock();
        }
    }
}
