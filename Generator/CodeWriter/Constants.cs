using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CodeWriter
    {
        private void Constants()
        {
            StartBlock( "internal static class Defines" );
            foreach ( var o in def.Consts )
            {
                var type = o.Type;
                type = Cleanup.ConvertType( type );

                var val = o.Val;

                // Don't need to ull in c#
                if ( val.EndsWith( "ull" ) )
                    val = val.Replace( "ull", "" );

                val = val.Replace( "uint32", "uint" );
                val = val.Replace( "16U", "16" );
                val = val.Replace( "8U", "8" );

                // we're not an actual typedef so can't cast like this
                val = val.Replace( "( SteamItemInstanceID_t ) ~ 0", "~default(ulong)" );

                // This is defined as 0xffffffff - which is too big for an int
                // It seems like the loop around is required, so we just hard code it
                if ( o.Name == "HSERVERQUERY_INVALID" && val == "0xffffffff" )
                    val = "-1";

                WriteLine( $"internal static readonly {type} {o.Name} = {val};" );
            }
            EndBlock();
        }
    }
}