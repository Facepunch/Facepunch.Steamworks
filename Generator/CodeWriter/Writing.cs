using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generator;

namespace Generator
{
    public partial class CodeWriter
    {
        private StringBuilder sb = new StringBuilder();
       
        private int indent = 0;
        public string Indent { get { return new string( '\t', indent ); } }

        private void EndBlock( string end = "" )
        {
            indent--;
            sb.AppendLine( $"{Indent}}}{end}" );
        }


        private void WriteLine( string v = "" )
        {
            sb.AppendLine( $"{Indent}{v}" );
        }

        private void StartBlock( string v )
        {
            sb.AppendLine( $"{Indent}{v}" );
            sb.AppendLine( $"{Indent}{{" );

            indent++;
        }

        private void WriteLines( List<string> beforeLines )
        {
            foreach ( var line in beforeLines )
            {
                if ( line == "}" )
                    indent--;

                WriteLine( line );

                if ( line == "{" )
                    indent++;
            }
        }

    }
}


