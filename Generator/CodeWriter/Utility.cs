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

        string CleanMemberName( string m )
        {
            if ( m == "m_pubParam" ) return "ParamPtr";
            if ( m == "m_cubParam" ) return "ParamCount";
            if ( m == "m_itemId" ) return "ItemId";

            var cleanName = m.Replace( "m_un", "" )
                    .Replace( "m_us", "" )
                    .Replace( "m_sz", "" )
                    .Replace( "m_h", "" )
                    .Replace( "m_pp", "" )
                    .Replace( "m_e", "" )
                    .Replace( "m_un", "" )
                    .Replace( "m_ul", "" )
                    .Replace( "m_fl", "" )
                    .Replace( "m_u", "" )
                    .Replace( "m_b", "" )
                    .Replace( "m_i", "" )
                    .Replace( "m_pub", "" )
                    .Replace( "m_cub", "" )
                    .Replace( "m_n", "" )
                    .Replace( "m_rgch", "" )
                    .Replace( "m_r", "" )
                    .Replace( "m_", "" );

            return cleanName.Substring( 0, 1 ).ToUpper() + cleanName.Substring( 1 );
        }
    }
}
