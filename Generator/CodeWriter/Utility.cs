using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CodeWriter
    {
        string CleanMemberName( string m )
        {
            if ( m == "m_pubParam" ) return "ParamPtr";
            if ( m == "m_cubParam" ) return "ParamCount";
            if ( m == "m_itemId" ) return "ItemId";
            if ( m == "m_handle" ) return "Handle";
            if (m == "m_result") return "Result";

            var cleanName = m.Replace( "m_un", "" )
                    .Replace( "m_us", "" )
                    .Replace( "m_sz", "" )
                    .Replace( "m_h", "" )
                    .Replace( "m_pp", "" )
                    .Replace( "m_e", "" )
                    .Replace( "m_un", "" )
                    .Replace( "m_ul", "" )
                    .Replace( "m_fl", "" )
                    .Replace( "m_u", "" )
                    .Replace( "m_b", "" )
                    .Replace( "m_i", "" )
                    .Replace( "m_pub", "" )
                    .Replace( "m_cub", "" )
                    .Replace( "m_n", "" )
                    .Replace( "m_rgch", "" )
                    .Replace( "m_r", "" )
                    .Replace( "m_", "" );

   

            return cleanName.Substring( 0, 1 ).ToUpper() + cleanName.Substring( 1 );
        }


        private string ToManagedType( string type )
        {
            type = type.Replace( "ISteamHTMLSurface::", "" );
            type = type.Replace( "class ", "" );
            type = type.Replace( "struct ", "" );
            type = type.Replace( "const void", "void" );
            type = type.Replace( "union ", "" );
            type = type.Replace( "enum ", "" );

			type = Cleanup.ConvertType( type );

			switch ( type )
            {
                case "uint64": return "ulong";
                case "uint32": return "uint";
                case "int32": return "int";
                case "int32_t": return "int";
                case "int64": return "long";
                case "int64_t": return "long";
                case "void *": return "IntPtr";
                case "uint8 *": return "IntPtr";
                case "int16": return "short";
                case "uint8": return "byte";
                case "int8": return "char";
                case "unsigned short": return "ushort";
                case "unsigned int": return "uint";
                case "uint16": return "ushort";
                case "const char *": return "string";
                case "_Bool": return "bool";
                case "SteamId": return "ulong";

                case "SteamAPIWarningMessageHook_t": return "IntPtr";
            }

            //type = type.Trim( '*', ' ' );

            // Enums - skip the 'E'
            if ( type[0] == 'E' )
            {
                return type.Substring( 1 );
            }

            if ( type.StartsWith( "ISteamMatchmak" ) && type.Contains( "Response" ) )
                return "IntPtr";

            return type;
        }

        private List<BaseType> ProcessArgs( SteamApiDefinition.Interface.Method func )
        {
	        var args = func.Params.Select( x =>
	        {
		        var bt = BaseType.Parse( x.ParamType, x.ParamName, bufferSizeName: x.OutStringCount );
		        bt.Func = func.Name;
		        return bt;
	        } ).ToList();

	        for ( var i = 0; i < args.Count; i++ )
	        {
		        if ( args[i] is not FetchStringType fetchString )
		        {
			        continue;
		        }

		        var bufferSizeIdx = -1;
		        if ( !string.IsNullOrWhiteSpace( fetchString.BufferSizeParamName ) )
		        {
			        bufferSizeIdx = args.FindIndex( x => x.VarName == fetchString.BufferSizeParamName );
		        }
		        else if ( args[i + 1] is IntType || args[i + 1] is UIntType || args[i + 1] is UIntPtrType )
		        {
			        bufferSizeIdx = i + 1;
		        }
				
		        if ( bufferSizeIdx >= 0 )
		        {
			        if ( args[bufferSizeIdx] is not LiteralType )
			        {
				        args[bufferSizeIdx] = new LiteralType( args[bufferSizeIdx], "(1024 * 32)" );
			        }
		        }
		        else
		        {
			        throw new System.Exception( $"Couldn't determine buffer size for parameter {args[i].VarName} in {func.FlatName}" );
		        }
	        }

	        return args;
        }
    }
}
