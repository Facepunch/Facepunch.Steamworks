using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// The struct can be passed as a pointer or as plain old data.
/// If it's a pointer we need to marshal it back to our type
/// (this only happens in a couple of places in the sdk)
/// </summary>

internal class StructType : BaseType
{
	public string StructName;

	public override string TypeName => StructName;

	public override string TypeNameFrom => NativeType.EndsWith( "*" ) ? "IntPtr" : base.ReturnType;

	public override string Return( string varname )
	{
		if ( NativeType.EndsWith( "*" ) )
		{
			return $"return {varname}.ToType<{TypeName}>();";
		}

		return base.Return( varname );
	}
}