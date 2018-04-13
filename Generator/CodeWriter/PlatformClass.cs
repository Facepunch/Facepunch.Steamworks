using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CodeWriter
    {
        bool LargePack;
        bool X86;

        private void PlatformClass( string type, string libraryName, bool LargePack )
        {
            this.LargePack = LargePack;
            X86 = type.EndsWith( "32" );

            StartBlock( $"internal static partial class Platform" );
            {
                StartBlock( $"internal class {type} : Interface" );
                {
                    WriteLine( "internal IntPtr _ptr;" );
                    WriteLine( "public bool IsValid { get{ return _ptr != IntPtr.Zero; } }" );
                    WriteLine();

                    WriteLine( "//" );
                    WriteLine( "// Constructor sets pointer to native class" );
                    WriteLine( "//" );
                    StartBlock( $"internal {type}( IntPtr pointer )" );
                    {
                        WriteLine( "_ptr = pointer;" );
                    }
                    EndBlock();

                    WriteLine( "//" );
                    WriteLine( "// When shutting down clear all the internals to avoid accidental use" );
                    WriteLine( "//" );
                    StartBlock( $"public virtual void Dispose()" );
                    {
                        WriteLine( "_ptr = IntPtr.Zero;" );
                    }
                    EndBlock();
                    WriteLine();

                    foreach ( var c in def.methods.GroupBy( x => x.ClassName ) )
                    {
                        PlatformClass( c.Key, c.ToArray() );
                    }

                    StartBlock( $"internal static unsafe class Native" );
                    {
                        foreach ( var c in def.methods.GroupBy( x => x.ClassName ) )
                        {
                            InteropClass( libraryName, c.Key, c.ToArray() );
                        }
                    }
                    EndBlock();
                }
                EndBlock();
            }
            EndBlock();
        }

        private void PlatformClass( string className, SteamApiDefinition.MethodDef[] methodDef )
        {
            if ( ShouldIgnoreClass( className ) ) return;

            LastMethodName = "";
            foreach ( var m in methodDef )
            {
                PlatformClassMethod( className, m );
            }

            WriteLine();
        }

        private void PlatformClassMethod( string classname, SteamApiDefinition.MethodDef methodDef )
        {
            var arguments = BuildArguments( methodDef.Params );

            var ret = new Argument( "return", methodDef.ReturnType, TypeDefs );

            var methodName = methodDef.Name;

            if ( LastMethodName == methodName )
                methodName = methodName + "0";

            var flatName = $"SteamAPI_{classname}_{methodName}";

            if ( classname == "SteamApi" )
                flatName = methodName;

            var argstring = string.Join( ", ", arguments.Select( x => x.InteropParameter( true, true ) ) );
            if ( argstring != "" ) argstring = $" {argstring} ";

            StartBlock( $"public virtual {ret.Return()} {classname}_{methodName}({argstring})" );
            {

                //  var vars = string.Join( " + \",\" + ", arguments.Where( x => !x.InteropParameter( true, true ).StartsWith( "out " ) ).Select( x => x.Name ) );
                //   if ( vars != "" ) vars = "\" + " + vars + " + \"";
                //   WriteLine( $"Console.WriteLine( \"{classname}_{methodName}( {vars} )\" );" );

                if ( methodDef.NeedsSelfPointer )
                {
                    WriteLine( $"if ( _ptr == IntPtr.Zero ) throw new System.Exception( \"{classname} _ptr is null!\" );" );
                    WriteLine();
                }

                var retcode = "";
                if ( ret.NativeType != "void" )
                    retcode = "return ";

                AfterLines = new List<string>();

                foreach ( var a in arguments )
                {
                    if ( a.InteropParameter( LargePack ).Contains( ".PackSmall" ) )
                    {
                        WriteLine( $"var {a.Name}_ps = new {a.ManagedType.Trim( '*' )}.PackSmall();" );
                        AfterLines.Add( $"{a.Name} = {a.Name}_ps;" );
                        a.Name = "ref " + a.Name + "_ps";

                        if ( retcode != "" )
                            retcode = "var ret = ";
                    }
                }

                argstring = string.Join( ", ", arguments.Select( x => x.InteropVariable( false ) ) );

                if ( methodDef.NeedsSelfPointer )
                    argstring = "_ptr" + ( argstring.Length > 0 ? ", " : "" ) + argstring;

                WriteLine( $"{retcode}Native.{flatName}({argstring});" );

                WriteLines( AfterLines );

                if ( retcode.StartsWith( "var" ) )
                {
                    WriteLine( "return ret;" );
                }

            }
            EndBlock();

            LastMethodName = methodDef.Name;
        }



        private void InteropClass( string libraryName, string className, SteamApiDefinition.MethodDef[] methodDef )
        {
            if ( ShouldIgnoreClass( className ) ) return;

            WriteLine( $"//" );
            WriteLine( $"// {className} " );
            WriteLine( $"//" );

            LastMethodName = "";
            foreach ( var m in methodDef )
            {
                InteropClassMethod( libraryName, className, m );
            }

            WriteLine();
        }

        private void InteropClassMethod( string library, string classname, SteamApiDefinition.MethodDef methodDef )
        {
            var arguments = BuildArguments( methodDef.Params );
            var ret = new Argument( "return", methodDef.ReturnType, TypeDefs );

            var methodName = methodDef.Name;

            if ( LastMethodName == methodName )
                methodName = methodName + "0";

            var flatName = $"SteamAPI_{classname}_{methodName}";

            if ( classname == "SteamApi" )
                flatName = methodName;

            var argstring = string.Join( ", ", arguments.Select( x => x.InteropParameter( LargePack, true ) ) );

            if ( methodDef.NeedsSelfPointer )
            {
                argstring = "IntPtr " + classname + ( argstring.Length > 0 ? ", " : "" ) + argstring;
            }

            if ( argstring != "" ) argstring = $" {argstring} ";

            if ( X86 )
                Write( $"[DllImport( \"{library}\", CallingConvention = CallingConvention.Cdecl )] " );
            else
                Write( $"[DllImport( \"{library}\" )] " );

            if ( ret.Return() == "bool" ) WriteLine( "[return: MarshalAs(UnmanagedType.U1)]" );

            WriteLine( $"internal static extern {ret.Return()} {flatName}({argstring});" );
            LastMethodName = methodDef.Name;
        }


    }
}
