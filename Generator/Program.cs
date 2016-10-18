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

            Console.WriteLine( content );

            foreach ( var d in def.typedefs )
            {
                Console.WriteLine( "{0} = {1}", d.Name, d.Type );
            }

            foreach ( var d in def.enums )
            {
                Console.WriteLine( "{0} = {1}", d.Name, d.Values );
            }

            Console.ReadKey();
        }
    }
}


