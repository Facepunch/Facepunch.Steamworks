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
            var content = System.IO.File.ReadAllText( "steam_api.json" );
            var def = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamApiDefinition>( content );

            var parser = new CodeParser( @"D:\Dropbox (Facepunch Studios)\Software\SteamWorks\steamworks_sdk_138a\public\steam" );

            parser.ExtendDefinition( def );

            var generator = new CodeWriter( def );

            generator.ToFolder( "../Facepunch.Steamworks/SteamNative/" );
        }
    }
}


