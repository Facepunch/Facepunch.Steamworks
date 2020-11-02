using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CodeWriter
    {
        void Callbacks()
        {
            var callbackList = new List<SteamApiDefinition.StructDef>();

            foreach ( var c in def.callback_structs )
            {
				var name = Cleanup.ConvertType( c.Name );

				if ( !Cleanup.ShouldCreate( name ) )
					continue;

                if ( name.Contains( "::" ) )
                    continue;

                var partial = "";
                if ( c.Methods != null ) partial = " partial";

                int defaultPack = c.IsPack4OnWindows ? 4 : 8;

				var isCallback = true;
                var iface = "";
                if ( isCallback )
                    iface = " : ICallbackData";

                //
                // Main struct
                //
                WriteLine( $"[StructLayout( LayoutKind.Sequential, Pack = Platform.{(c.IsPack4OnWindows?"StructPackSize": "StructPlatformPackSize")} )]" );
                StartBlock( $"{Cleanup.Expose( name )}{partial} struct {name}{iface}" );
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
                            WriteLine( $"public CallbackType CallbackType => CallbackType.{name.Replace( "_t", "" )};" );
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
