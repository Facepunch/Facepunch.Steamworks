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
				var name = Cleanup.ConvertType( c.Name );

				if ( SkipStructs.Contains( c.Name ) )
                    continue;

				if ( !Cleanup.ShouldCreate( name ) )
					continue;

                if ( name.Contains( "::" ) )
                    continue;

				

				int defaultPack = 8;

                if ( c.Fields.Any( x => x.Type.Contains( "CSteamID" ) ) && !ForceLargePackStructs.Contains( c.Name ) )
                    defaultPack = 4;

				var isCallback = !string.IsNullOrEmpty( c.CallbackId );

				//
				// Main struct
				//
				WriteLine( "[StructLayout( LayoutKind.Sequential, Pack = 4 )]" );
                StartBlock( $"{Cleanup.Expose( name )} struct {name}{(isCallback?" : Steamworks.ISteamCallback":"")}" );
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

							if ( defaultPack == 4 )
							{
								WriteLine( $"public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof({name}) : typeof(Pack8) );" );
								WriteLine( $"public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? (({name})({name}) Marshal.PtrToStructure( p, typeof({name}) )) : (({name})(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));" );
							}
							else
							{
								WriteLine( $"public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof({name}) : typeof(Pack8) );" );
								WriteLine( $"public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? (({name})({name}) Marshal.PtrToStructure( p, typeof({name}) )) : (({name})(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));" );
							}
						}
						WriteLine( "#endregion" );
					}
					else
					{
						WriteLine( "#region Marshalling" );
						{
							WriteLine( $"public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof({name}) : typeof(Pack8) );" );
							WriteLine( $"public {name} Fill( IntPtr p ) => Config.PackSmall ? (({name})({name}) Marshal.PtrToStructure( p, typeof({name}) )) : (({name})(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));" );
						}
						WriteLine( "#endregion" );
					}

					WriteLine( "#region Packed Versions" );
					{
						//
						// Windows Packed version
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
							Write( $"public static implicit operator {name} ( {name}.Pack8 d ) => " );
							{
								Write( $"new {name}{{ " );
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

                WriteLine( $"internal {t} {CleanMemberName( m.Name )}; // {m.Name} {m.Type}" );
            }
        }
    }
}
