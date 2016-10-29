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
        // Don't give a fuck about these classes, they just cause us trouble
        //
        public readonly static string[] SkipStructs = new string[]
        {
            "CSteamID",
            "CSteamAPIContext",
            "CCallResult",
            "CCallback",
            "ValvePackingSentinel_t"
        };

        void Structs()
        {
            foreach ( var c in def.structs )
            {
                if ( SkipStructs.Contains( c.Name ) )
                    continue;

                if ( c.Name.Contains( "::" ) )
                    continue;

                int defaultPack = 8;

                if ( c.Fields.Any( x => x.Type.Contains( "class CSteamID" ) ) )
                    defaultPack = 4;

                //
                // Main struct
                //
                WriteLine( $"[StructLayout( LayoutKind.Sequential, Pack = {defaultPack} )]" );
                StartBlock( $"public struct {c.Name}" );
                {
                    //
                    // The fields
                    //
                    StructFields( c.Fields );

                    WriteLine();
                    WriteLine( "//" );
                    WriteLine( "// Read this struct from a pointer, usually from Native" );
                    WriteLine( "//" );
                    StartBlock( $"public static {c.Name} FromPointer( IntPtr p )" );
                    {
                        WriteLine( $"return ({c.Name}) Marshal.PtrToStructure( p, typeof({c.Name}) );" );
                    }
                    EndBlock();

                    if ( defaultPack == 8 )
                        defaultPack = 4;

                    //
                    // Small packed struct (for osx, linux)
                    //
                    WriteLine();
                    WriteLine( $"[StructLayout( LayoutKind.Sequential, Pack = {defaultPack} )]" );
                    StartBlock( $"public struct PackSmall" );
                    {
                        StructFields( c.Fields );

                        //
                        // Implicit convert from PackSmall to regular
                        //
                        WriteLine();
                        WriteLine( "//" );
                        WriteLine( $"// Easily convert from PackSmall to {c.Name}" );
                        WriteLine( "//" );
                        StartBlock( $"public static implicit operator {c.Name} (  {c.Name}.PackSmall d )" );
                        {
                            StartBlock( $"return new {c.Name}()" );
                            {
                                foreach ( var f in c.Fields )
                                {
                                    WriteLine( $"{CleanMemberName( f.Name )} = d.{CleanMemberName( f.Name )}," );
                                }
                            }
                            EndBlock( ";" );
                        }
                        EndBlock();

                        WriteLine();
                        WriteLine( "//" );
                        WriteLine( "// Read this struct from a pointer, usually from Native" );
                        WriteLine( "//" );
                        StartBlock( $"public static PackSmall FromPointer( IntPtr p )" );
                        {
                            WriteLine( $"return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );" );
                        }
                        EndBlock();

                    }
                    EndBlock();

                }
                EndBlock();
                WriteLine();
            }
        }

        private void StructFields( SteamApiDefinition.StructDef.StructFields[] fields )
        {
            foreach ( var m in fields )
            {
                var t = ToManagedType( m.Type );

                if ( TypeDefs.ContainsKey( t ) )
                {
                    t = TypeDefs[t].ManagedType;
                }

                if ( t == "bool" )
                {
                    WriteLine( "[MarshalAs(UnmanagedType.I1)]" );
                }

                if ( t.StartsWith( "char " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "char", "" ).Trim( '[', ']', ' ' );
                    t = "string";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValTStr, SizeConst = {num})]" );
                }

                if ( t.StartsWith( "uint8 " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "uint8", "" ).Trim( '[', ']', ' ' );
                    t = "char";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValTStr, SizeConst = {num})]" );
                }

                if ( t.StartsWith( "CSteamID " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "CSteamID", "" ).Trim( '[', ']', ' ' );
                    t = $"ulong[]";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.U8)]" );
                }

                if ( t.StartsWith( "PublishedFileId_t " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "PublishedFileId_t", "" ).Trim( '[', ']', ' ' );
                    t = $"ulong[]";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.U8)]" );
                }

                if ( t.StartsWith( "uint32 " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "uint32", "" ).Trim( '[', ']', ' ' );
                    t = $"uint[]";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.U8)]" );
                }

                if ( t.StartsWith( "float " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "float", "" ).Trim( '[', ']', ' ' );
                    t = $"float[]";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.R4)]" );
                }

                if ( t == "const char **" )
                {
                    t = "IntPtr";
                }

                WriteLine( $"public {t} {CleanMemberName( m.Name )}; // {m.Name} {m.Type}" );
            }
        }
    }
}
