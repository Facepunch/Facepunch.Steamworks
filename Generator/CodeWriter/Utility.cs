using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CodeWriter
    {
        static string[] IgnoredClasses = new string[]
        {
             "ISteamMatchmakingPingResponse",
             "ISteamMatchmakingServerListResponse",
             "ISteamMatchmakingPlayersResponse",
             "ISteamMatchmakingRulesResponse",
             "ISteamMatchmakingPingResponse",
        };

        public static bool ShouldIgnoreClass( string name )
        {
            return IgnoredClasses.Contains( name );
        }

        public string InterfaceNameToClass( string name )
        {
            if ( name[0] == 'I' )
                name = name.Substring( 1 );

            return name;
        }

    }
}
