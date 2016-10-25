using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CSharpGenerator
    {
        private void Enums()
        {
            //StartBlock( "namespace Enum" );

            foreach ( var o in def.enums )
            {

                WriteLine( $"//" );
                WriteLine( $"// {o.Name}" );
                WriteLine( $"//" );
                var name = o.Name;

                if ( name.Contains( "::" ) )
                    name = o.Name.Substring( o.Name.LastIndexOf( ":" ) + 1 );

                // Skip the E
                if ( name[0] == 'E' )
                    name = name.Substring( 1 );

                StartBlock( $"public enum {name} : int" );

                int iFinished = int.MaxValue;
                for ( int i = 0; i < 4096; i++ )
                {
                    var c = o.Values.First().Name[i];
                    foreach ( var entry in o.Values )
                    {
                        if ( entry.Name[i] != c )
                        {
                            iFinished = i;
                            break;
                        }
                    }

                    if ( iFinished != int.MaxValue )
                        break;
                }

                foreach ( var entry in o.Values )
                {
                    var ename = entry.Name;

                    if ( iFinished != int.MaxValue )
                        ename = ename.Substring( iFinished );

                    if ( char.IsNumber( ename[0] ) )
                        ename = name + ename;

                    // while ( char.IsNumber( ename [0] ) )
                    // {
                    //     ename = ename.Substring( 1 );
                    // }

                    WriteLine( $"{ename} = {entry.Value}," );
                }

                EndBlock();
                WriteLine();
            }

            // EndBlock();
        }
    }
}
