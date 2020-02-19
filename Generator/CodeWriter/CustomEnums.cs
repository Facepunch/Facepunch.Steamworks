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
			StartBlock( "enum CallbackType" );
			foreach ( var c in def.callback_structs.OrderBy( x => x.CallbackId ) )
			{
				WriteLine( $"{c.Name.Replace( "_t", "" ) } = {c.CallbackId}," );
			}
			EndBlock();
        }
    }
}
