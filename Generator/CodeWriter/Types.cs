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
            StartBlock( "public static class CallbackIdentifiers" );
            foreach ( var o in def.CallbackIds )
            {
                WriteLine( $"public const int {o.Key} = {o.Value};" );
            }
            EndBlock();
        }
    }
}
