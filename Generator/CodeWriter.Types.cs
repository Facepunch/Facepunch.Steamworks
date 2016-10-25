using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CSharpGenerator
    {
        private void Types()
        {
            foreach ( var o in def.typedefs.Where( x => !x.Name.Contains( "::" ) ) )
            {
                if ( o.Name == "ValvePackingSentinel_t" ) continue;
                if ( o.Name == "SteamAPIWarningMessageHook_t" ) continue;
                if ( o.Name == "Salt_t" ) continue;
                if ( o.Name == "SteamAPI_CheckCallbackRegistered_t" ) continue;
                if ( o.Name == "compile_time_assert_type" ) continue;
                if ( o.Name.StartsWith( "uint" ) ) continue;
                if ( o.Name.StartsWith( "int" ) ) continue;
                if ( o.Name.StartsWith( "ulint" ) ) continue;
                if ( o.Name.StartsWith( "lint" ) ) continue;
                if ( o.Name.StartsWith( "PFN" ) ) continue;

                StartBlock( $"public struct {o.Name}" );
                WriteLine( $"public {ToManagedType( o.Type )} Value;" );
                WriteLine();
                StartBlock( $"public static implicit operator {o.Name}( {ToManagedType( o.Type )} value )" );
                WriteLine( $"return new {o.Name}(){{ Value = value }};" );
                EndBlock();
                StartBlock( $"public static implicit operator {ToManagedType( o.Type )}( {o.Name} value )" );
                WriteLine( $"return value.Value;" );
                EndBlock();
                EndBlock();
                WriteLine();
            }
        }
    }
}
