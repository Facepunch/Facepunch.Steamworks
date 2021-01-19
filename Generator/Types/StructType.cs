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

    public override string TypeName => IsPointer && TreatAsPointer ? StructName + PointerSuffix : StructName;

	public override string TypeNameFrom => IsPointer && !TreatAsPointer ? "IntPtr" : TypeName;

    public override string AsArgument() => IsPointer && TreatAsPointer ? $"{TypeName} {VarName}" : base.AsArgument();

    public override string AsCallArgument() => IsPointer && TreatAsPointer ? VarName : base.AsCallArgument();

    public override bool TreatAsPointer => StructName == "NetMsg";

    public bool IsPointer => NativeType.EndsWith( "*" );

    public override string Return( string varname )
	{
		if ( IsPointer && !TreatAsPointer )
		{
            return $"return {varname}.ToType<{TypeName}>();";
		}

		return base.Return( varname );
	}

    private string PointerSuffix => new string( '*', NativeType.Count( c => c == '*' ) );
}