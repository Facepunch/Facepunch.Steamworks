using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CSharpGenerator
    {
        void Structs()
        {
            foreach ( var c in def.structs )
            {
                if ( c.Name == "CSteamID" ||
                    c.Name == "CSteamAPIContext" ||
                    c.Name == "CCallResult" ||
                    c.Name == "CCallback" )
                    continue;

                if ( c.Name.Contains( "::" ) )
                    continue;

                int defaultPack = 8;

                if ( c.Fields.Any( x => x.Type.Contains( "class CSteamID" ) ) )
                    defaultPack = 4;

                WriteLine( $"[StructLayout( LayoutKind.Sequential, Pack = {defaultPack} )]" );
                StartBlock( $"public struct {c.Name}" );

                StructFields( c.Fields );

                WriteLine( $"public static {c.Name} FromPointer( IntPtr p ) {{ return ({c.Name}) Marshal.PtrToStructure( p, typeof({c.Name}) ); }}" );


                if ( defaultPack == 8 )
                    defaultPack = 4;

                WriteLine();
                WriteLine( $"[StructLayout( LayoutKind.Sequential, Pack = {defaultPack} )]" );
                StartBlock( $"public struct PackSmall" );
                StructFields( c.Fields );

                WriteLine();
                StartBlock( $"public static implicit operator  {c.Name} (  {c.Name}.PackSmall d )" );
                StartBlock( $"return new {c.Name}()" );
                foreach ( var f in c.Fields )
                {
                    WriteLine( $"{f.Name} = d.{f.Name}," );
                }
                EndBlock( ";" );
                EndBlock();

                EndBlock();


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

                WriteLine( $"public {t} {m.Name}; // {m.Type}" );
            }
        }
    }
}
