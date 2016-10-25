using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CSharpGenerator
    {
        private void PlatformClass( string type, string libraryName )
        {
            StartBlock( $"internal static partial class Platform" );
            StartBlock( $"public static class {type}" );
            foreach ( var c in def.methods.GroupBy( x => x.ClassName ) )
            {
                PlatformClass( libraryName, c.Key, c.ToArray() );
            }
            EndBlock();
            EndBlock();
        }

        private void PlatformClass( string libraryName, string key, SteamApiDefinition.MethodDef[] methodDef )
        {
            if ( key == "ISteamMatchmakingPingResponse" ) return;
            if ( key == "ISteamMatchmakingServerListResponse" ) return;
            if ( key == "ISteamMatchmakingPlayersResponse" ) return;
            if ( key == "ISteamMatchmakingRulesResponse" ) return;
            if ( key == "ISteamMatchmakingPingResponse" ) return;

            StartBlock( $"public static unsafe class {key}" );

            LastMethodName = "";
            foreach ( var m in methodDef )
            {
                PlatformClassMethod( libraryName, key, m );
            }

            EndBlock();
            WriteLine();
        }

        private void PlatformClassMethod( string library, string classname, SteamApiDefinition.MethodDef methodDef )
        {
            var arguments = BuildArguments( methodDef.Params );

            var ret = new Argument()
            {
                Name = "return",
                NativeType = methodDef.ReturnType
            };

            ret.Build( null, TypeDefs );

            var methodName = methodDef.Name;

            if ( LastMethodName == methodName )
                methodName = methodName + "0";

            var flatName = $"SteamAPI_{classname}_{methodName}";

            if ( classname == "Global" )
                flatName = methodName;


            var argstring = string.Join( ", ", arguments.Select( x => x.InteropParameter() ) );

            if ( methodDef.NeedsSelfPointer )
            {
                argstring = "IntPtr " + classname + ( argstring.Length > 0 ? ", " : "" ) + argstring;
            }

            WriteLine( $"[DllImportAttribute( \"{library}\", EntryPoint = \"{flatName}\" )] internal static extern {ret.Return()} {methodName}( {argstring} );" );
            LastMethodName = methodDef.Name;
        }
    }
}
