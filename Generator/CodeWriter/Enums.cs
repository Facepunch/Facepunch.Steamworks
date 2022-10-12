using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CodeWriter
    {
        private void Enums()
        {
            foreach ( var o in def.enums )
            {
                WriteLine( $"//" );
                WriteLine( $"// {o.Name}" );
                WriteLine( $"//" );
                var name = o.Name;

                // We're not interested in namespacing
                if ( name.Contains( "::" ) )
                    name = o.Name.Substring( o.Name.LastIndexOf( ":" ) + 1 );

                // Skip the E
                if ( name[0] == 'E' )
                    name = name.Substring( 1 );

                name = Cleanup.ConvertType( name );

                if ( !Cleanup.ShouldCreate( name ) )
                    continue;

                var lowest = o.Values.Min( x => long.Parse( x.Value ) );
                var highest = o.Values.Max( x => long.Parse( x.Value ) );

                var t = "int";

                if ( highest > int.MaxValue )
                    t = "uint";

                WriteEnum( o, name, t );
            }
        }

        private void WriteEnum( SteamApiDefinition.EnumDef o, string name, string t = "int" )
        {
            StartBlock( $"{Cleanup.Expose( name )} enum {name} : {t}" );
            {
                //
                // If all the enum values start with the same 
                // string, remove it. This converts
                // "k_EUserHasLicenseResultHasLicense" to "HasLicense" etc
                //
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

                    if ( ename.Contains( "Force32Bit" ) )
                        continue;

                    if ( iFinished != int.MaxValue )
                        ename = ename.Substring( iFinished );

                    //
                    // Names aren't allowed to start with a number
                    // So just stick the enum name on the front
                    //
                    if ( char.IsNumber( ename[0] ) )
                    {
                        var p = name;

                        if ( p == "HTTPStatusCode" ) p = "Code";
                        if ( p == "SteamIPType" ) p = "Type";

                        ename = p + ename;
                    }

                    if (ename.StartsWith( $"k_E{name}" ))
	                    ename = ename[(name.Length + 3)..];

                    WriteLine( $"{ename.Trim( ' ', '_' )} = {entry.Value}," );
                }
            }
            EndBlock();
            WriteLine();
        }
    }
}
