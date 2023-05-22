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
		public static SteamApiDefinition Definitions;

		static void Main( string[] args )
        {
            var content = System.IO.File.ReadAllText( "steam_sdk/steam_api.json" );
            var def = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamApiDefinition>( content );

			Definitions = def;

			var generator = new CodeWriter( def );

            generator.ToFolder( "../Facepunch.Steamworks/Generated/" );
        }
    }
}


