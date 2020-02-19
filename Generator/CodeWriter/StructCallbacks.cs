using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CodeWriter
    {
        void StructCallbacks()
        {
            var callbackList = new List<SteamApiDefinition.StructDef>();

            foreach ( var c in def.callback_structs )
            {
				var name = Cleanup.ConvertType( c.Name );

				if ( SkipStructs.Contains( c.Name ) )
                    continue;

				if ( !Cleanup.ShouldCreate( name ) )
					continue;

                if ( name.Contains( "::" ) )
                    continue;

				int defaultPack = c.IsPack4OnWindows ? 4 : 8;

				var isCallback = true;
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

					if ( c.Enums != null )
					{
						foreach ( var e in c.Enums )
						{
							WriteEnum( e, e.Name );
						}
					}

					// if (  c.CallbackId ) )
					{
                        callbackList.Add( c );
                    }

                }
                EndBlock();
                WriteLine();
            }
        }
    }
}
