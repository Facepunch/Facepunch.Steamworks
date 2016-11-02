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
            StartBlock( "internal static class CallbackIdentifiers" );
            foreach ( var o in def.CallbackIds )
            {
                WriteLine( $"public const int {o.Key} = {o.Value};" );
            }
            EndBlock();

            StartBlock( "internal static class Defines" );
            foreach ( var o in def.Defines )
            {
                WriteLine( $"internal const string {o.Key} = \"{o.Value}\";" );
            }
            EndBlock();
        }
    }
}