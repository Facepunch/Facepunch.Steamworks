using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class BaseType
{
	public string VarName;
	public string NativeType;
	public virtual string TypeName => $"{NativeType}";
	public virtual string TypeNameFrom => TypeName;

	public static BaseType Parse( string type, string varname = null )
	{
		if ( type == "SteamAPIWarningMessageHook_t" ) return new PointerType { NativeType = type, VarName = varname };

		if ( type == "SteamAPICall_t" ) return new SteamApiCallType { NativeType = type, VarName = varname };

		if ( type == "void" ) return new VoidType { NativeType = type, VarName = varname };
		if ( type.Replace( " ", "" ) == "constchar*" ) return new ConstCharType { NativeType = type, VarName = varname };
		if ( type == "char *" ) return new StringBuilderType { NativeType = type, VarName = varname };

		var basicType = type.Trim( ' ', '*' );

		if ( basicType == "void" ) return new PointerType { NativeType = type, VarName = varname };
		if ( basicType.StartsWith( "ISteam" ) ) return new PointerType { NativeType = type, VarName = varname };
		if ( basicType == "const void" ) return new PointerType { NativeType = type, VarName = varname };
		if ( basicType == "int32" || basicType == "int" ) return new IntType { NativeType = type, VarName = varname };
		if ( basicType == "uint32" ) return new UIntType { NativeType = type, VarName = varname };
		if ( basicType == "uint8" ) return new UInt8Type { NativeType = type, VarName = varname };
		if ( basicType == "uint16" ) return new UInt16Type { NativeType = type, VarName = varname };
		if ( basicType == "CSteamID" ) return new CSteamIdType { NativeType = type, VarName = varname };
		if ( basicType == "uint64" ) return new ULongType { NativeType = type, VarName = varname };
		if ( basicType == "bool" ) return new BoolType { NativeType = type, VarName = varname };

		if ( basicType.EndsWith( "_t" ) || basicType == "CSteamID" ) return new StructType { NativeType = type, VarName = varname, StructName = basicType };
		if ( basicType.StartsWith( "E" ) && char.IsUpper( basicType[1] ) ) return new EnumType { NativeType = type.Substring( 1 ), VarName = varname };

		return new BaseType { NativeType = type, VarName = varname };
	}

	public virtual string AsArgument() => IsVector? $"[In,Out] {TypeName}[]  {VarName}" : $"{Ref}{TypeName} {VarName}";
	public virtual string AsCallArgument() => $"{Ref}{VarName}";

	public virtual string Return( string varname ) => $"return {varname};";
	public virtual string ReturnAttribute => null;

	public virtual string ReturnType => TypeName;

	public virtual string Ref => !IsVector && NativeType.EndsWith( "*" ) ? "ref " : "";
	public virtual bool IsVector => (NativeType.EndsWith( "*" ) && (VarName.StartsWith( "pvec" ) || VarName.StartsWith( "pub" ))) || NativeType.EndsWith( "**" );

	public virtual bool IsVoid => false;
	public virtual bool IsReturnedWeird => false;
}


internal class BoolType : BaseType
{
	public override string TypeName => $"bool";
	public override string AsArgument() => $"[MarshalAs( UnmanagedType.U1 )] {Ref}{TypeName} {VarName}";
	public override string ReturnAttribute => "[return: MarshalAs( UnmanagedType.I1 )]";

}

internal class StructType : BaseType
{
	public string StructName;

	public override string TypeName => StructName;
}

internal class SteamApiCallType : BaseType
{
	public string CallResult;
	public override string TypeName => "SteamAPICall_t";
	public override string Return( string varname ) => $"return await (new Result<{CallResult}>( {varname} )).GetResult();";
	public override string ReturnType => $"async Task<{CallResult}?>";
}

internal class CSteamIdType : BaseType
{
	public override bool IsReturnedWeird => true;
}

internal class IntType : BaseType
{
	public override string TypeName => $"int";
}

internal class UIntType : BaseType
{
	public override string TypeName => $"uint";
}
internal class UInt8Type : BaseType
{
	public override string TypeName => $"byte";
}

internal class UInt16Type : BaseType
{
	public override string TypeName => $"ushort";
}

internal class PointerType : BaseType
{
	public override string TypeName => $"IntPtr";
	public override string Ref => "";
}



internal class ULongType : BaseType
{
	public override string TypeName => $"ulong";
}

internal class ConstCharType : BaseType
{
	public override string TypeName => $"string";
	public override string TypeNameFrom => $"IntPtr";
	public override string Return( string varname ) => $"return GetString( {varname} );";

	public override string Ref => "";
}

internal class StringBuilderType : BaseType
{
	public override string TypeName => $"StringBuilder";
	public override string Ref => "";
}

internal class VoidType : BaseType
{
	public override string TypeName => $"void";
	public override string TypeNameFrom => $"void";
	public override string Return( string varname ) => $"";
	public override bool IsVoid => true;
}

internal class EnumType : BaseType
{

}