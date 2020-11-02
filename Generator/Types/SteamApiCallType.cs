using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Functions returning SteamAPICall_t are converted to be asyncronhous
/// and return a generic CallResult struct with the T of the result type.
/// </summary>
internal class SteamApiCallType : BaseType
{
	public string CallResult;
	public override string TypeName => "SteamAPICall_t";
	public override string Return( string varname )
	{
		return $"return new CallResult<{CallResult}>( {varname}, IsServer );";
	}

	public override string ReturnType
	{
		get
		{
			if ( !string.IsNullOrEmpty( CallResult ) )
				return $"CallResult<{CallResult}>";

			return $"CallResult";
		}
	}
}