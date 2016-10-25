using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CSharpGenerator
    {
        void PlatformInterface()
        {
            StartBlock( $"internal static partial class Platform" );
            StartBlock( $"public interface Interface" );

            WriteLine( "bool IsValid { get; } " );
            WriteLine();

            foreach ( var c in def.methods.GroupBy( x => x.ClassName ) )
            {
                PlatformInterface( c.Key, c.ToArray() );
            }

            EndBlock();
            EndBlock();
        }

        private void PlatformClass( string type, string libraryName )
        {
            StartBlock( $"internal static partial class Platform" );
            StartBlock( $"public class {type} : Interface" );

            WriteLine( "internal IntPtr _ptr;" );
            WriteLine( "public bool IsValid { get{ return _ptr != null; } }" );

            WriteLine();

            //
            // Constructor
            //
            StartBlock( $"public {type}( IntPtr pointer )" );
                WriteLine( "_ptr = pointer;" );
            EndBlock();

            foreach ( var c in def.methods.GroupBy( x => x.ClassName ) )
            {
                PlatformClass( c.Key, c.ToArray() );
            }

            StartBlock( $"internal static unsafe class Native" );
            foreach ( var c in def.methods.GroupBy( x => x.ClassName ) )
            {
                InteropClass( libraryName, c.Key, c.ToArray() );
            }
            EndBlock();

            EndBlock();
            EndBlock();
        }

        private void PlatformClass( string className, SteamApiDefinition.MethodDef[] methodDef )
        {
            if ( className == "ISteamMatchmakingPingResponse" ) return;
            if ( className == "ISteamMatchmakingServerListResponse" ) return;
            if ( className == "ISteamMatchmakingPlayersResponse" ) return;
            if ( className == "ISteamMatchmakingRulesResponse" ) return;
            if ( className == "ISteamMatchmakingPingResponse" ) return;

            LastMethodName = "";
            foreach ( var m in methodDef )
            {
                PlatformClassMethod( className, m );
            }

            WriteLine();
        }

        private void PlatformInterface( string className, SteamApiDefinition.MethodDef[] methodDef )
        {
            if ( className == "ISteamMatchmakingPingResponse" ) return;
            if ( className == "ISteamMatchmakingServerListResponse" ) return;
            if ( className == "ISteamMatchmakingPlayersResponse" ) return;
            if ( className == "ISteamMatchmakingRulesResponse" ) return;
            if ( className == "ISteamMatchmakingPingResponse" ) return;

            LastMethodName = "";
            foreach ( var m in methodDef )
            {
                PlatformInterfaceMethod( className, m );
            }

            WriteLine();
        }

        private void PlatformInterfaceMethod( string classname, SteamApiDefinition.MethodDef methodDef )
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

            if ( classname == "SteamApi" )
                flatName = methodName;

            var argstring = string.Join( ", ", arguments.Select( x => x.InteropParameter() ) );
            if ( argstring != "" ) argstring = $" {argstring} ";

            WriteLine( $"{ret.Return()} {classname}_{methodName}({argstring});" );
            LastMethodName = methodDef.Name;
        }

        private void PlatformClassMethod( string classname, SteamApiDefinition.MethodDef methodDef )
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

            if ( classname == "SteamApi" )
                flatName = methodName;


            var argstring = string.Join( ", ", arguments.Select( x => x.InteropParameter() ) );
            if ( argstring != "" ) argstring = $" {argstring} ";

            StartBlock( $"public virtual {ret.Return()} {classname}_{methodName}({argstring})" );

            if ( methodDef.NeedsSelfPointer )
            {
                WriteLine( $"if ( _ptr == null ) throw new System.Exception( \"{classname} _ptr is null!\" );" );
                WriteLine();
            }

                var retcode = "";
            if ( ret.NativeType != "void" )
                retcode = "return ";

            argstring = string.Join( ", ", arguments.Select( x => x.InteropVariable() ) );

            if ( methodDef.NeedsSelfPointer )
                argstring = "_ptr" + ( argstring.Length > 0 ? ", " : "" ) + argstring;

            WriteLine( $"{retcode}Native.{classname}.{methodName}({argstring});" );

            EndBlock();

            LastMethodName = methodDef.Name;
        }

        private void InteropClassMethod( string library, string classname, SteamApiDefinition.MethodDef methodDef )
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

            if ( classname == "SteamApi" )
                flatName = methodName;


            var argstring = string.Join( ", ", arguments.Select( x => x.InteropParameter() ) );

            if ( methodDef.NeedsSelfPointer )
            {
                argstring = "IntPtr " + classname + ( argstring.Length > 0 ? ", " : "" ) + argstring;
            }

            if ( argstring != "" ) argstring = $" {argstring} ";

            WriteLine( $"[DllImportAttribute( \"{library}\", EntryPoint = \"{flatName}\" )] internal static extern {ret.Return()} {methodName}({argstring});" );
            LastMethodName = methodDef.Name;
        }

        private void InteropClass( string libraryName, string className, SteamApiDefinition.MethodDef[] methodDef )
        {
            if ( className == "ISteamMatchmakingPingResponse" ) return;
            if ( className == "ISteamMatchmakingServerListResponse" ) return;
            if ( className == "ISteamMatchmakingPlayersResponse" ) return;
            if ( className == "ISteamMatchmakingRulesResponse" ) return;
            if ( className == "ISteamMatchmakingPingResponse" ) return;

            StartBlock( $"internal static unsafe class {className}" );

            LastMethodName = "";
            foreach ( var m in methodDef )
            {
                InteropClassMethod( libraryName, className, m );
            }

            EndBlock();

            WriteLine();
        }


    }
}
