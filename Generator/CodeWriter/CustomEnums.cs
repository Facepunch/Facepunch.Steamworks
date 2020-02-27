using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CodeWriter
    {
        void CustomEnums()
        {
			StartBlock( "public enum CallbackType" );
			foreach ( var c in def.callback_structs.OrderBy( x => x.CallbackId ) )
			{
				WriteLine( $"{c.Name.Replace( "_t", "" ) } = {c.CallbackId}," );
			}
			EndBlock();

			StartBlock( "internal static partial class CallbackTypeFactory" );
			StartBlock( "internal static System.Collections.Generic.Dictionary<CallbackType, System.Type> All = new System.Collections.Generic.Dictionary<CallbackType, System.Type>" );
			foreach ( var c in def.callback_structs.OrderBy( x => x.CallbackId ) )
			{
				if ( Cleanup.IsDeprecated( c.Name ) )
					Write( "// " );

				WriteLine( $"{{ CallbackType.{c.Name.Replace( "_t", "" ) }, typeof( {Cleanup.ConvertType(c.Name)} )}}," );
			}
			EndBlock( ";" );
			EndBlock();
		}
    }
}
