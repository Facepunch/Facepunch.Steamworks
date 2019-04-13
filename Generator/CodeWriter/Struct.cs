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

        private Dictionary<string, TypeDef> TypeDefs = new Dictionary<string, TypeDef>();

        //
        // Don't give a fuck about these classes, they just cause us trouble
        //
        public readonly static string[] SkipStructs = new string[]
        {
            "CSteamID",
            "CSteamAPIContext",
            "CCallResult",
            "CCallback",
            "ValvePackingSentinel_t",
            "CCallbackBase",
			"CSteamGameServerAPIContext"
		};

        public readonly static string[] ForceLargePackStructs = new string[]
        {
            "LeaderboardEntry_t"
        };

        void Structs()
        {
            var callbackList = new List<SteamApiDefinition.StructDef>();

            foreach ( var c in def.structs )
            {
                if ( SkipStructs.Contains( c.Name ) )
                    continue;

                if ( c.Name.Contains( "::" ) )
                    continue;

                int defaultPack = 8;

                if (  c.Fields.Any( x => x.Type.Contains( "class CSteamID" ) ) && !ForceLargePackStructs.Contains( c.Name ) )
                    defaultPack = 4;

				var isCallback = !string.IsNullOrEmpty( c.CallbackId );

				//
				// Main struct
				//
                StartBlock( $"public struct {c.Name}{(isCallback?" : Steamworks.ISteamCallback":"")}" );
                {
					//
					// The fields
					//
					StructFields( c.Fields );
					WriteLine();

					if ( isCallback )
                    {
						WriteLine( "#region ISteamCallback" );
						{
							WriteLine( $"public int GetCallbackId() => {c.CallbackId};" );
							WriteLine( $"public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );" );
							WriteLine( $"public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? (({c.Name})(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : (({c.Name})(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));" );
						}
						WriteLine( "#endregion" );
					}
					else
					{
						WriteLine( "#region Marshalling" );
						{
							WriteLine( $"public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );" );
							WriteLine( $"public {c.Name} Fill( IntPtr p ) => Platform.PackSmall ? (({c.Name})(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : (({c.Name})(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));" );
						}
						WriteLine( "#endregion" );
					}

					WriteLine( "#region Packed Versions" );
					{
						//
						// Small packed struct (for osx, linux)
						//

						WriteLine( $"[StructLayout( LayoutKind.Sequential, Pack = 4 )]" );
						StartBlock( $"public struct Pack4" );
						{
							StructFields( c.Fields );

							//
							// Implicit convert from PackSmall to regular
							//
							WriteLine();
							Write( $"public static implicit operator {c.Name} ( {c.Name}.Pack4 d ) => " );
							{
								Write( $"new {c.Name}{{ " );
								{
									foreach ( var f in c.Fields )
									{
										Write( $"{CleanMemberName( f.Name )} = d.{CleanMemberName( f.Name )}," );
									}
								}
								WriteLine( " };" );
							}

						}
						EndBlock();

						//
						// Small packed struct (for osx, linux)
						//
						WriteLine();
						WriteLine( $"[StructLayout( LayoutKind.Sequential, Pack = {defaultPack} )]" );
						StartBlock( $"public struct Pack8" );
						{
							StructFields( c.Fields );

							//
							// Implicit convert from PackSmall to regular
							//
							WriteLine();
							Write( $"public static implicit operator {c.Name} ( {c.Name}.Pack8 d ) => " );
							{
								Write( $"new {c.Name}{{ " );
								{
									foreach ( var f in c.Fields )
									{
										Write( $"{CleanMemberName( f.Name )} = d.{CleanMemberName( f.Name )}," );
									}
								}
								WriteLine( " };" );
							}

						}
						EndBlock();

					}
					WriteLine( "#endregion" );

                    if ( !string.IsNullOrEmpty( c.CallbackId ) )
                    {
                        callbackList.Add( c );
                    }

                }
                EndBlock();
                WriteLine();
            }

            StartBlock( $"internal static class Callbacks" );
            StartBlock( $"internal static void RegisterCallbacks( Facepunch.Steamworks.BaseSteamworks steamworks )" );
            {
                foreach ( var c in callbackList )
                {
                    WriteLine( $"new CallbackHandle<{c.Name}>( steamworks );" );
                }
            }
            EndBlock();
            EndBlock();
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
                    t = "byte[]";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num})] //  {m.Name}" );
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

                if (t.StartsWith("AppId_t ") && t.Contains("["))
                {
                    var num = t.Replace("AppId_t", "").Trim('[', ']', ' ');
                    t = $"AppId_t[]";
                    WriteLine($"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.U4)]");
                }

                WriteLine( $"internal {t} {CleanMemberName( m.Name )}; // {m.Name} {m.Type}" );
            }
        }
    }
}
