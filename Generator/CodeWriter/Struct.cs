using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CodeWriter
    {
        public class TypeDef
        {
            public string Name;
            public string NativeType;
            public string ManagedType;
        }

        private readonly Dictionary<string, TypeDef> TypeDefs = new Dictionary<string, TypeDef>();

        void Structs()
        {
            foreach ( var c in def.structs )
            {
				var name = Cleanup.ConvertType( c.Name );

				if ( !Cleanup.ShouldCreate( name ) )
					continue;

                if ( name.Contains( "::" ) )
                    continue;

                var partial = "";
                if ( c.Methods != null ) partial = " partial";

                //
                // Main struct
                //
                WriteLine( $"[StructLayout( LayoutKind.Sequential, Pack = Platform.{(c.IsPack4OnWindows?"StructPackSize": "StructPlatformPackSize")} )]" );
                StartBlock( $"{Cleanup.Expose( name )}{partial} struct {name}" );
                {
					//
					// The fields
					//
					StructFields( c.Fields );
					WriteLine();

                    if ( c.Enums != null )
                    {
                        foreach ( var e in c.Enums )
                        {
                            WriteEnum( e, e.Name );
                        }
                    }

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

				t = Cleanup.ConvertType( t );

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
					WriteLine( $"internal string {CleanMemberName( m.Name )}UTF8() => System.Text.Encoding.UTF8.GetString( {CleanMemberName( m.Name )}, 0, System.Array.IndexOf<byte>( {CleanMemberName( m.Name )}, 0 ) );" );

					var num = t.Replace( "char", "" ).Trim( '[', ']', ' ' );
					t = "byte[]";
					WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num})] // {t} {m.Name}" );
                }

                if ( t.StartsWith( "uint8 " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "uint8", "" ).Trim( '[', ']', ' ' );
                    t = "byte[]";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num})] //  {m.Name}" );
                }

                if ( t.StartsWith( "ushort " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "ushort", "" ).Trim( '[', ']', ' ' );
                    t = $"ushort[]";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.U2)]" );
                }

                if ( t.StartsWith( "SteamId" ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "SteamId", "" ).Trim( '[', ']', ' ' );
                    t = $"ulong[]";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.U8)]" );
                }

                if ( t.StartsWith( "PublishedFileId " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "PublishedFileId", "" ).Trim( '[', ']', ' ' );
                    t = $"PublishedFileId[]";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.U8)]" );
                }

                if ( t.StartsWith( "uint32 " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "uint32", "" ).Trim( '[', ']', ' ' );
                    t = $"uint[]";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.U4)]" );
                }

                if ( t.StartsWith( "uint " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "uint", "" ).Trim( '[', ']', ' ' );
                    t = $"uint[]";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.U4)]" );
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

                if (t.StartsWith("AppId ") && t.Contains("["))
                {
                    var num = t.Replace("AppId", "").Trim('[', ']', ' ');
                    t = $"AppId[]";
                    WriteLine($"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.U4)]");
                }

                if ( t == "SteamInputActionEvent_t.AnalogAction_t" )
                {
                    Write( "// " );
                }

                WriteLine( $"internal {t} {CleanMemberName( m.Name )}; // {m.Name} {m.Type}" );
            }
        }
    }
}
