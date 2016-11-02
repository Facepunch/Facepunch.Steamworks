
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CodeWriter
    {
        //
        // Don't give a fuck about these types
        //
        public readonly static string[] SkipTypes = new string[]
        {
            "ValvePackingSentinel_t",
            "SteamAPIWarningMessageHook_t",
            "Salt_t",
            "SteamAPI_CheckCallbackRegistered_t",
            "compile_time_assert_type"
        };

        //
        // Native types and function defs
        //
        public readonly static string[] SkipTypesStartingWith = new string[]
        {
            "uint",
            "int",
            "ulint",
            "lint",
            "PFN"
        };

        private void Types()
        {
            foreach ( var o in def.typedefs.Where( x => !x.Name.Contains( "::" ) ) )
            {
                if ( SkipTypes.Contains( o.Name ) )
                    continue;

                if ( SkipTypesStartingWith.Any( x => o.Name.StartsWith( x ) ) )
                    continue;

                StartBlock( $"internal struct {o.Name}" );
                {
                    WriteLine( $"public {ToManagedType( o.Type )} Value;" );
                    WriteLine();
                    StartBlock( $"public static implicit operator {o.Name}( {ToManagedType( o.Type )} value )" );
                    {
                        WriteLine( $"return new {o.Name}(){{ Value = value }};" );
                    }
                    EndBlock();
                    WriteLine();
                    StartBlock( $"public static implicit operator {ToManagedType( o.Type )}( {o.Name} value )" );
                    {
                        WriteLine( $"return value.Value;" );
                    }
                    EndBlock();
                }
                EndBlock();
                WriteLine();
            }
        }
    }
}
