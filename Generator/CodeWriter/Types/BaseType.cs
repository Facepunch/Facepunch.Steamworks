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

	public string Func;

	public virtual bool WindowsSpecific => false;

	public static BaseType Parse( string type, string varname = null )
	{
		type = Cleanup.ConvertType( type );

		if ( varname == "ppOutMessages" ) return new PointerType { NativeType = "void *", VarName = varname };
		if ( type == "SteamAPIWarningMessageHook_t" ) return new PointerType { NativeType = type, VarName = varname };

		if ( type == "SteamAPICall_t" ) return new SteamApiCallType { NativeType = type, VarName = varname };

		if ( type == "void" ) return new VoidType { NativeType = type, VarName = varname };
		if ( type.Replace( " ", "" ).StartsWith( "constchar*" ) ) return new ConstCharType { NativeType = type, VarName = varname };
		if ( type == "char *" ) return new StringBuilderType { NativeType = type, VarName = varname };

		var basicType = type.Replace( "const ", "" ).Trim( ' ', '*', '&' );

		if ( basicType == "void" ) return new PointerType { NativeType = type, VarName = varname };
		if ( basicType.StartsWith( "ISteam" ) ) return new PointerType { NativeType = type, VarName = varname };
		if ( basicType == "const void" ) return new PointerType { NativeType = type, VarName = varname };
		if ( basicType == "int32" || basicType == "int" ) return new IntType { NativeType = type, VarName = varname };
		if ( basicType == "uint32" ) return new UIntType { NativeType = type, VarName = varname };
		if ( basicType == "uint8" ) return new UInt8Type { NativeType = type, VarName = varname };
		if ( basicType == "uint16" ) return new UInt16Type { NativeType = type, VarName = varname };
		if ( basicType == "SteamId" ) return new CSteamIdType { NativeType = type, VarName = varname };

		// DANGER DANGER Danger
		if ( basicType == "size_t" ) return new ULongType { NativeType = type, VarName = varname };
		if ( basicType == "intptr_t" ) return new LongType { NativeType = type, VarName = varname };
		if ( basicType == "ptrdiff_t" ) return new LongType { NativeType = type, VarName = varname };

		if ( basicType == "uint64" ) return new ULongType { NativeType = type, VarName = varname };
		if ( basicType == "int64" ) return new LongType { NativeType = type, VarName = varname };
		if ( basicType == "bool" ) return new BoolType { NativeType = type, VarName = varname };

		if ( basicType.EndsWith( "_t" ) ) return new StructType { NativeType = type, VarName = varname, StructName = basicType };
		if ( basicType == "InventoryItemId" ) return new StructType { NativeType = type, VarName = varname, StructName = basicType };
		if ( basicType == "InventoryDefId" ) return new StructType { NativeType = type, VarName = varname, StructName = basicType };
		if ( basicType == "PingLocation" ) return new StructType { NativeType = type, VarName = varname, StructName = basicType };
		if ( basicType == "NetIdentity" ) return new StructType { NativeType = type, VarName = varname, StructName = basicType };
		if ( basicType == "NetAddress" ) return new StructType { NativeType = type, VarName = varname, StructName = basicType };
		if ( basicType == "ConnectionInfo" ) return new StructType { NativeType = type, VarName = varname, StructName = basicType };
		if ( basicType.StartsWith( "E" ) && char.IsUpper( basicType[1] ) ) return new EnumType { NativeType = type.Substring( 1 ), VarName = varname };

		return new BaseType { NativeType = type, VarName = varname };
	}

	public virtual string AsArgument() => IsVector ? $"[In,Out] {Ref}{TypeName.Trim( '*', ' ' )}[]  {VarName}" : $"{Ref}{TypeName.Trim( '*', ' ' )} {VarName}";
	public virtual string AsWinArgument() => AsArgument();
	public virtual string AsCallArgument() => $"{Ref}{VarName}";

	public virtual string Return( string varname ) => $"return {varname};";
	public virtual string ReturnAttribute => null;

	public virtual string ReturnType => TypeName;

	public virtual string Ref => !IsVector && NativeType.EndsWith( "*" ) || NativeType.EndsWith( "**" ) || NativeType.Contains( "&" ) ? "ref " : "";
	public virtual bool IsVector
	{
		get
		{
			if ( Func == "ReadP2PPacket" ) return false;
			if ( Func == "SendP2PPacket" ) return false;

			if ( VarName == "pOut" ) return false;
			if ( VarName == "pOutBuffer" ) return false;
			if ( VarName == "pubRGB" ) return false;
			if ( VarName == "pOutResultHandle" ) return false;
			if ( VarName == "pOutDataType" ) return false;

			if ( VarName == "psteamIDClans" ) return true;
			if ( VarName == "pScoreDetails" ) return true;
			if ( VarName == "prgUsers" ) return true;
			if ( VarName == "pBasePrices" ) return true;
			if ( VarName == "pCurrentPrices" ) return true;
			if ( VarName == "pItemDefIDs" ) return true;
			if ( VarName == "pDetails" && Func == "GetDownloadedLeaderboardEntry" ) return true;
			if ( VarName == "pData" && NativeType.EndsWith( "*" ) && Func.StartsWith( "GetGlobalStatHistory" ) ) return true;
			if ( NativeType.EndsWith( "**" ) ) return true;
			if ( VarName.StartsWith( "pArray" ) ) return true;
			if ( VarName.StartsWith( "punArray" ) ) return true;

			if ( NativeType.EndsWith( "*" ) )
			{
				if ( VarName.StartsWith( "pvec" ) ) return true;
				if ( VarName.StartsWith( "pub" ) ) return true;
				if ( VarName.StartsWith( "pOut" ) ) return true;
			}

			return false;
		}
	}


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

	public override string TypeNameFrom => NativeType.EndsWith( "*" ) ? "IntPtr" : base.ReturnType;

	public override string Return( string varname )
	{
		if ( NativeType.EndsWith( "*" ) )
		{
			return $"return {TypeName}.Fill( {varname} );";
		}

		return base.Return( varname );
	}

	public override bool WindowsSpecific
	{
		get
		{
			var s = Generator.Program.Definitions.structs.FirstOrDefault( x => x.Name == StructName );
			if ( s == null ) return false;

			return !s.IsPack4OnWindows;
		}
	}

	public override string AsWinArgument()
	{
		if ( WindowsSpecific )
			return IsVector ? $"[In,Out] {Ref}{TypeName.Trim( '*', ' ' )}.Pack8[]  {VarName}" : $"{Ref}{TypeName.Trim( '*', ' ' )}.Pack8 {VarName}";

		return AsArgument();
	}
}

internal class SteamApiCallType : BaseType
{
	public string CallResult;
	public override string TypeName => "SteamAPICall_t";
	public override string Return( string varname ) => $"return await {CallResult}.GetResultAsync( {varname} );";
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

internal class LongType : BaseType
{
	public override string TypeName => $"long";
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