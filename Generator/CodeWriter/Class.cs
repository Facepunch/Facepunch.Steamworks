using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CodeWriter
    {
        string LastMethodName;
        List<string> BeforeLines;
        List<string> AfterLines;
        string ReturnType;
        string ReturnVar;
        SteamApiDefinition.MethodDef MethodDef;
        string ClassName;

        //
        // Output a class into a file
        //
        void Class( string FileName )
        {
            foreach  ( var g in def.methods.GroupBy( x => x.ClassName ) )
            {
                if ( ShouldIgnoreClass( g.Key ) ) continue;

                sb = new StringBuilder();
                Header();
                Class( g.Key, g.OrderBy( x => x.Name ).ToArray() );
                Footer();
                System.IO.File.WriteAllText( $"{FileName}{InterfaceNameToClass(g.Key)}.cs", sb.ToString() );
            }
        }

        private void Class( string classname, SteamApiDefinition.MethodDef[] methodDef )
        {
            StartBlock( $"public unsafe class {InterfaceNameToClass(classname)} : IDisposable" );
            {
                WriteLine( "//" );
                WriteLine( "// Holds a platform specific implentation" );
                WriteLine( "//" );
                WriteLine( "internal Platform.Interface _pi;" );
                WriteLine();

                WriteLine( "//" );
                WriteLine( "// Constructor decides which implementation to use based on current platform" );
                WriteLine( "//" );
                StartBlock( $"public {InterfaceNameToClass( classname )}( IntPtr pointer )" );
                {
                    WriteLine( "if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );" );
                    WriteLine( "else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );" );
                    WriteLine( "else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );" );
                    WriteLine( "else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );" );
                    WriteLine( "else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );" );
                }
                EndBlock();
                WriteLine();

                WriteLine( "//" );
                WriteLine( "// Class is invalid if we don't have a valid implementation" );
                WriteLine( "//" );
                WriteLine( "public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }" );
                WriteLine();

                WriteLine( "//" );
                WriteLine( "// When shutting down clear all the internals to avoid accidental use" );
                WriteLine( "//" );
                StartBlock( $"public virtual void Dispose()" );
                {
                    StartBlock( " if ( _pi != null )" );
                    {
                        WriteLine( "_pi.Dispose();" );
                        WriteLine( "_pi = null;" );
                    }
                    EndBlock();
                }
                EndBlock();
                WriteLine();

                //
                // Methods
                //
                foreach ( var m in methodDef )
                {
                    ClassMethod( classname, m );
                }
            }
            EndBlock();
        }



        private void ClassMethod( string classname, SteamApiDefinition.MethodDef m )
        {
            var argList = BuildArguments( m.Params );
            var callargs = BuildArguments( m.Params );

            BeforeLines = new List<string>();
            AfterLines = new List<string>();
            ReturnType = ToManagedType( m.ReturnType );
            ReturnVar = "";
            MethodDef = m;
            ClassName = classname;

            var statc = classname == null ? " static" : "";

            Detect_VectorReturn( argList, callargs );
            Detect_InterfaceReturn( argList, callargs );
            Detect_StringFetch( argList, callargs );
            Detect_StringReturn( argList, callargs );
            Detect_MatchmakingFilters( argList, callargs );
            Detect_ReturningStruct();
            Detect_IntPtrArgs( argList, callargs );
            Detect_MultiSizeArrayReturn( argList, callargs );

            var methodName = m.Name;

            if ( LastMethodName == methodName )
                methodName += "0";

            var argString = string.Join( ", ", argList.Select( x => x.ManagedParameter() ) );
            if ( argString != "" ) argString = " " + argString + " ";
            StartBlock( $"public{statc} {ReturnType} {methodName}({argString})" );

            CallPlatformClass( classname, m, callargs.Select( x => x.InteropVariable( true ) ).ToList(), ReturnVar );

            WriteLines( BeforeLines );

            WriteLines( AfterLines );

            EndBlock();
            WriteLine();

            LastMethodName = m.Name;
        }

        private void Detect_MultiSizeArrayReturn( List<Argument> argList, List<Argument> callargs )
        {
            if ( ReturnType != "bool" ) return;

            var argPtr = argList.FirstOrDefault( x => x.Name == "pItemDefIDs" );
            var argNum = argList.FirstOrDefault( x => x.Name == "punItemDefIDsArraySize" );

            if ( argPtr == null )
            {
                argPtr = argList.FirstOrDefault( x => x.Name == "pOutItemsArray" );
                argNum = argList.FirstOrDefault( x => x.Name == "punOutItemsArraySize" );
            }

            if ( argPtr == null || argNum == null )
                return;

            WriteLine( "// using: Detect_MultiSizeArrayReturn" );

            var typeName = argPtr.ManagedType.Trim( '*', ' ' );

            BeforeLines.Add( $"{argNum.ManagedType.Trim( '*', ' ' )} {argNum.Name} = 0;" );
            BeforeLines.Add( $"" );
            BeforeLines.Add( $"bool success = false;" );

            ReturnType = argPtr.ManagedType.Trim( '*', ' ' ) + "[]";
            ReturnVar = "success";

            CallPlatformClass( ClassName, MethodDef, callargs.Select( x => x.Name.Replace( "(IntPtr) ", "" )  == argPtr.Name ? "IntPtr.Zero" : x.InteropVariable( true ) ).ToArray().ToList(), ReturnVar );
            BeforeLines.Add( $"if ( !success || {argNum.Name} == 0) return null;" );
            BeforeLines.Add( "" );

            BeforeLines.Add( $"var {argPtr.Name} = new {typeName}[{argNum.Name}];" );
            BeforeLines.Add( $"fixed ( void* {argPtr.Name}_ptr = {argPtr.Name} )" );
            BeforeLines.Add( $"{{" );

            foreach ( var arg in callargs.Where( x => x.Name.Replace( "(IntPtr) ", "" ) == argPtr.Name ) )
            {
                arg.Name += "_ptr";
            }

            AfterLines.Add( $"if ( !success ) return null;" );
            AfterLines.Add( $"return {argPtr.Name};" );
            AfterLines.Add( $"}}" );

            argList.Remove( argPtr );
            argList.Remove( argNum );

        }

        private void Detect_IntPtrArgs( List<Argument> argList, List<Argument> callargs )
        {
            foreach ( var a in callargs )
            {
                if ( !a.InteropParameter( false ).StartsWith( "IntPtr" ) )
                {
                    continue;
                }

                a.Name = "(IntPtr) " + a.Name;
            }
        }

        private void Detect_ReturningStruct()
        {
            if ( !MethodDef.ReturnType.EndsWith( "*" ) ) return;
            if ( !MethodDef.ReturnType.Contains( "_t" ) ) return;

            WriteLine( "// with: Detect_ReturningStruct" );

            ReturnType = ReturnType.Trim( '*',  ' ' );
            ReturnVar = "struct_pointer";

            BeforeLines.Add( "IntPtr struct_pointer;" );

            AfterLines.Add( $"if ( struct_pointer == IntPtr.Zero ) return default({ReturnType});" );
            AfterLines.Add( $"return {ReturnType}.FromPointer( struct_pointer );" );

        }

        private void Detect_MatchmakingFilters( List<Argument> argList, List<Argument> callargs )
        {
            if ( !argList.Any( x => x.Name == "ppchFilters" ) ) return;
            if ( !argList.Any( x => x.Name == "nFilters" ) ) return;

            var filters = argList.Single( x => x.Name == "ppchFilters" );
            filters.ManagedType = "IntPtr";

            WriteLine( "// with: Detect_MatchmakingFilters" );

        }

        private void Detect_StringReturn( List<Argument> argList, List<Argument> callargs )
        {
            if ( ReturnType != "string" ) return;
            if ( MethodDef.ReturnType != "const char *" ) return;
            WriteLine( "// with: Detect_StringReturn" );

            BeforeLines.Add( "IntPtr string_pointer;" );
            ReturnVar = "string_pointer";

            AfterLines.Add( "return Marshal.PtrToStringAnsi( string_pointer );" );
        }

        private void Detect_StringFetch( List<Argument> argList, List<Argument> callargs )
        {
            bool ReturnString = argList.Count < 4 && (ReturnType == "bool" || ReturnType == "void" || ReturnType == "int"|| ReturnType == "uint");
            bool IsFirst = true;

            //System.Text.StringBuilder pStrBuffer1 = new System.Text.StringBuilder(2048);
            //bool result = NativeEntrypoints.SteamAPI_ISteamUGC_GetQueryUGCMetadata(m_pSteamUGC,handle,index,pStrBuffer1,2048);
            //pchMetadata = pStrBuffer1.ToString();

            for ( int i=0; i< argList.Count; i++ )
            {
                if ( argList[i].ManagedType != "char*" ) continue;
                if ( i == argList.Count ) continue;

                var chr = argList[i];
                var num = argList[i+1];

                var IntReturn = ReturnType.Contains( "int" );

                if ( num.ManagedType.Trim( '*' ) != "int" && num.ManagedType.Trim( '*' ) != "uint" ) continue;

                argList.Remove( num );

                if ( ReturnString )
                    argList.Remove( chr );

                chr.ManagedType = "out string";

                WriteLine( "// with: Detect_StringFetch " + ReturnString );

                if ( IsFirst )
                    BeforeLines.Add( $"{ReturnType} bSuccess = default( {ReturnType} );" );

                if ( !ReturnString )
                    BeforeLines.Add( $"{chr.Name} = string.Empty;" );

                BeforeLines.Add( $"System.Text.StringBuilder {chr.Name}_sb = new System.Text.StringBuilder( 4096 );" );

                if ( ReturnString ) ReturnType = "string";
                ReturnVar = "bSuccess";

                BeforeLines.Add( $"{num.ManagedType.Trim( '*' )} {num.Name} = 4096;" );

                callargs.Where( x => x.Name == chr.Name ).All( x => { x.Name = x.Name + "_sb"; return true; } );
           //     callargs.Where( x => x.Name == num.Name ).All( x => { x.Name = "4096"; return true; } );



                if ( ReturnString ) AfterLines.Insert( 0, $"return {chr.Name}_sb.ToString();" );
                else AfterLines.Insert( 0, $"{chr.Name} = {chr.Name}_sb.ToString();" );


                if ( IntReturn )
                {
                    if ( ReturnString ) AfterLines.Insert( 0, "if ( bSuccess <= 0 ) return null;" );
                    else AfterLines.Insert( 0, "if ( bSuccess <= 0 ) return bSuccess;" );
                }
                else
                {
                    if ( ReturnString ) AfterLines.Insert( 0, "if ( !bSuccess ) return null;" );
                    else AfterLines.Insert( 0, "if ( !bSuccess ) return bSuccess;" );
                }

                IsFirst = false;
            }

            if ( !IsFirst )
            {
                if ( !ReturnString ) AfterLines.Add( "return bSuccess;" );
            }
         
            /*
            

            argList.Clear();
            ReturnType = "string";
            ReturnVar = "bSuccess";




            */
        }

        private void Detect_InterfaceReturn( List<Argument> argList, List<Argument> callargs )
        {
            WriteLine( "// " + ReturnType );
            if ( !ReturnType.StartsWith( "ISteam" )  )
                return;

            BeforeLines.Add( "IntPtr interface_pointer;" );

            ReturnVar = "interface_pointer";
            ReturnType = ReturnType.Substring( 1 ).Trim( '*', ' ' );

            AfterLines.Add( $"return new {ReturnType}( interface_pointer );" );
        }

        private void Detect_VectorReturn( List<Argument> argList, List<Argument> callArgs )
        {
            var vec = argList.FirstOrDefault( x => x.Name.StartsWith( "pvec" ) );
            var max = argList.FirstOrDefault( x => x.Name.StartsWith( "unMax" ) );
            if ( max == null ) max = argList.FirstOrDefault( x => x.Name.StartsWith( "unNum" ) );

            if ( vec == null || max == null )
                return;

            WriteLine( "// with: Detect_VectorReturn" );

            //argList.Remove( vec );
            argList.Remove( max );

            vec.ManagedType = vec.ManagedType.Replace( "*", "[]" );

            BeforeLines.Add( $"var {max.Name} = ({max.ManagedType}) {vec.Name}.Length;" );
            BeforeLines.Add( $"fixed ( {vec.ManagedType.Trim( '[', ']' ) }* {vec.Name}_ptr = {vec.Name}  )" );
            BeforeLines.Add( $"{{" );

            AfterLines.Add( $"}}" );

            foreach ( var arg in callArgs.Where( x => x.Name == vec.Name ) )
            {
                arg.Name += "_ptr";
            }

        }

        private void CallPlatformClass( string classname, SteamApiDefinition.MethodDef m, List<string> argList, string returnVar )
        {
            if ( classname == null )
            {
                classname = "SteamApi";
            }

            var methodName = m.Name;

            if ( LastMethodName == methodName )
                methodName += "0";

            var args = string.Join( ", ", argList );
            if ( args != "" ) args = $" {args} ";

            var r = "";
            if ( ReturnType != "void" )
                r = "return ";

            if ( returnVar != "" )
                r = returnVar + " = ";

            //  StartBlock( "case 1:" );
            BeforeLines.Add( $"{r}_pi.{classname}_{methodName}({args});" );
            // WriteLine( "break;" );
            // EndBlock();

            // EndBlock();
        }
    }
}
