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

	public static BaseType Parse( string type, string varname = null, string callresult = null )
	{
		type = Cleanup.ConvertType( type );

		if ( varname == "ppOutMessages" ) return new PointerType { NativeType = "void *", VarName = varname };
		if ( type == "SteamAPIWarningMessageHook_t" ) return new PointerType { NativeType = type, VarName = varname };

		if ( type == "SteamAPICall_t" ) return new SteamApiCallType { NativeType = type, VarName = varname, CallResult = callresult };

		if ( type == "void" ) return new VoidType { NativeType = type, VarName = varname };
		if ( type.Replace( " ", "" ).StartsWith( "constchar*" ) ) return new ConstCharType { NativeType = type, VarName = varname };
		if ( type == "char *" ) return new FetchStringType { NativeType = type, VarName = varname };

		var basicType = type.Replace( "const ", "" ).Trim( ' ', '*', '&' );

		if ( basicType == "void" ) return new PointerType { NativeType = type, VarName = varname };
		if ( basicType.StartsWith( "ISteam" ) ) return new PointerType { NativeType = type, VarName = varname };
		if ( basicType == "const void" ) return new PointerType { NativeType = type, VarName = varname };
		if ( basicType == "int32" || basicType == "int" ) return new IntType { NativeType = type, VarName = varname };
		if ( basicType == "uint" ) return new UIntType { NativeType = type, VarName = varname };
		if ( basicType == "uint8" ) return new UInt8Type { NativeType = type, VarName = varname };
		if ( basicType == "uint16" ) return new UInt16Type { NativeType = type, VarName = varname };
		if ( basicType == "unsigned short" ) return new UInt16Type { NativeType = type, VarName = varname };
		
		// DANGER DANGER Danger
		if ( basicType == "intptr_t" ) return new PointerType { NativeType = type, VarName = varname };
		if ( basicType == "size_t" ) return new UIntPtrType { NativeType = type, VarName = varname };

		if ( basicType == "uint64" ) return new ULongType { NativeType = type, VarName = varname };
		if ( basicType == "int64" ) return new LongType { NativeType = type, VarName = varname };
		if ( basicType == "bool" ) return new BoolType { NativeType = type, VarName = varname };

		//
		// Enum types are handled in a generic way, but we do need to clean up the name
		//
		if ( Generator.CodeWriter.Current.IsEnum( basicType ) )
		{
			return new BaseType { NativeType = Cleanup.CleanEnum( type ), VarName = varname };
		}

		//
		// Structs are generally sent as plain old data, but need marshalling if they're expected as a pointer
		//
		if ( Generator.CodeWriter.Current.IsStruct( basicType ) )
		{
			return new StructType { NativeType = type, VarName = varname, StructName = basicType };
		}

		//
		// c# doesn't really have typerdefs, so we convert things like HSteamUser into structs
		// which from a memory point of view behave in the same way.
		//
		if ( Generator.CodeWriter.Current.IsTypeDef( basicType ) )
		{
			return new StructType { NativeType = type, VarName = varname, StructName = basicType };
		}

		return new BaseType { NativeType = type, VarName = varname };
	}

	public virtual bool ShouldSkipAsArgument => false;

	public virtual string AsNativeArgument() => AsArgument();
	public virtual string AsArgument()
    {
        if (IsVector)
        {
            return $"[In,Out] {Ref}{TypeName.Trim('*', ' ', '&')}[]  {VarName}";
        }

        return TreatAsPointer
            ? $"{Ref}{TypeName}{new string('*', NativeType.Count(c => c == '*'))} {VarName}"
			: $"{Ref}{TypeName.Trim('*', ' ', '&')} {VarName}";
    }

    public virtual string AsCallArgument() => $"{Ref}{VarName}";

	public virtual string Return( string varname ) => $"return {varname};";
	public virtual string ReturnAttribute => null;

	public virtual string ReturnType => TypeName;

	public virtual string Ref => !TreatAsPointer && !IsVector && NativeType.EndsWith( "*" ) || NativeType.EndsWith( "**" ) || NativeType.Contains( "&" ) ? "ref " : "";
	
	public virtual bool IsVector
	{
		get
        {
            if ( TreatAsPointer ) return false;

			if ( Func == "ReadP2PPacket" ) return false;
			if ( Func == "SendP2PPacket" ) return false;
			if ( VarName == "pOutMessageNumber" ) return false;
			if ( VarName == "pOptions" ) return true;
			if ( VarName == "pLanes" ) return true;
			if ( VarName == "pLanePriorities" ) return true;
			if ( VarName == "pLaneWeights" ) return true;

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
			if ( VarName == "handlesOut" ) return true;
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

	public virtual bool TreatAsPointer => VarName == "pOutMessageNumberOrResult";

	public virtual bool IsVoid => false;
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

internal class UIntPtrType : BaseType
{
	public override string TypeName => $"UIntPtr";
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
