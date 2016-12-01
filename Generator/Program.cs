using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Generator
{
    class Program
    {
        static void Main( string[] args )
        {
            var content = System.IO.File.ReadAllText( "steam_sdk/steam_api.json" );
            var def = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamApiDefinition>( content );

            AddExtras( def );

            var parser = new CodeParser( @"steam_sdk" );

            parser.ExtendDefinition( def );

            var generator = new CodeWriter( def );

            generator.ToFolder( "../Facepunch.Steamworks/SteamNative/" );
        }

        private static void AddExtras( SteamApiDefinition def )
        {
            def.structs.Add( new SteamApiDefinition.StructDef()
            {
                Name = "ItemInstalled_t",
                Fields = new SteamApiDefinition.StructDef.StructFields[]
               {
                    new SteamApiDefinition.StructDef.StructFields()
                    {
                        Name = "m_unAppID",
                        Type = "AppId_t"
                    },
                    new SteamApiDefinition.StructDef.StructFields()
                    {
                        Name = "m_nPublishedFileId",
                        Type = "PublishedFileId_t"
                    }
               }

            } );

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
                Name = "SteamAPI_RegisterCallResult",
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
                        Type = "SteamAPICall_t"
                    },
                }
            } );

            def.methods.Add( new SteamApiDefinition.MethodDef()
            {
                ClassName = "SteamApi",
                Name = "SteamAPI_UnregisterCallResult",
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
                        Type = "SteamAPICall_t"
                    },
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

        }
    }
}


