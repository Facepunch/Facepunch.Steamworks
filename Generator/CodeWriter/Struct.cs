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

				int defaultPack = c.IsPack4OnWindows ? 4 : 8;

				var isCallback = !string.IsNullOrEmpty( c.CallbackId );
                var iface = "";
                if ( isCallback )
                    iface = " : ICallbackData";

                //
                // Main struct
                //
                WriteLine( $"[StructLayout( LayoutKind.Sequential, Pack = Platform.{(c.IsPack4OnWindows?"StructPackSize": "StructPlatformPackSize")} )]" );
                StartBlock( $"{Cleanup.Expose( name )} struct {name}{iface}" );
                {
					//
					// The fields
					//
					StructFields( c.Fields );
					WriteLine();

					if ( isCallback )
                    {
						WriteLine( "#region SteamCallback" );
						{

							WriteLine( $"public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof({name}) );" );
							WriteLine( $"public int DataSize => _datasize;" );
                            WriteLine( $"public int CallbackId => {c.CallbackId};" );

                            WriteLine( $"internal static {name} Fill( IntPtr p ) => (({name})Marshal.PtrToStructure( p, typeof({name}) ) );" );
							WriteLine();
							WriteLine( $"static Action<{name}> actionClient;" );
							WriteLine( $"[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );" );

							WriteLine( $"static Action<{name}> actionServer;" );
							WriteLine( $"[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );" );

							StartBlock( $"public static void Install( Action<{name}> action, bool server = false )" );
							{
								StartBlock( "if ( server )" );
								{
									WriteLine( $"Event.Register( OnServer, _datasize, {c.CallbackId}, true );" );
									WriteLine( $"actionServer = action;" );
								}
								Else();
								{
									WriteLine( $"Event.Register( OnClient, _datasize, {c.CallbackId}, false );" );
									WriteLine( $"actionClient = action;" );
								}
								EndBlock();

							}
							EndBlock();
						}
						WriteLine( "#endregion" );
					}
					else
					{
						WriteLine( "#region Marshalling" );
						{
							WriteLine( $"internal static {name} Fill( IntPtr p ) => (({name})({name}) Marshal.PtrToStructure( p, typeof({name}) ) );" );
						}
						WriteLine( "#endregion" );
					}

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
