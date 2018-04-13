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

            AddMissing( def );

            var parser = new CodeParser( @"steam_sdk" );

            parser.ExtendDefinition( def );

            var generator = new CodeWriter( def );

            generator.ToFolder( "../Facepunch.Steamworks/SteamNative/" );
        }

        private static void AddMissing( SteamApiDefinition output )
        {
            var content = System.IO.File.ReadAllText( "steam_api_missing.json" );
            var missing = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamApiDefinition>( content );

            output.structs.AddRange( missing.structs );
            output.methods.AddRange( missing.methods );

            foreach ( var s in output.structs )
            {
                if ( s.Fields == null ) s.Fields = new SteamApiDefinition.StructDef.StructFields[0];
            }
        }
    }
}


