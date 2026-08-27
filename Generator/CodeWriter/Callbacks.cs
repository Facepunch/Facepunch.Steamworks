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

				var isCallback = true;
                var iface = "";
                if ( isCallback )
                    iface = " : ICallbackData";

                //
                // Main struct
                //
                // Every callback struct carries the pack value the header itself declares:
                // 8 on Windows, 4 on Linux/macOS (VALVE_CALLBACK_PACK_LARGE / _SMALL,
                // steamclientpublic.h:1161-1178). Fields whose native alignment differs from
                // their managed one - the pack(1) id classes - are modelled by their type
                // instead, see FieldType/PackedId. There used to be a per-struct heuristic
                // here; it could not express a mixed-alignment struct and mis-laid-out 16 of
                // them.
                //
                WriteLine( $"[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]" );
                StartBlock( $"{Cleanup.Expose( name )}{UnsafeModifier( c )}{partial} struct {name}{iface}" );
                {
					//
					// The fields
					//
					StructFields( c.Name, c.Fields );
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
